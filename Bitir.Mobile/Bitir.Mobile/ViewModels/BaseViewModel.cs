using Bitir.Mobile.Models;
using Bitir.Mobile.Models.Auth;
using Bitir.Mobile.Models.Common;
using Bitir.Mobile.Services;
using Bitir.Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Xamarin.Forms;

namespace Bitir.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IAuthService authService => DependencyService.Get<IAuthService>();
        public IProductService productService => DependencyService.Get<IProductService>();
        public ICarrierService carrierService => DependencyService.Get<ICarrierService>();
        public IOrderService orderService => DependencyService.Get<IOrderService>();
        public IProvinceService provinceService => DependencyService.Get<IProvinceService>();
        public IDistrictService districtService => DependencyService.Get<IDistrictService>();
        public INeighbourhoodService neighbourhoodService => DependencyService.Get<INeighbourhoodService>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public Page CurrentPage { get; set; }

        public INavigation Navigation { get; set; }

        protected void SendNotification(ExceptionTransfer exceptionTransfer)
        {
            MessagingCenter.Send(this, "infomessage", exceptionTransfer);
        }

        protected ProfileResponse GetClaims()
        {
            ProfileResponse result = null;
            if (App.authResponse != null && !string.IsNullOrEmpty(App.authResponse.Token))
            {
                var token = App.authResponse.Token;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var id = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
                var email = jwtSecurityToken.Claims.FirstOrDefault(i => i.Type ==  "email").Value;
                var phone = jwtSecurityToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.MobilePhone).Value;
                var name= jwtSecurityToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name).Value;
                var surName= jwtSecurityToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Surname).Value;
                result = new ProfileResponse
                {
                    Id = id,
                    Email = email,
                    Phone = phone,
                    Name=name,
                    Surname=surName
                };

            }
            return result;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
