namespace ProjectCluster.Data.CloudinaryHelper
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryExtension
    {
        public static async Task<List<string>> UploadMultipleAsync(Cloudinary cloudinary, ICollection<IFormFile> pictures)
        {
            List<string> urls = new List<string>();

            foreach (var picture in pictures)
            {
                byte[] destinationImage;

                using (var memoryStream = new MemoryStream())
                {
                    await picture.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(picture.FileName, destinationStream),
                    };

                    var result = await cloudinary.UploadAsync(uploadParams);

                    urls.Add(result.Uri.AbsoluteUri);
                }
            }

            return urls;
        }

        public static async Task<string> UploadSingleAsync(Cloudinary cloudinary, IFormFile picture)
        {
            string url = string.Empty;
            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(picture.FileName, destinationStream),
                };

                var result = await cloudinary.UploadAsync(uploadParams);

                url = result.Uri.AbsoluteUri;
            }

            return url;
        }
    }
}