using Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Bitir.Mobile.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (Status)value;
            switch (status)
            {
                case Status.Pasive:
                    return Color.FromHex("#ffd9d9");
                case Status.Active:
                default:
                    return Color.Default;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
