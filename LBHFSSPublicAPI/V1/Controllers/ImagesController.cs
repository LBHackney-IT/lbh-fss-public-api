using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LBHFSSPublicAPI.V1.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ImageStoreOptions _options;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IAmazonS3 s3Client, ImageStoreOptions options, ILogger<ImagesController> logger)
        {
            _s3Client = s3Client;
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Streams an image from the private S3 imagestore. Size can be "medium" or "original".
        /// S3 key format: images/{id}-{size}.jpg
        /// </summary>
        [HttpGet]
        [Route("{id}/{size}")]
        public async Task<IActionResult> GetImage(int id, string size)
        {
            if (string.IsNullOrEmpty(_options?.BucketName))
                return StatusCode(500, "Image store not configured (IMAGE_STORE_BUCKET).");

            if (!string.Equals(size, "medium", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(size, "original", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Size must be 'medium' or 'original'.");

            var key = $"images/{id}-{size.ToLowerInvariant()}.jpg";

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
                return File(stream, "image/jpeg", enableRangeProcessing: true);
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            catch (AmazonS3Exception ex)
            {
                var msg = $"S3 error: {ex.ErrorCode} - {ex.Message}";
                _logger.LogError(ex, "S3 GetObject failed. Bucket: {Bucket}, Key: {Key}, ErrorCode: {ErrorCode}", _options.BucketName, key, ex.ErrorCode);
                Response.Headers["X-Error-Message"] = msg;
                return StatusCode(502, msg);
            }
            catch (Exception ex)
            {
                var msg = $"Image error: {ex.Message}";
                _logger.LogError(ex, "Image request failed. Id: {Id}, Size: {Size}", id, size);
                Response.Headers["X-Error-Message"] = msg;
                return StatusCode(500, msg);
            }
        }
    }
}
