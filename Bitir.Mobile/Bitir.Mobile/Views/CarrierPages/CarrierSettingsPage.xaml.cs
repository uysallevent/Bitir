using Bitir.Mobile.ViewModels;
using SalesModule.Dtos;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarrierSettingsPage
    {
        private readonly CarrierSettingsPageViewModel productSettingsPageViewModel;
        public CarrierSettingsPage(StoreCarrier storeCarrier)
        {
            this.InitializeComponent();
            BindingContext = productSettingsPageViewModel = new CarrierSettingsPageViewModel(storeCarrier);
        }
    }
}