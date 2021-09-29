    using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Services;
using Bitir.Mobile.Services.Interfaces;
using Bitir.Mobile.ViewModels;
using Bitir.Mobile.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "Montserrat-Bold")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
[assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace Bitir.Mobile
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA1NzQ5QDMxMzkyZTMyMmUzMFBtdldHWFdrWS90K3o1azl6V3hpRmdSTnlIVFpHYzRsSkZjMEZDNXB2REE9");
            InitializeComponent();
            DependencyService.Register<AuthService>();
            MessagingCenter.Subscribe<BaseViewModel, ExceptionTransfer>(this, "infomessage", (s, e) =>
           {
               if (!string.IsNullOrEmpty(e.NotificationMessage))
               {
                   MainThread.BeginInvokeOnMainThread(async() =>
                   {
                      await App.Current.MainPage.DisplayAlert("Bilgi", e.NotificationMessage, "Tamam");
                   });
               }
           });

            MainPage = new LoginPage();
        }

        public static AuthResponse authResponse;

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
