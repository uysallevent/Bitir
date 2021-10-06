﻿
using Bitir.Mobile.Exceptions;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Validators;
using Bitir.Mobile.Validators.Rules;
using Bitir.Mobile.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.ViewModels
{
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

        public Command LoginCommand { get; set; }

        public Command SignUpCommand { get; set; }

        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        public bool AreFieldsValid()
        {
            bool isEmail = this.Email.Validate();
            bool isNameValid = this.Name.Validate();
            bool isPasswordValid = this.Password.Validate();
            return isPasswordValid && isNameValid && isEmail;
        }

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

        private void AddValidationRules()
        {
            this.Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Ad Soyad Zorunludur!" });
            this.Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email Zorunludur!" });
            this.Password.Item1.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Şifre Zorunludur!" });
            this.Password.Item2.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Şifre Tekrarı Zorunludur!" });
        }

        
        private async void LoginClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage(), true);
        }

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

        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        #endregion
    }
}