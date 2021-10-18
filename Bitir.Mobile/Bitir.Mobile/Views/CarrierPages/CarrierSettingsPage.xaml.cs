using Bitir.Mobile.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarrierSettingsPage
    {
        private readonly CarrierSettingsPageViewModel productSettingsPageViewModel;
        public CarrierSettingsPage(Module.Shared.Entities.ProductModuleEntities.StoreProductViewModel storeProductViewModel)
        {
            this.InitializeComponent();
            BindingContext = productSettingsPageViewModel = new ProductSettingsPageViewModel(storeProductViewModel);
            //(BindingContext as ProductSettingsPageViewModel).StoreProductViewModel = storeProductViewModel;
        }
    }
}