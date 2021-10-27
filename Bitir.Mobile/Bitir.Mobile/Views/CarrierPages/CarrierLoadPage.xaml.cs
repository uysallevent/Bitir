using Bitir.Mobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarrierLoadPage : ContentPage
    {
        public CarrierLoadPage()
        {
            this.InitializeComponent();
        }

        private void ProductQuantityEntry_Focused(object sender, FocusEventArgs e)
        {
            var val = ((BorderlessEntry)sender).Text;
            if (val == "0")
            {
                ((BorderlessEntry)sender).Text = string.Empty;
            }
        }
    }
}