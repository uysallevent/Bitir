using Bitir.Data.Model.Dtos;
using Bitir.Mobile.Models.Auth;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseWrapper<AuthResponse>> LoginCheckAsync(AuthLoginRequest request);
        Task<ResponseWrapperListing<AccountTypeResponse>> GetAccoutTypesAsync();
        Task<ResponseWrapper<AuthResponse>> RegisterAsync(AuthRegisterRequest request);
        Task<ResponseWrapper<ProfileResponse>> GetAccountByIdAsync(int Id);
        Task<ResponseWrapper<ProfileResponse>> UpdateAccountAsync(AuthRegisterRequest request);
    }
}
