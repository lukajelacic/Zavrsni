using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;

namespace eMusicStudio.Web.Helper
{
    public static class Image
    {
        private static System.Drawing.Image Resize(System.Drawing.Image img, int iWidth, int iHeight)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics graphic = Graphics.FromImage((System.Drawing.Image)bmp);
            graphic.DrawImage(img, 0, 0, iWidth, iHeight);

            return (System.Drawing.Image)bmp;
        }
        public static string Upload(IFormFile image, IWebHostEnvironment webhost)
        {

            string uniqueFileName = null;
            if (image != null)
            {
                //using (var memoryStream = new MemoryStream())
                //{
                //    image.CopyToAsync(memoryStream);
                //    using (var img = System.Drawing.Image.FromStream(memoryStream))
                //    {
                //        // TODO: ResizeImage(img, 100, 100);
                //        slika = Resize(img, 150, 150);
                //    }
                //}
                
                string uploadsFolder = Path.Combine(webhost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public static void Delete(IWebHostEnvironment webhost, string folder, string fileName)
        {
            string fullPath = Path.Combine(webhost.WebRootPath, "images", folder, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
