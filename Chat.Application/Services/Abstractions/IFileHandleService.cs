using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services.Abstractions
{
    public interface IFileHandleService
    {
        Task<string> SaveAsync(IFormFile data);
        Task<byte[]> LoadAsync(string path);
    }
}
