using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ByWoggi.classes
{
    public static class ImageConverter
    {
        /// <summary>
        /// Конвертирует BitmapImage в массив байтов.
        /// </summary>
        /// <param name="image">Изображение для конвертации.</param>
        /// <returns>Массив байтов.</returns>
        public static byte[] ImageToByteArray(BitmapImage image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder(); // Используем PNG формат для кодирования
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Конвертирует массив байтов в BitmapImage.
        /// </summary>
        /// <param name="byteArray">Массив байтов для конвертации.</param>
        /// <returns>BitmapImage.</returns>
        public static BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                throw new ArgumentNullException(nameof(byteArray));

            var image = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                ms.Position = 0;
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze(); // Делаем изображение потоконезависимым
            }
            return image;
        }
    }
}
