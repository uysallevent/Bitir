using Module.Shared.Entities.ProductModuleEntities;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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
    public class ProductSettingsPageViewModel : BaseViewModel
    {
        #region Fields

        private static ProductSettingsPageViewModel profileViewModel;

        private StoreProductViewModel storeProductViewModel;
        private string profileImage;

        private string productName;

        private bool status;

        private Command editCommand;

        private Command nightModeCommand;

        private Command textSizeCommand;

        private Command settingsCommand;

        #endregion

        #region Constructor

        public ProductSettingsPageViewModel()
        {
            StoreProductViewModel = new StoreProductViewModel();
        }

        #endregion

        #region Public properties

        public StoreProductViewModel StoreProductViewModel
        {
            get
            {
                return this.storeProductViewModel;
            }

            set
            {
                if (this.storeProductViewModel != value)
                {
                    this.SetProperty(ref this.storeProductViewModel, value);
                    Status = this.storeProductViewModel.Status == Core.Enums.Status.Active;

                }
            }
        }


        [DataMember(Name = "itemImage")]
        public string ProfileImage
        {
            get
            {
                return App.ImageServerPath + this.profileImage;
            }

            set
            {
                if (this.profileImage != value)
                {
                    this.SetProperty(ref this.profileImage, value);
                }
            }
        }

        public string ProductName
        {
            get
            {
                return this.productName;
            }

            set
            {
                if (this.productName != value)
                {
                    this.SetProperty(ref this.productName, value);
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

        #endregion

        #region Command

        /// <summary>
        /// Gets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command BackButtonCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new Command(async () => await this.BackButtonClicked()));
            }
        }
        #endregion

        #region Methods

        private async Task BackButtonClicked()
        {
          await  App.Current.MainPage.Navigation.PopModalAsync(true);
        }


        #endregion
    }
}
