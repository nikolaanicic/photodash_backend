using Contracts.Services.ImagesService;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ServiceImplementations
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImage(IFormFile base64Image)
        {

            var randPart = GetRandPathPart();
            var folder = GetFolderName();
            var basePath = Directory.GetCurrentDirectory();
            var dbPath = Path.Combine(folder,randPart);

            var imagePath = Path.Combine(Path.Combine(basePath, folder), randPart);

            await Save(base64Image, imagePath);
            return dbPath;
        }


        private async Task Save(IFormFile image, string path)
        {
            using (FileStream fStream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(fStream);
            }
        }

        private string GetFolderName()
        {
            return Path.Combine("Resources", "Images");
        }


        private string GetRandPathPart()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTU";
            var random = new Random();
            int len = 10;
            return new string(Enumerable.Repeat(chars, len).Select(s => s[random.Next(s.Length)]).ToArray()) + ".jpg";
        }

        public async Task<bool> RemoveImage(string path)
        {
            bool retval = false;
            if (File.Exists(path))
            {
                File.Delete(path);
                retval = true;
            }
            return retval;

        }
    }
}
