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
            Task.Run(async () => await GetProvince());
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
        private ValidatableObject<string> plate;
        private ObservableCollection<Province> provinces;
        private ObservableCollection<District> districts;
        private ObservableCollection<Neighbourhood> neighbourhoods;
        private object capacity;
        private Province selectedProvince;
        private District selectedDistrict;
        private Neighbourhood selectedNeighbourhood;
        private int provinceId;
        private int districtId;
        private int neighbourhoodId;
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
            this.SelectedProvince = new Province();
            this.SelectedDistrict = new District();
            IsBusy = false;
        }

        public bool AreFieldsValid()
        {
            bool isPlate = this.Plate.Validate();
            return isPlate;
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
                        Capacity = int.Parse(this.Capacity.ToString())
                    });

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
