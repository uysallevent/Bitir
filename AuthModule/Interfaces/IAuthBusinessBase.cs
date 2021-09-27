using AuthModule.Dto;
using AuthModule.Entities;
using AuthModule.Security.JWT;
using BaseModule.Interfaces;
using Bitir.Data.Model.Dtos;
using Core.Entities;
using Core.Utilities.Results;
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