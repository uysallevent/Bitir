using AuthModule.Models;
using BaseModule.Dal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthModule.Interfaces
{
    public interface IUserAccountDal: IDalBase<UserAccount>
    {
    }
}
