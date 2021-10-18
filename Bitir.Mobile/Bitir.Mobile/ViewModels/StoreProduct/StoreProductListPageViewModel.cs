﻿using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Views;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using Rg.Plugins.Popup.Services;
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

            MessagingCenter.Subscribe<CarrierSettingsPageViewModel, bool>(this, "UpdateProductList", async (s, b) =>
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
                    StoreProducts = new ObservableCollection<StoreProductViewModel>(result.List);
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
                App.Current.MainPage.Navigation.PushModalAsync(new ProductSettingsPage(item));
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