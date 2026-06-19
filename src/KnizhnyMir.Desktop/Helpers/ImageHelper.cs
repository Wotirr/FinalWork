using System.IO;
using System.Windows.Media.Imaging;

namespace KnizhnyMir.Desktop.Helpers
{
    /// <summary>Помогает загружать изображения товаров с учётом относительных путей.</summary>
    public static class ImageHelper
    {
        private const string PlaceholderUri = "pack://application:,,,/Resources/picture.png";

        private static readonly string ProductsDirectory =
            Path.Combine(AppContext.BaseDirectory, "Resources", "Products");

        /// <summary>
        /// Возвращает изображение товара по имени файла. Если файл отсутствует,
        /// возвращается изображение-заглушка. Используются относительные пути,
        /// поэтому приложение работает и при перемещении папки.
        /// </summary>
        public static BitmapImage GetProductImage(string? photo)
        {
            if (!string.IsNullOrWhiteSpace(photo))
            {
                var path = Path.Combine(ProductsDirectory, photo);
                if (File.Exists(path))
                {
                    return Load(new Uri(path, UriKind.Absolute));
                }
            }

            return Load(new Uri(PlaceholderUri, UriKind.Absolute));
        }

        private static BitmapImage Load(Uri uri)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = uri;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
