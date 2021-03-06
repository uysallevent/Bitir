using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Views;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class StoreProductListPageViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemSelectedCommand;
        private Command<object> menuCommand;
        private ObservableCollection<StoreProductViewModel> storeProducts;
        private IList<IGrouping<int, StoreProductViewModel>> productStockList;

        #endregion

        #region Constructor
        public StoreProductListPageViewModel()
        {
            Task.Run(async () => await GetStoreProducts());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            MessagingCenter.Subscribe<ProductAddPageViewModel, bool>(this, "UpdateProductList", async (s, b) =>
             {
                 if (b)
                 {
                     await GetStoreProducts();
                 }
             });

            MessagingCenter.Subscribe<ProductSettingsPageViewModel, bool>(this, "UpdateProductList", async (s, b) =>
            {
                if (b)
                {
                    await GetStoreProducts();
                }
            });
        }
        #endregion

        #region Properties
        public Command BackButtonCommand { get; set; }

        public Command<object> ItemSelectedCommand
        {
            get
            {
                return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        public Command<object> AddProductCommand
        {
            get
            {
                return this.menuCommand ?? (this.menuCommand = new Command<object>(this.AddButtonClicked));
            }
        }

        public ObservableCollection<StoreProductViewModel> StoreProducts
        {
            get
            {
                return this.storeProducts;
            }

            set
            {
                if (this.storeProducts == value)
                {
                    return;
                }

                this.SetProperty(ref this.storeProducts, value);
            }
        }

        public IList<IGrouping<int, StoreProductViewModel>> ProductStockList
        {
            get
            {
                return this.productStockList;
            }
            set
            {
                if (this.productStockList == value)
                {
                    return;
                }
                this.SetProperty(ref this.productStockList, value);
            }
        }

        #endregion

        #region Methods
        private async Task GetStoreProducts()
        {
            IsBusy = true;
            try
            {
                var result = await productService.GetStoreProducts();
                if (result != null && result.List.Any())
                {
                    ProductStockList = result.List.GroupBy(x => x.ProductStoreId).ToList();
                    var groupByStoreProdId = ProductStockList.Select(x => new StoreProductViewModel
                    {
                        ProductStoreId = x.Key,
                        Abbreviation = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Abbreviation,
                        Id = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Id,
                        Name = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Name,
                        Position = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Position,
                        Price = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Price,
                        ProductPriceId = x.FirstOrDefault(y => y.ProductStoreId == x.Key).ProductPriceId,
                        ProductStockId = x.FirstOrDefault(y => y.ProductStoreId == x.Key).ProductStockId,
                        Quantity = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Quantity,
                        Status = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Status,
                        Stock = x.Where(y => y.ProductStoreId == x.Key).Sum(y => y.Stock),
                        Unit = x.FirstOrDefault(y => y.ProductStoreId == x.Key).Unit
                    });
                    StoreProducts = new ObservableCollection<StoreProductViewModel>(groupByStoreProdId);
                }
                else
                {
                    StoreProducts = new ObservableCollection<StoreProductViewModel>();
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

        private void NavigateToNextPage(object selectedItem)
        {
            var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as StoreProductViewModel;
            if (item != null)
            {
                var productStock = ProductStockList.FirstOrDefault(x => x.Key == item.ProductStoreId).ToList();
                App.Current.MainPage.Navigation.PushModalAsync(new ProductSettingsPage(productStock));
            }
        }

        private void AddButtonClicked(object selectedItem)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new ProductAddPage());
        }

        public async Task BackButtonClicked()
        {
            await App.Current.MainPage.Navigation.PopModalAsync(true);
        }

        #endregion
    }
}
