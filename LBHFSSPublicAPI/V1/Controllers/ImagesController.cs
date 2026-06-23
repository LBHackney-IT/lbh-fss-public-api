using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LBHFSSPublicAPI.V1.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ImageStoreOptions _options;
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IAmazonS3 s3Client, ImageStoreOptions options, DatabaseContext databaseContext, ILogger<ImagesController> logger)
        {
            _s3Client = s3Client;
            _options = options;
            _databaseContext = databaseContext;
            _logger = logger;
        }

        /// <summary>
        /// Streams an image from the private S3 imagestore. Size can be "medium" or "original".
        /// Uses the image key stored against the service so original file extensions are preserved.
        /// </summary>
        [HttpGet]
        [Route("{id}/{size}")]
        public async Task<IActionResult> GetImage(int id, string size)
        {
            if (string.IsNullOrEmpty(_options?.BucketName))
                return StatusCode(500, "Image store not configured (IMAGE_STORE_BUCKET_NAME).");

            if (!string.Equals(size, "medium", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(size, "original", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Size must be 'medium' or 'original'.");

            var key = await GetImageKey(id, size).ConfigureAwait(false);
            if (string.IsNullOrEmpty(key))
                return NotFound();

            try
            {
                var request = new Amazon.S3.Model.GetObjectRequest
                {
                    BucketName = _options.BucketName,
                    Key = key
                };
                using var response = await _s3Client.GetObjectAsync(request);
                if (response?.ResponseStream == null)
                    return NotFound();

                var stream = new MemoryStream();
                await response.ResponseStream.CopyToAsync(stream);
                stream.Position = 0;
                return File(stream, GetContentType(key), enableRangeProcessing: true);
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (AmazonS3Exception ex)
            {
                var msg = $"S3 error: {ex.ErrorCode} - {ex.Message}";
                _logger.LogError(ex, "S3 GetObject failed. Bucket: {Bucket}, ErrorCode: {ErrorCode}", _options.BucketName, ex.ErrorCode);
                Response.Headers["X-Error-Message"] = msg;
                return StatusCode(502, msg);
            }
            catch (Exception ex)
            {
                var msg = $"Image error: {ex.Message}";
                _logger.LogError(ex, "Image request failed");
                Response.Headers["X-Error-Message"] = msg;
                return StatusCode(500, msg);
            }
        }

        private async Task<string> GetImageKey(int serviceId, string size)
        {
            var service = await _databaseContext.Services
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == serviceId)
                .ConfigureAwait(false);

            var imageUrls = service?.Image?.Url?.Split(';', StringSplitOptions.RemoveEmptyEntries);
            if (imageUrls == null || imageUrls.Length == 0)
                return null;

            var imageUrl = string.Equals(size, "medium", StringComparison.OrdinalIgnoreCase) && imageUrls.Length > 1
                ? imageUrls[1]
                : imageUrls[0];

            return ToS3Key(imageUrl);
        }

        private static string ToS3Key(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return null;

            if (Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri))
                return Uri.UnescapeDataString(uri.AbsolutePath.TrimStart('/'));

            return imageUrl.Trim().TrimStart('/');
        }

        private static string GetContentType(string key)
        {
            return Path.GetExtension(key).ToLowerInvariant() switch
            {
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "image/jpeg"
            };
        }
    }
}
