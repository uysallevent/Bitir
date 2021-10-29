using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Core.Enums;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    /// <summary>
    /// ViewModel for profile page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProductSettingsPageViewModel : BaseViewModel
    {
        #region Fields
        private StoreProductViewModel storeProductViewModel;

        private IList<StoreProductViewModel> storeProductStock;

        private string profileImage;

        private string productName;

        private bool status;

        private ValidatableObject<object> _price;

        private ValidatableObject<object> _quantity;

        #endregion

        #region Constructor

        public ProductSettingsPageViewModel(List<StoreProductViewModel> storeProductStocks)
        {
            InitializeProperties();
            this.AddValidationRules();
            SubmitButtonCommand = new Command(async () => await SubmitButtonClicked());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            RemoveButtonCommand = new Command(async () => await RemoveButtonClicked());
            StoreProductStock = new ObservableCollection<StoreProductViewModel>(storeProductStocks);
            this.StoreProductViewModel = storeProductStocks.FirstOrDefault();
            Status = this.storeProductViewModel.Status != Core.Enums.Status.Pasive;
            ProductName = this.storeProductViewModel.FullName;
            Price.Value = this.storeProductViewModel.Price;
            Quantity.Value = storeProductStocks.FirstOrDefault(x => x.Position == "DEPO")?.Stock ?? 0;
        }

        #endregion

        #region  Properties

        public StoreProductViewModel StoreProductViewModel
        {
            get
            {
                return this.storeProductViewModel;
            }

            set
            {
                if (this.storeProductViewModel != value)
                {
                    this.SetProperty(ref this.storeProductViewModel, value);
                }
            }
        }

        public IList<StoreProductViewModel> StoreProductStock
        {
            get
            {
                return this.storeProductStock;
            }
            set
            {
                if (this.storeProductStock != value)
                {
                    this.SetProperty(ref this.storeProductStock, value);
                }
            }
        }

        public string ProfileImage
        {
            get
            {
                return App.ImageServerPath + this.profileImage;
            }

            set
            {
                if (this.profileImage != value)
                {
                    this.SetProperty(ref this.profileImage, value);
                }
            }
        }

        public string ProductName
        {
            get
            {
                return this.productName;
            }

            set
            {
                if (this.productName != value)
                {
                    this.SetProperty(ref this.productName, value);
                }
            }
        }

        public bool Status
        {
            get
            {
                return this.status;
            }

            set
            {
                if (this.status != value)
                {
                    this.SetProperty(ref this.status, value);
                }
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

        #region Command

        public Command SubmitButtonCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        public Command RemoveButtonCommand { get; set; }

        #endregion

        #region Methods
        private void InitializeProperties()
        {
            this.Price = new ValidatableObject<object>();
            this.Quantity = new ValidatableObject<object>();
        }

        private void AddValidationRules()
        {
            this.Price.Validations.Add(new IsNotNullOrEmptyRule<object> { ValidationMessage = "Lütfen fiyat bilgisi girin" });
            this.Quantity.Validations.Add(new IsNotNullOrEmptyRule<object> { ValidationMessage = "Lütfen ürün stok bilgisi girin" });
        }

        private bool AreFieldsValid()
        {
            bool isPrice = Price.Validate();
            bool isQuantity = Quantity.Validate();
            return isPrice && isQuantity;
        }

        private async Task BackButtonClicked()
        {
            await App.Current.MainPage.Navigation.PopModalAsync(true);
        }

        private async Task SubmitButtonClicked()
        {
            if (this.AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var result = await productService.StoreProductUpdate(new UpdateProductStoreRequest
                    {
                        Price = decimal.Parse(this.Price.Value.ToString()),
                        Quantity = int.Parse(this.Quantity.Value.ToString()),
                        ProductPriceId = StoreProductViewModel.ProductPriceId,
                        ProductQuantityId = StoreProductViewModel.ProductPriceId,
                        ProductStockId = StoreProductViewModel.ProductStockId,
                        ProductStoreId = StoreProductViewModel.ProductStoreId,
                        Status = this.status ? Core.Enums.Status.Active : Core.Enums.Status.Pasive
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
        }

        private async Task RemoveButtonClicked()
        {
            IsBusy = true;
            try
            {
                var question = await App.Current.MainPage.DisplayAlert("Uyarı", "Ürünü silmek istediğinize eminmisiniz ?", "Evet", "Hayır");
                if (!question)
                {
                    return;
                }

                var result = await productService.StoreProductRemoveFromStore(storeProductViewModel.ProductStoreId);

                if (result != null && result.Result)
                {
                    SendNotification(new ExceptionTransfer { NotificationMessage = "Ürün başarı ile silindi" });
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

        #endregion
    }
}
