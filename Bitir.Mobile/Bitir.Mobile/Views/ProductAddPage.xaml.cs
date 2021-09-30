using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    /// <summary>
    /// Page to add business details such as name, email address, and phone number.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductAddPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAddPage" /> class.
        /// </summary>
        public ProductAddPage()
        {
            this.InitializeComponent();
        }
    }
}