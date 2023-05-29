using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Helper.Extentions
{
    public static class ReadFileExtention
    {
        public static byte[]? ReadFile(this string filePath)
        {
            var imagePath = Path.Combine(filePath);

            if (System.IO.File.Exists(imagePath))
            {
                return System.IO.File.ReadAllBytes(imagePath);
            }

            return new byte[0];
        }
    }
}
