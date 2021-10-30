using AuthModule.Dto;
using AuthModule.Security.JWT;
using BaseModule.Interfaces;
using Core.Entities;
using Core.Wrappers;
using Module.Shared.Entities.AuthModuleEntities;
using System.Threading.Tasks;

namespace AuthModule.Interfaces
{
    public interface IAuthBusinessBase<T> : IBusinessBase<T> where T : class, IEntity, new()
    {
        Task<ResponseWrapper<AccessToken>> Login(LoginDto loginDto);
        Task<ResponseWrapper<AccessToken>> RefreshTokenLogin(int userId, string refreshToken);
        Task<ResponseWrapper<AccessToken>> Register(UserAccount entity);
    }
}