using Bitir.Mobile.Models.Order;
using Bitir.Mobile.ViewModels;
using Module.Shared.Entities.SalesModuleEntities;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreOrderDetailPage
    {
        private readonly StoreOrderDetailViewModel storeOrderDetailViewModel;
        public StoreOrderDetailPage(StoreOrder storeOrder)
        {
            this.InitializeComponent();
            BindingContext = storeOrderDetailViewModel = new StoreOrderDetailViewModel(storeOrder);
        }
    }
}