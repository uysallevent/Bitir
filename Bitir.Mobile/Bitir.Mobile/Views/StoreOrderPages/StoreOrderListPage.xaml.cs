
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    /// <summary>
    /// Page to show the Songs play list page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreOrderListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProductListPage" /> class.
        /// </summary>
        public StoreOrderListPage()
        {
            this.InitializeComponent();
        }
    }
}