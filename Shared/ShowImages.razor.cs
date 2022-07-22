using Azure.Storage.Blobs;
using BalzorHandleImages.Services;

namespace BalzorHandleImages.Shared
{
    public partial class ShowImages : IBlobFactory
    {
        private readonly IBlobFactory _blobFactory;
        public ShowImages(IBlobFactory blobFactory)
        {
            _blobFactory = blobFactory ?? throw new ArgumentNullException(nameof(blobFactory));
        }

        public Task<BlobContainerClient> CreateContainerAsync(string containerName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BlobContainerClient> GetContainer(string containerName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUrlImageAsync(string imageName)
        {
            var blobContainer = await _blobFactory.GetContainer("uploaded-images",new CancellationToken());
            var cloudFile = blobContainer.GetBlobClient(imageName);
            Stream fileStream = new MemoryStream();
            cloudFile.DownloadTo(fileStream);
            //return fileStream;
            //Puedo transformat filestream a Base64?
            Console.WriteLine(cloudFile.Uri);
            return cloudFile.Uri.ToString();
        }



    }
}
