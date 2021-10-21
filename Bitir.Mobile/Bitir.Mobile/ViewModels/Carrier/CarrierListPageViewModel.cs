using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Views;
using SalesModule.Dtos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class CarrierListPageViewModel : BaseViewModel
    {

        #region Constructure
        public CarrierListPageViewModel()
        {
            MessagingCenter.Subscribe<CarrierAddPageViewModel, bool>(this, "UpdateCarrierList", async (s, b) =>
            {
                if (b)
                {
                    await GetStoreCarriers();
                }
            });
            this.GotoNewCarrierAddPageCommand = new Command(async () => await GotoNewCarrierAddPageClicked());
            Task.Run(async () => await GetStoreCarriers());
        }
        #endregion

        #region Commands

        public Command GotoNewCarrierAddPageCommand { get; set; }

        public Command<object> ItemSelectedCommand
        {
            get
            {
                return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command<object>(async (s) => await this.NavigateToNextPage(s)));
            }
        }
        #endregion

        #region Fields

        private ObservableCollection<StoreCarrier> carriers;
        private Command<object> itemSelectedCommand;

        #endregion

        #region Property

        public ObservableCollection<StoreCarrier> Carriers
        {
            get
            {
                return this.carriers;
            }

            set
            {
                if (this.carriers == value)
                {
                    return;
                }

                this.SetProperty(ref this.carriers, value);
            }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            Carriers = new ObservableCollection<StoreCarrier>();
        }

        public async Task GetStoreCarriers()
        {
            try
            {
                IsBusy = true;
                var result = await carrierService.GetStoreCarriers();
                Carriers = new ObservableCollection<StoreCarrier>(result.List);
            }
            catch (BadRequestException ex)
            {
                SendNotification(new ExceptionTransfer { ex = ex, NotificationMessage = ex.Message });

            }
            catch (InternalServerErrorException ex)
            {
                SendNotification(new ExceptionTransfer { ex = ex, NotificationMessage = "Servis hatası !!" });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task NavigateToNextPage(object selectedItem)
        {
            var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as StoreCarrier;
            if (item != null)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new CarrierSettingsPage(item));
            }
        }

        public async Task GotoNewCarrierAddPageClicked()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new CarrierAddPage());
        }

        #endregion

    }
}
