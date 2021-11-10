using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Models.Order;
using Bitir.Mobile.Views;
using Core.Entities;
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
            Initialize();
            Task.Run(async () => await GetStoreOrders());
            LoadMoreCommand = new Command(async () => await LoadMore());

        }
        #endregion

        #region Fields
        private ObservableCollection<StoreOrder> storeOrders;
        private Command<object> itemSelectedCommand;
        private int page;
        private bool hasNextPage;
        #endregion

        #region Properties
        public ObservableCollection<StoreOrder> StoreOrders
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

        public int Page
        {
            get
            {
                return this.page;
            }

            set
            {
                if (this.page == value)
                {
                    return;
                }

                this.SetProperty(ref this.page, value);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return this.hasNextPage;
            }

            set
            {
                if (this.hasNextPage == value)
                {
                    return;
                }

                this.SetProperty(ref this.hasNextPage, value);
            }
        }
        #endregion

        #region Commands
        public Command GetStoreOrdersCommand { get; set; }
        #endregion

        #region Methods
        private void Initialize()
        {
            Page = 1;
        }

        public async Task GetStoreOrders(int? page = null)
        {
            try
            {
                IsBusy = true;
                var result = await orderService.GetStoreOrders(new PagingRequestEntity<StoreOrdersView> { Page = Page, PageSize = 10 });
                if (result == null || !result.List.Any())
                {
                    return;
                }

                var groupByOrderId = result
                    .List
                    .GroupBy(x => x.OrderId)
                    .Select(x => new StoreOrder
                    {
                        OrderId = x.Key,
                        AddressId = result.List.FirstOrDefault(y => y.OrderId == x.Key).UserAddressId,
                        UserId = result.List.FirstOrDefault(y => y.OrderId == x.Key).UserId,
                        CarrierId= result.List.FirstOrDefault(y => y.OrderId == x.Key).CarrierId,
                        CustomerName = result.List.FirstOrDefault(y => y.OrderId == x.Key).CustomerName,
                        OrderAddress = result.List.FirstOrDefault(y => y.OrderId == x.Key).OrderAddress,
                        OrderDistrictName = result.List.FirstOrDefault(y => y.OrderId == x.Key).OrderDistrictName,
                        OrderNote = result.List.FirstOrDefault(y => y.OrderId == x.Key).OrderNote,
                        OrderProvinceName = result.List.FirstOrDefault(y => y.OrderId == x.Key).OrderProvinceName,
                        OrderStatus = result.List.FirstOrDefault(y => y.OrderId == x.Key).OrderStatus,
                        OrderDate= result.List.FirstOrDefault(y => y.OrderId == x.Key).OrderDate,
                        StoreOrderDetails = result.List.Where(y => y.OrderId == x.Key).Select(y => new StoreOrderDetail
                        {
                            OrderQuantity = y.OrderQuantity,
                            ProductName = y.ProductName,
                            ProductQuantity = y.ProductQuantity,
                            ProductStoreId = y.ProductStoreId,
                            ProductUnit = y.ProductUnit,
                            ProductUnitAbbreviation = y.ProductUnitAbbreviation
                        }).ToList()
                    });

                HasNextPage = result.HasNextpage;
                if (StoreOrders != null && StoreOrders.Any())
                {
                    foreach (var item in groupByOrderId)
                    {
                        StoreOrders.Add(item);
                    }
                }
                else
                {
                    StoreOrders = new ObservableCollection<StoreOrder>(groupByOrderId);
                }
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
            if (HasNextPage)
            {
                await GetStoreOrders(Page++);
            }
        }

        private void NavigateToNextPage(object selectedItem)
        {
            var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as StoreOrder;
            if (item != null)
            {
                App.Current.MainPage.Navigation.PushModalAsync(new StoreOrderDetailPage(item));
            }
        }

        #endregion
    }
}
