namespace TAK.Data.CloudinaryHelper
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

#pragma warning disable CS0618 // 'UploadResult.Uri' is obsolete: 'Property Uri is deprecated, please use Url instead'
                    urls.Add(result.Uri.AbsoluteUri);
#pragma warning restore CS0618 // 'UploadResult.Uri' is obsolete: 'Property Uri is deprecated, please use Url instead'
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

#pragma warning disable CS0618 // 'UploadResult.Uri' is obsolete: 'Property Uri is deprecated, please use Url instead'
                url = result.Uri.AbsoluteUri;
#pragma warning restore CS0618 // 'UploadResult.Uri' is obsolete: 'Property Uri is deprecated, please use Url instead'
            }
            //E
            return url;
        }
    }
}
