using Bitir.Mobile.Exeptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    public class ProfilePageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfileViewModel" /> class
        /// </summary>
        public ProfilePageViewModel()
        {
            this.EditCommand = new Command(this.EditButtonClicked);
            this.ProfileNameCommand = new Command(this.ProfileNameClicked);
            this.PasswordCommand = new Command(this.PasswordButtonClicked);
            this.EmailCommand = new Command(this.EmailButtonClicked);
            this.PhoneCommand = new Command(this.PhoneButtonClicked);
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            Task.Run(async () => await LoadProfile());
            MessagingCenter.Subscribe<ProfilePopupViewModel, bool>(this, "ReLoadAccountDetails", (s, e) =>
               {
                   if (e)
                   {
                       MainThread.BeginInvokeOnMainThread(async () =>
                       {
                           await LoadProfile();
                       });
                   }
               });
        }

        #endregion

        #region Command

        public Command EditCommand { get; set; }

        public Command ProfileNameCommand { get; set; }

        public Command EmailCommand { get; set; }

        public Command PhoneCommand { get; set; }

        public Command PasswordCommand { get; set; }

        public Command BackButtonCommand { get; set; }

        #endregion

        #region Property
        public ProfileResponse ProfileResponse
        {
            get
            {
                return this.profileResponse;
            }

            set
            {
                if (this.profileResponse == value)
                {
                    return;
                }

                this.SetProperty(ref this.profileResponse, value);
            }
        }
        #endregion

        #region Field
        private ProfileResponse profileResponse;
        #endregion

        #region Methods
        private void EditButtonClicked(object obj)
        {
            // Do something
        }

        private async Task LoadProfile()
        {
            IsBusy = true;
            try
            {
                var accountDataFromClaims = GetClaims();
                if (accountDataFromClaims != null && accountDataFromClaims.Id > 0)
                {
                    var accountDetail = await authService.GetAccountByIdAsync(accountDataFromClaims.Id);
                    ProfileResponse = accountDetail.Result;
                }
            }
            catch (ServiceException ex)
            {
                SendNotification(new ExceptionTransfer { ex = ex, NotificationMessage = "Hesap bilgileri bulunamadı" });
            }
            finally
            {
                IsBusy = false;
            }
        }


        private async void ProfileNameClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer($"{GetClaims().Name} {GetClaims().Surname}", "Ad Soyad", "Name")));
        }

        private async void EmailButtonClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer(GetClaims().Email, "Email", "Email")));
        }

        private async void PhoneButtonClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer(GetClaims().Phone, "Telefon", "Phone")));

        }

        private async void PasswordButtonClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer("", "Şifre", "Password", true)));

        }

        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        #endregion
    }
}
