using AuthModule.Security.JWT;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthModule.Interfaces
{
    public interface IAuthBusinessBase
    {
        IDataResult<AccessToken> RefreshTokenLogin(int userId, string refreshToken);
    }
}
