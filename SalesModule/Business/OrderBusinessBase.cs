using AuthModule.Interfaces;
using BaseModule.Business;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Exceptions;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
using System.Linq;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class OrderBusinessBase : BusinessBase<Order>, IOrderBusinessBase<Order>
    {
        private readonly IRepository<Carrier> _carrierRepository;
        private readonly IRepository<Carrier_Store> _carrierStoreRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<UserAccount> _userAccountRepository;
        private readonly IRepository<UserAddress> _userAddressRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IProcedureExecuter<StoreOrderViewModel> _executeStoreOrderViewModel;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderBusinessBase(
            IUnitOfWork uow,
            IConfiguration configuration,
            ILoggerFactory logger,
            IRepository<Carrier_Store> carrierStoreRepository,
            IRepository<Order> orderRepository,
            IRepository<UserAccount> userAccountRepository,
            IRepository<UserAddress> userAddressRepository,
            IRepository<District> districtRepository,
            IRepository<Province> provinceRepository,
            IProcedureExecuter<StoreOrderViewModel> executeStoreOrderViewModel,
            IHttpContextAccessor httpContextAccessor) : base(orderRepository, uow)
        {
            _uow = uow;
            _configuration = configuration;
            _logger = logger.CreateLogger<CarrierBusinessBase>();
            _carrierStoreRepository = carrierStoreRepository;
            _orderRepository = orderRepository;
            _userAccountRepository = userAccountRepository;
            _userAddressRepository = userAddressRepository;
            _httpContextAccessor = httpContextAccessor;
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
            _executeStoreOrderViewModel = executeStoreOrderViewModel;
        }

        public async Task<ResponseWrapperListing<StoreOrderViewModel>> StoreOrders()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }
            var orders = await _executeStoreOrderViewModel.ExecuteProc("EXEC sales.GetStoreOrders {0}", new object[] { storeId });
            return new ResponseWrapperListing<StoreOrderViewModel>(orders);
        }
    }
}