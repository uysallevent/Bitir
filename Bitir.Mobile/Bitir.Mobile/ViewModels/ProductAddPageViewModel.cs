using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using ProductModule.Dtos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ProductAddPageViewModel : BaseViewModel
    {
        #region Constructor

        public ProductAddPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.SubmitCommand = new Command(async () => await SubmitClicked());
            Task.Run(async () => await GetSystemProducts());
        }

        #endregion

        #region Properties
        public ObservableCollection<SystemProductResponse> SystemProducts
        {
            get
            {
                return this._systemProducts;
            }

            set
            {
                if (this._systemProducts == value)
                {
                    return;
                }

                this.SetProperty(ref this._systemProducts, value);
            }
        }

        public ValidatableObject<SystemProductResponse> SelectedProduct
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

        public ValidatableObject<object> Id
        {
            get
            {
                return this._id;
            }

            set
            {
                if (this._id == value)
                {
                    return;
                }

                this.SetProperty(ref this._id, value);
            }
        }

        public ValidatableObject<object> Price
        {
            get
            {
                return this._price;
            }

            set
            {
                if (this._price == value)
                {
                    return;
                }

                this.SetProperty(ref this._price, value);
            }
        }

        public ValidatableObject<object> Quantity
        {
            get
            {
                return this._quantity;
            }

            set
            {
                if (this._quantity == value)
                {
                    return;
                }

                this.SetProperty(ref this._quantity, value);
            }
        }

        #endregion

        #region Fields
        private ObservableCollection<SystemProductResponse> _systemProducts;
        private ValidatableObject<object> _id;
        private ValidatableObject<object> _price;
        private ValidatableObject<object> _quantity;
        private ValidatableObject<SystemProductResponse> _selectedProduct;
        #endregion

        #region Comments
        public Command SubmitCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        private void InitializeProperties()
        {
            this.Id = new ValidatableObject<object>();
            this.Price = new ValidatableObject<object>();
            this.Quantity = new ValidatableObject<object>();
            this.SelectedProduct = new ValidatableObject<SystemProductResponse>();
        }

        private void AddValidationRules()
        {
            this.Id.Validations.Add(new IsNotNullOrEmptyRule<object> { ValidationMessage = "Lütfen bir ürün seçin" });
            this.Price.Validations.Add(new IsNotNullOrEmptyRule<object> { ValidationMessage = "Lütfen fiyat bilgisi girin" });
            this.Quantity.Validations.Add(new IsNotNullOrEmptyRule<object> { ValidationMessage = "Lütfen ürün stok bilgisi girin" });
            this.SelectedProduct.Validations.Add(new IsNotNullOrEmptyRule<SystemProductResponse> { ValidationMessage = "Lütfen bir ürün seçin" });
        }

        private bool AreFieldsValid()
        {
            bool isPrice = Price.Validate();
            bool isProductSelect = SelectedProduct.Validate();
            bool isQuantity = Quantity.Validate();
            return isPrice && isQuantity && isProductSelect;
        }

        private async Task SubmitClicked()
        {
            if (this.AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var result = await productService.AddProductToStore(
                        new AddProductToStoreRequest
                        {
                            ProductQuantityId = SelectedProduct.Value.Id,
                            Price = decimal.Parse(this.Price.Value.ToString()),
                            Quantity = int.Parse(this.Quantity.Value.ToString())
                        });
                    if (result != null)
                    {
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Ürün başarı ile eklendi" });
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
        }

        private async Task GetSystemProducts()
        {
            IsBusy = true;
            try
            {
                var result = await productService.GetSystemProducts();
                SystemProducts = new ObservableCollection<SystemProductResponse>(result.List);
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

        private async Task BackButtonClicked()
        {
            _ = await App.Current.MainPage.Navigation.PopModalAsync(true);
        }


        #endregion
    }
}