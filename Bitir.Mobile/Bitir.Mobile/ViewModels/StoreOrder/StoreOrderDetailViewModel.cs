using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Models.Order;
using Bitir.Mobile.Views;
using Core.Entities;
using Module.Shared.Entities.SalesModuleEntities;
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
            BackButtonCommand = new Command(async () => await BackButtonClicked());
            this.StoreOrder = storeOrder;

        }
        #endregion

        #region Fields
        private StoreOrder storeOrder;
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

        #endregion

        #region Commands
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        private void Initialize()
        {

        }


        private async Task BackButtonClicked()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }


        #endregion
    }
}
