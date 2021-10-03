
using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Bitir.Mobile.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private ValidatableObject<string> name;

        private ValidatablePair<string> password;

        private ValidatableObject<bool> customer;

        private ValidatableObject<bool> vendor;

        private IList<AccountTypeResponse> accountTypes;
        private bool signUpButtonStatus;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.InitializeProperties();
            this.AddValidationRules();
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(async (obj) => await SignUpClicked(obj));
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            Task.Run(async () => await GetAccountTypes());
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public ValidatableObject<string> Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.SetProperty(ref this.name, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public ValidatablePair<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }

        public ValidatableObject<bool> Customer
        {
            get
            {
                return this.customer;
            }

            set
            {
                if (this.customer == value)
                {
                    return;
                }

                this.SetProperty(ref this.customer, value);
            }
        }

        public ValidatableObject<bool> Vendor
        {
            get
            {
                return this.vendor;
            }

            set
            {
                if (this.vendor == value)
                {
                    return;
                }

                this.SetProperty(ref this.vendor, value);
            }
        }

        public IList<AccountTypeResponse> AccountTypes
        {
            get
            {
                return this.accountTypes;
            }

            set
            {
                if (this.vendor == value)
                {
                    return;
                }

                this.SetProperty(ref this.accountTypes, value);
            }
        }

        public bool SignUpButtonStatus
        {
            get
            {
                return this.signUpButtonStatus;
            }

            set
            {
                if (this.signUpButtonStatus == value)
                {
                    return;
                }

                this.SetProperty(ref this.signUpButtonStatus, value);
            }
        }
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize whether fieldsvalue are true or false.
        /// </summary>
        /// <returns>true or false </returns>
        public bool AreFieldsValid()
        {
            bool isEmail = this.Email.Validate();
            bool isNameValid = this.Name.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isPasswordValid && isNameValid && isEmail;
        }

        /// <summary>
        /// Initializing the properties.
        /// </summary>
        private void InitializeProperties()
        {
            this.Name = new ValidatableObject<string>();
            this.Password = new ValidatablePair<string>();
            this.Customer = new ValidatableObject<bool>();
            this.Vendor = new ValidatableObject<bool>();
            this.customer.Value = true;
            this.vendor.Value = false;
            this.signUpButtonStatus = true;
        }

        /// <summary>
        /// this method contains the validation rules
        /// </summary>
        private void AddValidationRules()
        {
            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Ad Soyad Zorunludur!" });
            this.Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email Zorunludur!" });
            this.Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Şifre Zorunludur!" });
            this.Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Şifre Tekrarı Zorunludur!" });
        }

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage(), true);
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async Task SignUpClicked(object obj)
        {
            if (this.AreFieldsValid())
            {
                IsBusy = true;
                try
                {
                    var accountType = (this.Customer.Value) ? this.AccountTypes.FirstOrDefault(x => x.Name == "Customer").Id
                                           : this.AccountTypes.FirstOrDefault(x => x.Name == "Vendor").Id;

                    var result = await authService.RegisterAsync(new AuthRegisterRequest
                    {
                        Id = 0,
                        Name = (this.Name.Value.Contains(' ')) ? this.Name.Value.Split(' ')[0] : this.Name.Value,
                        Surname = (this.Name.Value.Contains(' ')) ? this.Name.Value.Split(' ')[1] : string.Empty,
                        Username = this.Email.Value,
                        Email = this.Email.Value,
                        Password = this.Password.Item1.Value,
                        AccountTypeId = accountType
                    });
                    if (result != null && !string.IsNullOrEmpty(result.Result.Token))
                    {
                        App.authResponse = result.Result;
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        Application.Current.MainPage = new NavigationPage(new DashboardPage());
                        SendNotification(new ExceptionTransfer { NotificationMessage = "Hesap Oluşturma Başarılı" });
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
        }

        private async Task GetAccountTypes()
        {
            IsBusy = true;
            try
            {
                var result = await authService.GetAccoutTypesAsync();
                if (result != null && result.List.Any())
                {
                    AccountTypes = result.List.ToList();
                }
            }
            catch (ServiceException ex)
            {
                this.SignUpButtonStatus = false;
                SendNotification(new ExceptionTransfer { ex = ex, NotificationMessage = "Hesap tipleri yüklenemedi" });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        #endregion
    }
}