using Bitir.Mobile.ViewModels;
using Module.Shared.Entities.ProductModuleEntities;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductSettingsPage
    {
        private readonly ProductSettingsPageViewModel productSettingsPageViewModel;
        public ProductSettingsPage(StoreProductViewModel storeProductViewModel)
        {
            this.InitializeComponent();
            BindingContext = productSettingsPageViewModel = new ProductSettingsPageViewModel(storeProductViewModel);
        }
    }
}