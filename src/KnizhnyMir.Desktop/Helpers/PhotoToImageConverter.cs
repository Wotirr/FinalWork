using System.Globalization;
using System.Windows.Data;

namespace KnizhnyMir.Desktop.Helpers
{
    /// <summary>Преобразует имя файла фотографии товара в изображение для отображения.</summary>
    public class PhotoToImageConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            ImageHelper.GetProductImage(value as string);

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
