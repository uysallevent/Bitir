using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Dtos;
using SalesModule.Dtos;
using System.Runtime.Serialization;
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
        private StoreCarrier storeCarrier;

        private ValidatableObject<string> plate;

        private object capacity;

        private bool status;
        #endregion

        #region Constructor

        public CarrierSettingsPageViewModel(StoreCarrier storeCarrier)
        {
            InitializeProperties();
            this.AddValidationRules();
            SubmitButtonCommand = new Command(async () => await SubmitButtonClicked());
            RemoveButtonCommand = new Command(async () => await RemoveButtonClicked());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.StoreCarrier = storeCarrier;
            this.Plate.Value = storeCarrier.Plate;
            this.capacity = storeCarrier.Capacity;
        }

        #endregion

        #region Public properties

        public StoreCarrier StoreCarrier
        {
            get
            {
                return this.storeCarrier;
            }

            set
            {
                if (this.storeCarrier != value)
                {
                    this.SetProperty(ref this.storeCarrier, value);
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
        public Command RemoveButtonCommand { get; set; }
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods

        private void InitializeProperties()
        {
            this.plate = new ValidatableObject<string>();
        }

        private void AddValidationRules()
        {
            this.plate.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Lütfen plaka bilgisi girin" });
        }

        private bool AreFieldsValid()
        {
            bool isPlate = plate.Validate();
            return isPlate;
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
                    var result = await carrierService.UpdateStoreCarrier(new UpdateCarrierToStoreRequest
                    {
                        CarrierId = StoreCarrier.CarrierId,
                        CarrierStoreId = StoreCarrier.CarrierStoreId,
                        Capacity = int.Parse(this.Capacity.ToString()),
                        Plate = this.Plate.Value,
                        Status = StoreCarrier.Status
                    });

                    if (result != null && result.Result)
                    {
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Araç başarı ile güncellendi" });
                        await App.Current.MainPage.Navigation.PopModalAsync(true);
                        MessagingCenter.Send(this, "UpdateCarrierList", true);
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
            if (this.AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var question =await App.Current.MainPage.DisplayAlert("Uyarı","Aracı silmek istediğinize eminmisiniz ?","Evet","Hayır");
                    if (!question)
                    {
                        return;
                    }

                    var result = await carrierService.UpdateStoreCarrier(new UpdateCarrierToStoreRequest
                    {
                        CarrierId = StoreCarrier.CarrierId,
                        CarrierStoreId = StoreCarrier.CarrierStoreId,
                        Status = Core.Enums.Status.Deleted
                    });

                    if (result != null && result.Result)
                    {
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Araç başarı ile silindi" });
                        await App.Current.MainPage.Navigation.PopModalAsync(true);
                        MessagingCenter.Send(this, "UpdateCarrierList", true);
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
