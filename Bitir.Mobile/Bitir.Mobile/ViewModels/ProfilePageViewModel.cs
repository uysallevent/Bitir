using Bitir.Mobile.Models;
using Bitir.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
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

        #region Methods
        private void EditButtonClicked(object obj)
        {
            // Do something
        }

        private async void ProfileNameClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer("", "Ad Soyad")));

        }

        private async void EmailButtonClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer("", "Email")));
        }

        private async void PhoneButtonClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer("", "Telefon")));

        }

        private async void PasswordButtonClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as Grid).BackgroundColor = (Color)retVal;
            await Task.Delay(100).ConfigureAwait(true);
            (obj as Grid).BackgroundColor = Color.Transparent;
            await PopupNavigation.Instance.PushAsync(new ProfilePopupView(new PopupDataTransfer("", "Şifre",true)));

        }

        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        #endregion
    }
}
