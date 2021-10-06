using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Models.Product;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ProductAddPageViewModel : LoginViewModel
    {
        #region Constructor

        public ProductAddPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.SubmitCommand = new Command(this.SubmitClicked);
            Task.Run(async () => await GetSystemProducts());
        }

        #endregion

        #region Properties
        public IList<ProductResponse> SystemProducts
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

        public ValidatableObject<int?> Id
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

        public ValidatableObject<decimal?> Quantity
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

        public ValidatableObject<decimal?> Price
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
        #endregion

        #region Fields
        private IList<ProductResponse> _systemProducts;
        private ValidatableObject<int?> _id;
        private ValidatableObject<decimal?> _quantity;
        private ValidatableObject<decimal?> _price;
        #endregion

        #region Comments
        public Command SubmitCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        private void InitializeProperties()
        {
            this.Id = new ValidatableObject<int?>();
            this.Quantity = new ValidatableObject<decimal?>();
            this.Price = new ValidatableObject<decimal?>();
        }

        private void AddValidationRules()
        {
            this.Id.Validations.Add(new IsNotNullOrEmptyRule<int?> { ValidationMessage = "Lütfen bir ürün seçin" });
            this.Quantity.Validations.Add(new IsNotNullOrEmptyRule<decimal?> { ValidationMessage = "Lütfen miktar bilgisi girin" });
            this.Price.Validations.Add(new IsNotNullOrEmptyRule<decimal?> { ValidationMessage = "Lütfen fiyat bilgisi girin" });

        }

        private bool AreFieldsValid()   
        {
            bool isId = this.Id.Validate();
            bool isQuantity = this.Quantity.Validate();
            bool isPrice = this.Price.Validate();
            return isId && isQuantity && isPrice;
        }

        private void SubmitClicked(object obj)
        {
            if (this.AreFieldsValid())
            {

            }
        }

        private async Task GetSystemProducts()
        {
            IsBusy = true;
            try
            {
                var result = await productService.GetSystemProducts();
                SystemProducts = new ObservableCollection<ProductResponse>(result.List);
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