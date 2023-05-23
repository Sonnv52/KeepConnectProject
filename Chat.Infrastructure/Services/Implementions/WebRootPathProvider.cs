using Chat.Application.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
namespace Chat.Infrastructure.Services.Implementions
{
    public class WebRootPathProvider : IWebRootPathProvider
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WebRootPathProvider(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetWebRootPath()
        {
            return _hostingEnvironment.WebRootPath;
        }
    }
}
