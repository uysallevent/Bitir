using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using SalesModule.Dtos;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class CarrierLoadPageViewModel : CarrierListPageViewModel
    {
        #region Constructurer
        public CarrierLoadPageViewModel()
        {
            Initialize();
            Task.Run(async () => await GetStoreProductsByStore());
            SubmitCommand = new Command(async () => await SubmitButtonClicked());

        }

        #endregion

        #region Properties
        public ValidatableObject<StoreCarrier> SelectedCarrier
        {
            get
            {
                if (_selectedCarrier.Value != null && CarrierId != _selectedCarrier.Value.CarrierId)
                {
                    CarrierId = _selectedCarrier.Value.CarrierId;
                    Task.Run(async () => await GetStoreProductsByCarrier(CarrierId));
                }
                return _selectedCarrier;
            }
            set
            {
                if (this._selectedCarrier == value)
                {
                    return;
                }
                this.SetProperty(ref this._selectedCarrier, value);
            }
        }

        public ValidatableObject<StoreProdByCarrierResponse> SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                if (this._selectedProduct == value)
                {
                    return;
                }
                this.SetProperty(ref this._selectedProduct, value);
            }
        }

        public ObservableCollection<StoreProdByCarrierResponse> StoreProductsInCarrier
        {
            get
            {
                return _storeProductsInCarrier;
            }
            set
            {
                if (this._storeProductsInCarrier == value)
                {
                    return;
                }
                this.SetProperty(ref this._storeProductsInCarrier, value);
            }
        }

        public ObservableCollection<StoreProdByCarrierResponse> StoreProducts
        {
            get
            {
                return _storeProducts;
            }
            set
            {
                if (this._storeProducts == value)
                {
                    return;
                }
                this.SetProperty(ref this._storeProducts, value);
            }
        }

        public ValidatableObject<string> Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (this.quantity == value)
                {
                    return;
                }
                this.SetProperty(ref this.quantity, value);
            }
        }

        public int CarrierId
        {
            get
            {
                return carrierId;
            }
            set
            {
                if (this.carrierId == value)
                {
                    return;
                }
                this.SetProperty(ref this.carrierId, value);
            }
        }

        #endregion

        #region Fields
        private ValidatableObject<StoreCarrier> _selectedCarrier;
        private ValidatableObject<StoreProdByCarrierResponse> _selectedProduct;
        private ObservableCollection<StoreProdByCarrierResponse> _storeProductsInCarrier;
        private ObservableCollection<StoreProdByCarrierResponse> _storeProducts;
        private ValidatableObject<string> quantity;
        private int carrierId;
        private Command<object> removeProductFromCarrierCommand;
        #endregion

        #region Commands
        public Command SubmitCommand { get; set; }
        public Command<object> RemoveProductFromCarrierCommand
        {
            get
            {
                return this.removeProductFromCarrierCommand ?? (this.removeProductFromCarrierCommand = new Command<object>(async (s) => await this.RemoveProductClicked(s)));
            }
        }
        #endregion

        #region Methods
        private void Initialize()
        {
            Quantity = new ValidatableObject<string>();
            this.SelectedCarrier = new ValidatableObject<StoreCarrier>();
            this.SelectedProduct = new ValidatableObject<StoreProdByCarrierResponse>();
            AddValidationRules();
        }

        private void AddValidationRules()
        {
            this.SelectedCarrier.Validations.Add(new IsNotNullOrEmptyRule<StoreCarrier> { ValidationMessage = "Lütfen bir araç seçin" });
            this.SelectedProduct.Validations.Add(new IsNotNullOrEmptyRule<StoreProdByCarrierResponse> { ValidationMessage = "Lütfen bir ürün seçin" });
            this.Quantity.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Lütfen bir miktar bilgisi girin" });
            this.Quantity.Validations.Add(new ZeroCheck<string> { ValidationMessage = "Lütfen 0'dan farklı bir değer girin" });
        }

        private bool AreFieldsValid()
        {
            bool isCarrierSelect = SelectedCarrier.Validate();
            bool isProductSelect = SelectedProduct.Validate();
            bool isQuantity = Quantity.Validate();
            return isCarrierSelect && isProductSelect && isQuantity;
        }

        private async Task GetStoreProductsByCarrier(int carrierId)
        {
            IsBusy = true;
            try
            {
                var result = await productService.GetStoreProductsByCarrier(new StoreProdByCarrierRequest
                {
                    CarrierId = carrierId
                });
                StoreProductsInCarrier = null;
                if (result != null && result.List.Any())
                {
                    StoreProductsInCarrier = new ObservableCollection<StoreProdByCarrierResponse>(result.List);
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

        private async Task GetStoreProductsByStore()
        {
            IsBusy = true;
            try
            {
                var result = await productService.getStoreProductsByStore();
                StoreProductsInCarrier = null;
                if (result != null && result.List.Any())
                {
                    StoreProducts = new ObservableCollection<StoreProdByCarrierResponse>(result.List);
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

        private async Task RemoveProductClicked(object product)
        {
            var item = product as StoreProdByCarrierResponse;

            IsBusy = true;
            try
            {
                var result = await productService.StoreProductRemoveFromCarrier(new UpdateProductStoreRequest
                {
                    Status = Core.Enums.Status.Deleted,
                    ProductStockId = item.ProductStockId,
                    ProductStoreId = item.ProductStoreId,
                    Quantity = item.ProductStock,
                    CarrierId = this.CarrierId
                });

                if (result.Result)
                {
                    SendNotification(new ExceptionTransfer { NotificationMessage = "Ürün bilgileri başarı ile güncellendi" });
                    await App.Current.MainPage.Navigation.PopModalAsync(true);
                    MessagingCenter.Send(this, "UpdateProductList", true);
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

        private async Task SubmitButtonClicked()
        {
            if (this.AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var result = await productService.AddOrUpdateProductInCarrier(new UpdateProductStoreRequest
                    {
                        CarrierId = this.CarrierId,
                        Quantity = int.Parse(this.Quantity.Value.ToString()),
                        ProductStoreId = this.SelectedProduct.Value.ProductStoreId
                    });

                    if (result.Result)
                    {
                        this.Quantity = new ValidatableObject<string>();
                        this.SelectedProduct = new ValidatableObject<StoreProdByCarrierResponse>();
                        await GetStoreProductsByStore();
                        await GetStoreProductsByCarrier(this.carrierId);
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Araçta ürün güncellemesi gerçekleşti" });
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
        }

        #endregion


    }
}
