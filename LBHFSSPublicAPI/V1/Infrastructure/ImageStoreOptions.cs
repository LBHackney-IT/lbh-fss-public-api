namespace LBHFSSPublicAPI.V1.Infrastructure
{
    public class ImageStoreOptions
    {
        public string BucketName { get; }

        public ImageStoreOptions(string bucketName)
        {
            BucketName = bucketName;
        }
    }
}
