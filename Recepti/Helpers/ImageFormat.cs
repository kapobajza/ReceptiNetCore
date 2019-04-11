using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recepti.Helpers
{
    public class ImageFormat
    {
        public static string GetImageFormat(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            var bmp = Encoding.ASCII.GetBytes("BM");
            var png = new byte[] { 137, 80, 78, 71 };
            var tiff = new byte[] { 73, 73, 42 };
            var tiff2 = new byte[] { 77, 77, 42 };
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpeg2 = new byte[] { 255, 216, 255, 225 };

            if (bmp.SequenceEqual(fileBytes.Take(bmp.Length)))
            {
                return "bmp";
            }

            if (png.SequenceEqual(fileBytes.Take(png.Length)))
            {
                return "png";
            }

            if (tiff.SequenceEqual(fileBytes.Take(tiff.Length)))
            {
                return "tiff";
            }

            if (tiff2.SequenceEqual(fileBytes.Take(tiff2.Length)))
            {
                return "tiff2";
            }

            if (jpeg.SequenceEqual(fileBytes.Take(jpeg.Length)))
            {
                return "jpeg";
            }

            if (jpeg2.SequenceEqual(fileBytes.Take(jpeg2.Length)))
            {
                return "jpeg2";
            }

            return null;
        }
    }
}
