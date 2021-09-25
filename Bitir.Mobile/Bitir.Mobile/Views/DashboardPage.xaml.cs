using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    /// <summary>
    /// Page to show the health care details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardPage" /> class.
        /// </summary>
        public DashboardPage()
        {
            this.InitializeComponent();
        }
    }
}
