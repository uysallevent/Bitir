using Bitir.Mobile.Models;
using Bitir.Mobile.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePopupView
    {
        ProfilePopupViewModel profilePopupViewModel;
        public ProfilePopupView(PopupDataTransfer popupDataTransfer)
        {
            InitializeComponent();
            BindingContext = profilePopupViewModel = new ProfilePopupViewModel();
            profilePopupViewModel.PropertyName = popupDataTransfer.PropertyName;
            GenericEntry.Text = popupDataTransfer.Value;
            GenericEntry.IsPassword = popupDataTransfer.IsPassword;
            GenericEntry.Placeholder = popupDataTransfer.PlaceHolder;
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}