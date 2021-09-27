﻿using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Models.Auth;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseWrapper<AuthResponse>> LoginCheck(AuthLoginRequest request);
        Task<ResponseWrapperListing<AccountTypeResponse>> GetAccoutTypes();
        Task<ResponseWrapper<AuthResponse>> Register(AuthRegisterRequest request);
    }
}
