using Chat.Application.Services.Abstractions;

namespace Chat.Infrastructure.Services.Implementions
{
    public class WebRootPathProvider : IWebRootPathProvider
    {
        public WebRootPathProvider()
        {
        }

        public string GetWebRootPath()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string projectRootPath = Path.GetFullPath(Path.Combine(baseDirectory, "..\\..\\..\\..\\"));

            return projectRootPath;
        }
    }
}
