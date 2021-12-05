using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
using ProductModule.Dtos;
using SalesModule.Dtos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        public StoreCarriersView StoreCarriersView
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

        public ObservableCollection<StoreCarriersView> StoreCarrierDistZones
        {
            get
            {
                return this.storeCarrierDistZones;
            }

            set
            {
                if (this.storeCarrierDistZones == value)
                {
                    return;
                }

                this.SetProperty(ref this.storeCarrierDistZones, value);
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

        public IList<object> SelectedNeighbourhoods
        {
            get
            {
                return selectedNeighbourhoods;
            }
            set
            {
                if (this.selectedNeighbourhoods == value)
                {
                    return;
                }
                this.SetProperty(ref this.selectedNeighbourhoods, value);
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

        public int? ProvinceId
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

        public int? DistrictId
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

        public int? NeighbourhoodId
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
        private ObservableCollection<StoreCarriersView> storeCarrierDistZones;
        private Province selectedProvince;
        private District selectedDistrict;
        private Neighbourhood selectedNeighbourhood;
        private IList<object> selectedNeighbourhoods;
        private int? provinceId;
        private int? districtId;
        private int? neighbourhoodId;
        private object capacity;
        private bool status;
        private string driverName;
        private Command<object> removeCarrierZoneButtonCommand;
        #endregion

        #region Constructor

        public CarrierSettingsPageViewModel(StoreCarriersView storeCarriersView)
        {
            InitializeProperties();
            this.AddValidationRules();
            Task.Run(async () => await GetStoreCarrier(storeCarriersView.CarrierId));
            Task.Run(async () => await GetProvince());
            SubmitButtonCommand = new Command(async () => await SubmitButtonClicked());
            AddCarrierZoneButtonCommand = new Command(async () => await AddCarrierZoneClicked());
            RemoveCarrierButtonCommand = new Command(async () => await RemoveButtonClicked());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.StoreCarriersView = storeCarriersView;
            this.Plate.Value = storeCarriersView.Plate;
            this.DriverName = storeCarriersView.DriverName;
            this.capacity = storeCarriersView.Capacity;
        }

        #endregion

        #region Command

        public Command SubmitButtonCommand { get; set; }
        public Command RemoveCarrierButtonCommand { get; set; }
        public Command AddCarrierZoneButtonCommand { get; set; }
        public Command<object> RemoveCarrierZoneButtonCommand
        {
            get
            {
                return this.removeCarrierZoneButtonCommand ?? (this.removeCarrierZoneButtonCommand = new Command<object>(async (s) => await this.RemoveCarrierZoneClicked(s)));
            }
        }
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods

        private void InitializeProperties()
        {
            this.plate = new ValidatableObject<string>();
            this.SelectedProvince = new Province();
            this.SelectedDistrict = new District();
            this.SelectedNeighbourhoods = new ObservableCollection<object>();
            this.StoreCarrierDistZones = new ObservableCollection<StoreCarriersView>();
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
                        CarrierDistributionZoneId = StoreCarriersView.CarrierDistributionZoneId,
                        CarrierId = StoreCarriersView.CarrierId,
                        CarrierStoreId = StoreCarriersView.CarrierStoreId ?? 0,
                        Capacity = int.Parse(Capacity.ToString()),
                        Plate = Plate.Value,
                        DriverName = DriverName,
                        Status = Core.Enums.Status.Active,
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
                        CarrierId = StoreCarriersView.CarrierId,
                        CarrierStoreId = StoreCarriersView.CarrierStoreId ?? 0,
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

        private async Task RemoveCarrierZoneClicked(object zone)
        {
            try
            {
                IsBusy = true;

                var question = await App.Current.MainPage.DisplayAlert("Uyarı", "Bölgeyi silmek istediğinize eminmisiniz ?", "Evet", "Hayır");
                if (!question)
                {
                    return;
                }

                var item = zone as StoreCarriersView;
                var result = await carrierService.RemoveZoneFromCarrierById(item.CarrierDistributionZoneId ?? 0);
                if (result != null && result.Result)
                {
                    StoreCarrierDistZones.Remove(item);
                    SendNotification(new ExceptionTransfer { NotificationMessage = "Bölge başarı ile silindi" });
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

        private async Task AddCarrierZoneClicked()
        {
            try
            {
                IsBusy = true;

                var result = await carrierService.AddDistributionZoneToCarrier(new CarrierZoneRequest
                {
                    CarrierId = StoreCarriersView.CarrierId,
                    ProvinceId = SelectedProvince.Id,
                    DistrictId = SelectedDistrict.Id,
                    LocalityNames = Neighbourhoods.Select(y => y.LocalityName).ToList(),
                    Status = Core.Enums.Status.Active
                });

                if (result != null && result.Result)
                {
                    await GetStoreCarrier(StoreCarriersView.CarrierId);
                    SendNotification(new ExceptionTransfer { NotificationMessage = "Bölge başarı ile eklendi" });
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

        public async Task GetProvince()
        {
            try
            {
                IsBusy = true;
                var result = await provinceService.GetProvince(new Province { });
                Provinces = new ObservableCollection<Province>(result.List.OrderBy(x => x.Name));
                SelectedProvince = Provinces.FirstOrDefault(x => x.Id == this.StoreCarriersView.ProvinceId);

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

        public async Task GetDistrict(int? provinceId)
        {
            if (provinceId == null || provinceId == 0)
                return;
            try
            {
                IsBusy = true;
                var result = await districtService.GetDistrict(new District { ProvinceId = provinceId ?? 0 });
                Districts = new ObservableCollection<District>(result.List.OrderBy(x => x.Name));
                SelectedDistrict = Districts.FirstOrDefault(x => x.Id == this.StoreCarriersView.DistrictId);
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

        public async Task GetNeighbourhood(int? districtId)
        {
            try
            {
                IsBusy = true;
                var result = await neighbourhoodService.GetNeighbourhood(new Neighbourhood { DistrictId = districtId ?? 0 });
                Neighbourhoods = new ObservableCollection<Neighbourhood>(
                    result
                    .List
                    .GroupBy(x => x.LocalityName)
                    .OrderBy(x => x.Key)
                    .Select(x => new Neighbourhood { LocalityName = x.Key }));

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

        public async Task GetStoreCarrier(int carrierId)
        {
            try
            {
                IsBusy = true;
                var result = await carrierService.GetStoreCarrierById(carrierId);
                StoreCarrierDistZones = new ObservableCollection<StoreCarriersView>(result.List);
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
