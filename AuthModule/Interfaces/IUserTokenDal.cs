using AuthModule.Models;
using BaseModule.Interfaces;

namespace AuthModule.Interfaces
{
    public interface IUserTokenDal : IDalBase<UserToken>
    {
        UserToken GetUserAndRefreshToken(int userId, string refreshToken);

        int DisableActiveRecord(int userId);
    }
}