using Module.Shared.Entities.SalesModuleEntities;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreOrderDetailPage
    {
        public StoreOrderDetailPage(StoreOrderViewModel storeOrderViewModel)
        {
            this.InitializeComponent();
        }
    }
}