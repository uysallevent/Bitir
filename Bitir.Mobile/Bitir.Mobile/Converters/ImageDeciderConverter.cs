using Core.Enums;
using Module.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Bitir.Mobile.Converters
{
    public class ImageDeciderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (OrderStatusEnum)value;
            switch (status)
            {
                case OrderStatusEnum.completed:
                    return "order_completed";
                case OrderStatusEnum.inCarrier:
                    return "order_in_progress";
                case OrderStatusEnum.canceledByCustomer:
                    return "order_cancelled";
                case OrderStatusEnum.canceledByStore:
                    return "order_cancelled";
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
