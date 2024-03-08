using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta_Carros
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null; // Handle null values gracefully if needed
            }

            BsonBinaryData imageData = (BsonBinaryData)value;
            byte[] imageBytes = imageData.Bytes;
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // This is not necessary for binding images in this scenario
            throw new NotImplementedException();
        }
    }
}
