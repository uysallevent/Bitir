using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.ViewModels;
using Module.Shared.Entities.ProductModuleEntities;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreProductListPopupView
    {
        private StoreProductListPopupViewModel storeProductListPopupViewModel;
        public StoreProductListPopupView(StoreProductViewModel storeProductViewModel)
        {
            InitializeComponent();
            BindingContext = storeProductListPopupViewModel = new StoreProductListPopupViewModel();
            (BindingContext as StoreProductListPopupViewModel).StoreProductViewModel = storeProductViewModel;
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}