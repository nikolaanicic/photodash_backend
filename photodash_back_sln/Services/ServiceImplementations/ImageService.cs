﻿using Contracts.Services.ImagesService;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ServiceImplementations
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImage(string base64Image)
        {
            var imagePath = MakeFullPath();
            await Save(base64Image, imagePath);
            return imagePath;
        }


        private async Task Save(string image,string path)
        {
            var imgBytes = Convert.FromBase64String(image);

            using (MemoryStream imgStream = new MemoryStream(imgBytes))
            {
                using(FileStream fStream = new FileStream(path,FileMode.Create))
                {
                    await imgStream.CopyToAsync(fStream);
                }
            }
        }


        private string GetBasePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Resources", "Images"));
        }

        private string GetRandPathPart()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTU";
            var random = new Random();
            int len = 10;
            return new string(Enumerable.Repeat(chars, len).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string MakeFullPath()
        {
            return Path.Combine(GetBasePath(), GetRandPathPart()) + ".jpg";
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
