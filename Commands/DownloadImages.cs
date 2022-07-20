using System;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using BalzorHandleImages.Services;


namespace BalzorHandleImages.Commands
{
    public record DownloadImage(string ImgName) : INotification;
    public class DownloadImages : INotificationHandler<DownloadImage>
    {
        private readonly IBlobFactory _blobFactory;

        public DownloadImages(IBlobFactory blobFactory)
        {
            _blobFactory = blobFactory ?? throw new ArgumentNullException(nameof(blobFactory));
        }

        public async Task Handle(UploadImage command, CancellationToken cancellationToken)
        {
            var blobName = $"image_{command.ImageId}.jpg";
            var blobContainer = await _blobFactory.CreateContainerAsync("uploaded-images", cancellationToken);
            await blobContainer.DeleteBlobIfExistsAsync(blobName, cancellationToken: cancellationToken);

            if (command.ImageData is not null)
            {
                using var ms = new System.IO.MemoryStream(command.ImageData);
                await blobContainer.UploadBlobAsync(blobName, ms, cancellationToken);
            }
        }

        public async Task< Handle(DownloadImage notification, CancellationToken cancellationToken)
        {
            //no existe método para traer Contenedor, lo crea o lo trae
            var blobContainer = await _blobFactory.CreateContainerAsync("uploaded-images", cancellationToken);
            var blobitem = blobContainer.GetBlobsAsync(
                Azure.Storage.Blobs.Models.BlobTraits.None,
                Azure.Storage.Blobs.Models.BlobStates.None,
                notification.ImgName,
                cancellationToken: cancellationToken);
            return blobitem;
        }
    }
}
