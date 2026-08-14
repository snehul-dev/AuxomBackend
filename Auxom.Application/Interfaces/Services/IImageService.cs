using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile image)
    }
}
