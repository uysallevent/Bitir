
using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Bitir.Mobile.Views;
using Module.Shared.Entities.ProductModuleEntities;
using Rg.Plugins.Popup.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class StoreProductListPopupViewModel : BaseViewModel
    {
        #region Constructor
        public StoreProductListPopupViewModel()
        {
            InitializeProperties();
            this.ProductSettingClickedCommand = new Command(async () => await ProductSettings());
        }
        #endregion

        private void InitializeProperties()
        {
            StoreProductViewModel = new StoreProductViewModel();
        }

        #region Command
        public Command ProductSettingClickedCommand { get; set; }

        #endregion

        #region Method
        private async Task ProductSettings()
        {
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushModalAsync(new ProductSettingsPage(StoreProductViewModel));
        }
        #endregion

        #region Property
        public StoreProductViewModel _storeProductViewModel;
        #endregion

        #region Field
        public StoreProductViewModel StoreProductViewModel
        {
            get
            {
                return this._storeProductViewModel;
            }
            set
            {
                if (this._storeProductViewModel != value)
                {
                    this.SetProperty(ref this._storeProductViewModel, value);
                }
            }
        }
        #endregion
    }
}
