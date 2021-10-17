using Bitir.Mobile.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductSettingsPage
    {
        private readonly ProductSettingsPageViewModel productSettingsPageViewModel;
        public ProductSettingsPage(Module.Shared.Entities.ProductModuleEntities.StoreProductViewModel storeProductViewModel)
        {
            this.InitializeComponent();
            BindingContext = productSettingsPageViewModel = new ProductSettingsPageViewModel(storeProductViewModel);
            //(BindingContext as ProductSettingsPageViewModel).StoreProductViewModel = storeProductViewModel;
        }
    }
}