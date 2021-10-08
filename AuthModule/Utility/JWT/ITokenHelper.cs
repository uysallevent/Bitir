using AuthModule.Dtos;
using Module.Shared.Entities.AuthModuleEntities;
using System.Collections.Generic;

namespace AuthModule.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(UserAccount user, List<OperationClaim> operationClaims);
    }
}