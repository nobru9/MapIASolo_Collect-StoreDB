using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace SoilBot.Core.Services
{
    public class S3Uploader
    {
        private readonly IAmazonS3 _s3Client;
        public S3Uploader()
        {
            _s3Client = new AmazonS3Client(Amazon.RegionEndpoint.SAEast1);
        }

        public async Task UploadAsync(string bucketName, string fileName, string csvContent)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
            using var stream = new MemoryStream(bytes);
            var transfer = new TransferUtility(_s3Client);
            await transfer.UploadAsync(stream, bucketName, fileName);
        }
    }
}