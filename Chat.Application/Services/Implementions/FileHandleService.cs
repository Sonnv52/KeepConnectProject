using Chat.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Services.Implementions
{
    public class FileHandleService : IFileHandleService
    {
        private readonly IWebRootPathProvider _webRootPathProvider;

        public FileHandleService(IWebRootPathProvider webRootPathProvider)
        {
            _webRootPathProvider = webRootPathProvider;
        }

        public Task<byte[]> LoadAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveAsync(IFormFile data)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(data.FileName)}";
            string webRootPath = _webRootPathProvider.GetWebRootPath();
            string imagePath = Path.Combine(webRootPath, "Chat.Infrastructure\\wwroot\\Images\\Avatars",
                imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {

                try
                {
                   await data.CopyToAsync(stream);
                }
                catch(Exception e)
                {
                    throw new FileNotFoundException(e.ToString());
                }
            }

            return imagePath;
        }
    }
}
