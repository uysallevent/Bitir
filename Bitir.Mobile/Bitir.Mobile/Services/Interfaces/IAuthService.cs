using AuthModule.Dto;
using AuthModule.Security.JWT;
using Core.Wrappers;
using Bitir.Mobile.Models.Auth;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseWrapper<AccessToken>> LoginCheckAsync(LoginDto request);
        Task<ResponseWrapper<AccessToken>> RegisterAsync(AuthRegisterRequest request);
        Task<ResponseWrapper<ProfileResponse>> GetAccountByIdAsync(int Id);
        Task<ResponseWrapper<ProfileResponse>> UpdateAccountAsync(AuthRegisterRequest request);
    }
}
