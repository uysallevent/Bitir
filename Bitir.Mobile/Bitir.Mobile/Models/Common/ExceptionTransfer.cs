using System;
using Xamarin.Forms;

namespace Bitir.Mobile.Models.Common
{
    public class ExceptionTransfer
    {
        public Page page { get; set; }
        public Exception ex { get; set; }
        public string NotificationMessage { get; set; }
    }
}
