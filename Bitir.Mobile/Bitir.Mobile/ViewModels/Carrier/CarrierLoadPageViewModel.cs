using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Module.Shared.Entities.ProductModuleEntities;
using SalesModule.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitir.Mobile.ViewModels
{
    public class CarrierLoadPageViewModel : CarrierListPageViewModel
    {
        #region Constructurer
        public CarrierLoadPageViewModel()
        {
            Task.Run(async () => await GetStoreProducts());
        }

        #endregion

        #region Properties
        public ValidatableObject<StoreCarrier> SelectedCarrier
        {
            get
            {
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

        public ValidatableObject<StoreProductViewModel> SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                if (this._selectedProduct == value)
                {
                    return;
                }
                this.SetProperty(ref this._selectedProduct, value);
            }
        }

        public ObservableCollection<StoreProductViewModel> StoreProducts
        {
            get
            {
                return _storeProducts;
            }
            set
            {
                if (this._storeProducts == value)
                {
                    return;
                }
                this.SetProperty(ref this._storeProducts, value);
            }
        }
        #endregion

        #region Fields
        private ValidatableObject<StoreCarrier> _selectedCarrier;
        private ValidatableObject<StoreProductViewModel> _selectedProduct;
        private ObservableCollection<StoreProductViewModel> _storeProducts;
        #endregion

        #region Commands

        #endregion

        #region Methods
        private void Initialize()
        {
            this.SelectedCarrier = new ValidatableObject<StoreCarrier>();
            this.SelectedProduct = new ValidatableObject<StoreProductViewModel>();
        }

        private void AddValidationRules()
        {
            this.SelectedCarrier.Validations.Add(new IsNotNullOrEmptyRule<StoreCarrier> { ValidationMessage = "Lütfen bir araç seçin" });
        }

        private bool AreFieldsValid()
        {
            bool isCarrierSelect = SelectedCarrier.Validate();
            return isCarrierSelect;
        }

        private async Task GetStoreProducts()
        {
            IsBusy = true;
            try
            {
                var result = await productService.GetStoreProducts();
                if (result != null && result.List.Any())
                {
                    StoreProducts = new ObservableCollection<StoreProductViewModel>(result.List);
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
