using Bitir.Mobile.Exeptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
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
            this.ProfileUpdateCommand = new Command(async () => await this.UpdateProfileClicked());
        }
        #endregion


        #region Command
        public Command ProfileUpdateCommand { get; set; }

        #endregion


        #region Property
        public string UpdateData
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
        #endregion

        #region Field
        public string updateData;
        public string propertyname;
        #endregion

        #region Method
        private async Task UpdateProfileClicked()
        {
            IsBusy = true;
            try
            {
                if (!string.IsNullOrEmpty(updateData) && !string.IsNullOrEmpty(propertyname))
                {
                    var result =await authService.UpdateAccountAsync(new AuthRegisterRequest
                    {
                        Id = GetClaims().Id,
                        Name = (PropertyName == "Name") ? (updateData.Contains(" ") ? updateData.Split(' ')[0] : updateData) : null,
                        Surname = (PropertyName == "Name") ? (updateData.Contains(" ") ? updateData.Split(' ')[1] : null) : null,
                        Email = (PropertyName == "Email") ? updateData : null,
                        Phone = (PropertyName == "Phone") ? updateData : null,
                        Username = (PropertyName == "Email") ? updateData : null
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
