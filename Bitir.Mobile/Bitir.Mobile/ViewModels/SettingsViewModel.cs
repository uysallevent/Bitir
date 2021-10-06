using Bitir.Mobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    /// <summary>
    /// Viewmodel of settings page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Bitir.Mobile.ViewModels.SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel()
        {
            this.ManageProductCommand = new Command(async () => await ManageProductButtonClicked());
            this.BackButtonCommand = new Command(async () => await BackButtonClicked());
        }

        public Command ManageProductCommand { get; set; }
        public Command BackButtonCommand { get; set; }


        private async Task ManageProductButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProductAddPage());
        }

        private async Task BackButtonClicked()
        {
            _ = await App.Current.MainPage.Navigation.PopAsync(true);
        }

        private void PrivacyPolicyTapped(object obj)
        {
            // Do something
        }
    }
}
