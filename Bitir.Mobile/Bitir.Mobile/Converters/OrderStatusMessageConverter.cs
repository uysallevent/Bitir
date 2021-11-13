using Module.Shared.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Bitir.Mobile.Converters
{
    public class OrderStatusMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (OrderStatusEnum)value;
            switch (status)
            {
                case OrderStatusEnum.InCarrier:
                    return "Sipariş Taşıma Durumunda";
                case OrderStatusEnum.Completed:
                    return "Sipariş Tamamlandı";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
