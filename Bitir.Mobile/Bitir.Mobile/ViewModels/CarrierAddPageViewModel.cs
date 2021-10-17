using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using SalesModule.Dtos;
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
            SubmitButtonCommand = new Command(async () => await SubmitButtobClicked());
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
        #endregion

        #region Fields
        private ValidatableObject<string> plate;
        private object capacity;
        #endregion

        #region Commands

        public Command BackButtonCommand { get; set; }

        public Command SubmitButtonCommand { get; set; }

        #endregion

        #region Methods

        private void InitializeProperties()
        {
            this.Plate = new ValidatableObject<string>();
        }

        public bool AreFieldsValid()
        {
            bool isPlate = this.Plate.Validate();
            return isPlate;
        }

        public async Task SubmitButtobClicked()
        {
            if (AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var result =await carrierService.AddCarrierToStore(new AddCarrierToStoreRequest
                    {
                        Plate=this.Plate.Value,
                        Capacity=int.Parse(this.Capacity.ToString())
                    });

                    if(result!=null && result.Result)
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
