using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreProductListPopupView
    {
        public StoreProductListPopupView()
        {
            InitializeComponent();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}