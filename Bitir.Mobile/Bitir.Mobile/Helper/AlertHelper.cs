using System;
using Xamarin.Forms;

namespace Bitir.Mobile.Helper
{
    public static class AlertHelper
    {
        public static void DisplayAlert(string message)
        {
            Device.BeginInvokeOnMainThread(() => App.Current.MainPage.DisplayAlert("Uyarı", message, "Tamam"));
        }

        public static void DisplayAlertYesNo(string question, Action YesMetod)
        {
            Device.BeginInvokeOnMainThread(async () => {
                bool que = await App.Current.MainPage.DisplayAlert("Uyarı", question, "Evet", "Hayır");
                if (que)
                    YesMetod();
            });
        }

    }
}
