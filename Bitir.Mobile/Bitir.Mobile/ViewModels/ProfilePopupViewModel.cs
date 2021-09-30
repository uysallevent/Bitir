using Bitir.Mobile.Exeptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Rg.Plugins.Popup.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ProfilePopupViewModel : BaseViewModel
    {
        #region Constructor
        public ProfilePopupViewModel()
        {
            InitializeProperties();
            this.AddValidationRules();
            this.ProfileUpdateCommand = new Command(async () => await this.UpdateProfileClicked());
        }
        #endregion

        private void InitializeProperties()
        {
            this.passwordCompare = false;
            this.UpdateData = new ValidatableObject<string>();
        }

        #region Validation


        private void AddValidationRules()
        {
            this.UpdateData.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = $"Alan Zorunludur!" });
        }
        #endregion

        #region Command
        public Command ProfileUpdateCommand { get; set; }

        #endregion


        #region Property
        public ValidatableObject<string> UpdateData
        {
            get
            {
                return this.updateData;
            }

            set
            {
                if (this.updateData == value)
                {
                    return;
                }

                this.SetProperty(ref this.updateData, value);
            }
        }
        public string PropertyName
        {
            get
            {
                return this.propertyname;
            }

            set
            {
                if (this.propertyname == value)
                {
                    return;
                }

                this.SetProperty(ref this.propertyname, value);
            }
        }
        public bool PasswordCompare
        {
            get
            {
                return this.passwordCompare;
            }

            set
            {
                if (this.passwordCompare == value)
                {
                    return;
                }

                this.SetProperty(ref this.passwordCompare, value);
            }
        }
        #endregion

        #region Field
        public ValidatableObject<string> updateData;
        public string propertyname;
        public bool passwordCompare;
        #endregion

        #region Method
        private async Task UpdateProfileClicked()
        {
            IsBusy = true;
            try
            {
                if (!string.IsNullOrEmpty(updateData.Value) && !string.IsNullOrEmpty(propertyname) && UpdateData.Validate())
                {
                    var result = await authService.UpdateAccountAsync(new AuthRegisterRequest
                    {
                        Id = GetClaims().Id,
                        Name = (PropertyName == "Name") ? (updateData.Value.Contains(" ") ? updateData.Value.Split(' ')[0] : updateData.Value) : null,
                        Surname = (PropertyName == "Name") ? (updateData.Value.Contains(" ") ? updateData.Value.Split(' ')[1] : null) : null,
                        Email = (PropertyName == "Email") ? updateData.Value : null,
                        Phone = (PropertyName == "Phone") ? updateData.Value : null,
                        Username = (PropertyName == "Username") ? updateData.Value : null,
                        Password = (PropertyName == "Password") ? updateData.Value : null
                    });
                    if (result != null)
                    {
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Hesap bilgileri güncellendi" });
                        await PopupNavigation.Instance.PopAsync();
                        MessagingCenter.Send(this, "ReLoadAccountDetails", true);
                    }
                }

            }
            catch (ServiceException ex)
            {
                SendNotification(new ExceptionTransfer { ex = ex, NotificationMessage = "Hesap bilgisi güncellenemedi" });
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

    }
}
