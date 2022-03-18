using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services.ImagesService
{
    public interface IImageService
    {
        Task<string> SaveImage(IFormFile base64Image);
        Task<bool> RemoveImage(string path);
    }
}
