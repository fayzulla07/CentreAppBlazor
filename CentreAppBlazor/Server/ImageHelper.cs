using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CentreAppBlazor.Server.Extensions
{
    public static class ImageHelper
    {
        public enum ImageSize
        {
            Normal,
            Small
        }
        public static byte[] GetReducedImage(int width, int height, byte[] image)
        {
            try
            {
                var photo = ToImage(image);
                var thumb = photo.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                return ImageToByteArray(thumb);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }
        public static Image ToImage(byte[] image)
        {
            using (var ms = new MemoryStream(image))
            {
                return Image.FromStream(ms);
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
