using Bitir.Mobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class CarrierListPageViewModel : BaseViewModel
    {

        #region Constructure
        public CarrierListPageViewModel()
        {
            this.GotoNewCarrierAddPageCommand = new Command(async () => await GotoNewCarrierAddPageClicked());
        }
        #endregion

        #region Commands

        public Command GotoNewCarrierAddPageCommand { get; set; }

        #endregion

        #region Methods

        public async Task GotoNewCarrierAddPageClicked()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new CarrierAddPage());
        }

        #endregion

    }
}
