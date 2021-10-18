using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Core.Enums;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
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
    public class CarrierSettingsPageViewModel : BaseViewModel
    {
        #region Fields
        private StoreProductViewModel storeProductViewModel;

        private string profileImage;

        private ValidatableObject<string> plate;

        private object capacity;

        private bool status;
        #endregion

        #region Constructor

        public CarrierSettingsPageViewModel(StoreProductViewModel storeProductViewModel)
        {
            InitializeProperties();
            this.AddValidationRules();
            SubmitButtonCommand = new Command(async () => await SubmitButtonClicked());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.StoreProductViewModel = storeProductViewModel;
            Status = storeProductViewModel.Status != Core.Enums.Status.Pasive;
            ProductName = storeProductViewModel.FullName;
            Price.Value = storeProductViewModel.Price;
            Quantity.Value = storeProductViewModel.Stock;
        }

        #endregion

        #region Public properties

        private void InitializeProperties()
        {
            this.plate = new ValidatableObject<string>();
        }

        private void AddValidationRules()
        {
            this.plate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Lütfen plaka  bilgisi girin" });
        }

        private bool AreFieldsValid()
        {
            bool isPlate = plate.Validate();
            return isPlate;
        }

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

        public ValidatableObject<string> Plate
        {
            get
            {
                return this.plate;
            }

            set
            {
                if (this.plate != value)
                {
                    this.SetProperty(ref this.plate, value);
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

        public object Capacity
        {
            get
            {
                return this.capacity;
            }

            set
            {
                if (this.capacity == value)
                {
                    return;
                }

                this.SetProperty(ref this.capacity, value);
            }
        }
        #endregion

        #region Command

        public Command SubmitButtonCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods

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
                        Status=this.status?Core.Enums.Status.Active:Core.Enums.Status.Pasive
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


        #endregion
    }
}
