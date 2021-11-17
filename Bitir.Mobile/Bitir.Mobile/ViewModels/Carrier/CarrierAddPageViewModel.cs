using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Module.Shared.Entities.AuthModuleEntities;
using SalesModule.Dtos;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class CarrierAddPageViewModel : BaseViewModel
    {
        #region Constructurer
        public CarrierAddPageViewModel()
        {
            InitializeProperties();
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            SubmitButtonCommand = new Command(async () => await SubmitButtonClicked());
        }
        #endregion

        #region Properties
        public ValidatableObject<string> Plate
        {
            get
            {
                return this.plate;
            }

            set
            {
                if (this.plate == value)
                {
                    return;
                }

                this.SetProperty(ref this.plate, value);
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

        public string DriverName
        {
            get
            {
                return driverName;
            }
            set
            {
                if (this.driverName == value)
                {
                    return;
                }
                this.SetProperty(ref this.driverName, value);
            }
        }
        #endregion

        #region Fields
        private ValidatableObject<string> plate;
        private object capacity;
        private string driverName;
        #endregion

        #region Commands

        public Command BackButtonCommand { get; set; }

        public Command SubmitButtonCommand { get; set; }

        #endregion

        #region Methods

        private void InitializeProperties()
        {
            this.Plate = new ValidatableObject<string>();
            IsBusy = false;
        }

        public bool AreFieldsValid()
        {
            bool isPlate = this.Plate.Validate();
            return isPlate;
        }

        public async Task SubmitButtonClicked()
        {
            if (AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var result = await carrierService.AddCarrierToStore(new AddCarrierToStoreRequest
                    {
                        Plate = this.Plate.Value.ToUpperInvariant(),
                        DriverName = this.DriverName,
                        Capacity = int.Parse(this.Capacity.ToString())
                    }); ;

                    if (result != null && result.Result)
                    {
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Araç başarı ile eklendi" });
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

        public async Task BackButtonClicked()
        {
            await App.Current.MainPage.Navigation.PopModalAsync(true);
        }

        #endregion
    }
}
