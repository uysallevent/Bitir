using Core.Enums;
using Module.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Bitir.Mobile.Converters
{
    public class OrderStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (OrderStatusEnum)value;
            switch (status)
            {
                case OrderStatusEnum.InCarrier:
                    return Color.FromHex("#fffdbc");
                case OrderStatusEnum.Completed:
                    return Color.FromHex("#bcffd7");
                default:
                    return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
