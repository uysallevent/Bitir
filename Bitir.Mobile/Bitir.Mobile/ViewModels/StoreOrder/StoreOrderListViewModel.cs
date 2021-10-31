using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Module.Shared.Entities.SalesModuleEntities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class StoreOrderListViewModel : BaseViewModel
    {
        #region Constructor
        public StoreOrderListViewModel()
        {
            Task.Run(async () => await GetStoreOrders());
            LoadMoreCommand = new Command(async () => await LoadMore());

        }
        #endregion

        #region Fields
        private ObservableCollection<StoreOrderViewModel> storeOrders;
        private Command<object> itemSelectedCommand;
        #endregion

        #region Properties
        public ObservableCollection<StoreOrderViewModel> StoreOrders
        {
            get
            {
                return this.storeOrders;
            }

            set
            {
                if (this.storeOrders == value)
                {
                    return;
                }

                this.SetProperty(ref this.storeOrders, value);
            }
        }
        public Command<object> ItemSelectedCommand
        {
            get
            {
                return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        public Command LoadMoreCommand { get; set; }
        #endregion

        #region Commands
        public Command GetStoreOrdersCommand { get; set; }
        #endregion

        #region Methods
        public async Task GetStoreOrders()
        {
            try
            {
                IsBusy = true;
                var result = await orderService.GetStoreOrders(new Core.Entities.PagingRequestEntity<StoreOrderViewModel> { Page = 1, PageSize = 10 });
                StoreOrders = new ObservableCollection<StoreOrderViewModel>(result.List);
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

        private async Task LoadMore()
        {

        }

        private void NavigateToNextPage(object selectedItem)
        {
            var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as StoreOrderViewModel;
            if (item != null)
            {

            }
        }

        #endregion
    }
}
