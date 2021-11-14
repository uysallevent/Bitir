using Core.Wrappers;
using Module.Shared.Entities.AuthModuleEntities;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface IProvinceService
    {
        Task<ResponseWrapper<Province>> GetProvince(Province request);
    }
}
