using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
using ProductModule.Dtos;
using SalesModule.Dtos;
using System.Collections.ObjectModel;
using System.Linq;
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
        #region Properties

        public StoreCarriersView StoreCarrier
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

        public ObservableCollection<Province> Provinces
        {
            get
            {
                return this.provinces;
            }

            set
            {
                if (this.provinces == value)
                {
                    return;
                }

                this.SetProperty(ref this.provinces, value);
            }
        }

        public ObservableCollection<District> Districts
        {
            get
            {
                return this.districts;
            }

            set
            {
                if (this.districts == value)
                {
                    return;
                }

                this.SetProperty(ref this.districts, value);
            }
        }

        public ObservableCollection<Neighbourhood> Neighbourhoods
        {
            get
            {
                return this.neighbourhoods;
            }

            set
            {
                if (this.neighbourhoods == value)
                {
                    return;
                }

                this.SetProperty(ref this.neighbourhoods, value);
            }
        }

        public Province SelectedProvince
        {
            get
            {
                if (selectedProvince != null && ProvinceId != selectedProvince.Id)
                {
                    ProvinceId = selectedProvince.Id;
                    Task.Run(async () => await GetDistrict(ProvinceId));
                    Districts?.Clear();
                    Neighbourhoods?.Clear();
                }
                return selectedProvince;
            }
            set
            {
                if (this.selectedProvince == value)
                {
                    return;
                }
                this.SetProperty(ref this.selectedProvince, value);
            }
        }

        public District SelectedDistrict
        {
            get
            {
                if (selectedDistrict != null && DistrictId != selectedDistrict.Id)
                {
                    DistrictId = selectedDistrict.Id;
                    Task.Run(async () => await GetNeighbourhood(DistrictId));
                    Neighbourhoods?.Clear();
                }
                return selectedDistrict;
            }
            set
            {
                if (this.selectedDistrict == value)
                {
                    return;
                }
                this.SetProperty(ref this.selectedDistrict, value);
            }
        }

        public Neighbourhood SelectedNeighbourhood
        {
            get
            {
                if (selectedNeighbourhood != null && neighbourhoodId != selectedNeighbourhood.Id)
                {
                    NeighbourhoodId = selectedNeighbourhood.Id;
                }
                return selectedNeighbourhood;
            }
            set
            {
                if (this.selectedNeighbourhood == value)
                {
                    return;
                }
                this.SetProperty(ref this.selectedNeighbourhood, value);
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

        public int ProvinceId
        {
            get
            {
                return provinceId;
            }
            set
            {
                if (this.provinceId == value)
                {
                    return;
                }
                this.SetProperty(ref this.provinceId, value);
            }
        }

        public int DistrictId
        {
            get
            {
                return districtId;
            }
            set
            {
                if (this.districtId == value)
                {
                    return;
                }
                this.SetProperty(ref this.districtId, value);
            }
        }

        public int NeighbourhoodId
        {
            get
            {
                return neighbourhoodId;
            }
            set
            {
                if (this.neighbourhoodId == value)
                {
                    return;
                }
                this.SetProperty(ref this.neighbourhoodId, value);
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
        private StoreCarriersView storeCarrier;
        private ValidatableObject<string> plate;
        private ObservableCollection<Province> provinces;
        private ObservableCollection<District> districts;
        private ObservableCollection<Neighbourhood> neighbourhoods;
        private Province selectedProvince;
        private District selectedDistrict;
        private Neighbourhood selectedNeighbourhood;
        private int provinceId;
        private int districtId;
        private int neighbourhoodId;
        private object capacity;
        private bool status;
        private string driverName;
        #endregion

        #region Constructor

        public CarrierSettingsPageViewModel(StoreCarriersView storeCarriersView)
        {
            InitializeProperties();
            this.AddValidationRules();
            Task.Run(async () => await GetProvince());
            SubmitButtonCommand = new Command(async () => await SubmitButtonClicked());
            RemoveButtonCommand = new Command(async () => await RemoveButtonClicked());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.StoreCarrier = storeCarrier;
            this.Plate.Value = storeCarrier.Plate;
            this.capacity = storeCarrier.Capacity;
            this.ProvinceId = storeCarriersView.ProvinceId ?? -1;
            this.DistrictId = storeCarriersView.DistrictId ?? -1;
            this.DistrictId = storeCarriersView.DistrictId ?? -1;
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
            this.SelectedProvince = new Province();
            this.SelectedDistrict = new District();
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
                        DriverName = this.DriverName,
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
                    var question = await App.Current.MainPage.DisplayAlert("Uyarı", "Aracı silmek istediğinize eminmisiniz ?", "Evet", "Hayır");
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

        public async Task GetProvince()
        {
            try
            {
                IsBusy = true;
                var result = await provinceService.GetProvince(new Province { });
                Provinces = new ObservableCollection<Province>(result.List.OrderBy(x => x.Name));
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

        public async Task GetDistrict(int provinceId)
        {
            try
            {
                IsBusy = true;
                var result = await districtService.GetDistrict(new District { ProvinceId = provinceId });
                Districts = new ObservableCollection<District>(result.List.OrderBy(x => x.Name));

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

        public async Task GetNeighbourhood(int districtId)
        {
            try
            {
                IsBusy = true;
                var result = await neighbourhoodService.GetNeighbourhood(new Neighbourhood { DistrictId = districtId });
                Neighbourhoods = new ObservableCollection<Neighbourhood>(result.List.GroupBy(x => x.LocalityName).OrderBy(x => x.Key).Select(x => new Neighbourhood { LocalityName = x.Key }));

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
