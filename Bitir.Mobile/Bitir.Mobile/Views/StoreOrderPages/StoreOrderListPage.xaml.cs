using Bitir.Mobile.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    /// <summary>
    /// Page to show article bookmark items
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreOrderListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreOrderListPage" /> class.
        /// </summary>
        public StoreOrderListPage()
        {
            this.InitializeComponent();
            this.BindingContext = StoreOrderListViewModel.BindingContext;
        }
    }
}