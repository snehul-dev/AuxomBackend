using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(
            Stream imageStream,
            string fileName,
            string contentType,
            string folder
            );
    }
}
