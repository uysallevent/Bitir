using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Models.Order;
using Bitir.Mobile.Validators;
using Module.Shared.Enums;
using SalesModule.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class StoreOrderDetailViewModel : BaseViewModel
    {
        #region Constructor
        public StoreOrderDetailViewModel(StoreOrder storeOrder)
        {
            Initialize();
            Task.Run(async () => await GetStoreCarriers());
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.StoreOrder = storeOrder;
            SelectedOrderStatus.Value = storeOrder.OrderStatus.ToString();

        }
        #endregion

        #region Fields
        private StoreOrder storeOrder;
        private ObservableCollection<StoreCarrier> carriers;
        private ObservableCollection<string> orderStatusList;
        private ValidatableObject<StoreCarrier> _selectedCarrier;
        private ValidatableObject<string> selectedOrderStatus;
        private int carrierId;

        #endregion

        #region Properties
        public StoreOrder StoreOrder
        {
            get
            {
                return this.storeOrder;
            }

            set
            {
                if (this.storeOrder == value)
                {
                    return;
                }

                this.SetProperty(ref this.storeOrder, value);
            }
        }

        public ObservableCollection<StoreCarrier> Carriers
        {
            get
            {
                return this.carriers;
            }

            set
            {
                if (this.carriers == value)
                {
                    return;
                }

                this.SetProperty(ref this.carriers, value);
            }
        }

        public ObservableCollection<string> OrderStatusList
        {
            get
            {
                return this.orderStatusList;
            }

            set
            {
                if (this.orderStatusList == value)
                {
                    return;
                }

                this.SetProperty(ref this.orderStatusList, value);
            }
        }

        public ValidatableObject<StoreCarrier> SelectedCarrier
        {
            get
            {
                if (_selectedCarrier.Value != null && CarrierId != _selectedCarrier.Value.CarrierId)
                {
                    CarrierId = _selectedCarrier.Value.CarrierId;
                }
                return _selectedCarrier;
            }
            set
            {
                if (this._selectedCarrier == value)
                {
                    return;
                }
                this.SetProperty(ref this._selectedCarrier, value);
            }
        }

        public ValidatableObject<string> SelectedOrderStatus
        {
            get
            {
                return selectedOrderStatus;
            }
            set
            {
                this.SetProperty(ref selectedOrderStatus, value);
            }
        }

        public int CarrierId
        {
            get
            {
                return carrierId;
            }
            set
            {
                if (this.carrierId == value)
                {
                    return;
                }
                this.SetProperty(ref this.carrierId, value);
            }
        }

        #endregion

        #region Commands
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        private void Initialize()
        {
            SelectedCarrier = new ValidatableObject<StoreCarrier>();
            Carriers = new ObservableCollection<StoreCarrier>();
            SelectedOrderStatus = new ValidatableObject<string>();
            OrderStatusList = new ObservableCollection<string>(Enum.GetNames(typeof(OrderStatusEnum)).Where(x => x != "CanceledByCustomer").Select(b => b.ToString()).ToList());
        }

        public async Task GetStoreCarriers()
        {
            try
            {
                IsBusy = true;
                var result = await carrierService.GetStoreCarriers();
                Carriers = new ObservableCollection<StoreCarrier>(result.List);
                SelectedCarrier = new ValidatableObject<StoreCarrier>();
                SelectedCarrier.Value = Carriers.FirstOrDefault(x => x.CarrierId == this.StoreOrder.CarrierId);

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
            await App.Current.MainPage.Navigation.PopModalAsync();
        }


        #endregion
    }
}
