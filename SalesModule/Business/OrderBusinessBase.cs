using AuthModule.Interfaces;
using BaseModule.Business;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using Core.Entities;
using Core.Exceptions;
using Core.Utilities.ExpressionGenerator;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Module.Shared.Entities.SalesModuleEntities;
using System.Linq;
using System.Threading.Tasks;

namespace AuthModule.Business
{
    public class OrderBusinessBase : BusinessBase<Order>, IOrderBusinessBase<Order>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<StoreOrdersView> _storeOrderViewRepository;


        public OrderBusinessBase(
            IUnitOfWork uow,
            ILoggerFactory logger,
            IRepository<Order> orderRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository<StoreOrdersView> storeOrderViewModelRepository) : base(orderRepository, uow)
        {
            _uow = uow;
            _logger = logger.CreateLogger<OrderBusinessBase>();
            _httpContextAccessor = httpContextAccessor;
            _storeOrderViewRepository = storeOrderViewModelRepository;

        }

        public async Task<ResponseWrapperListing<StoreOrdersView>> StoreOrders(PagingRequestEntity<StoreOrdersView> request)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int.TryParse(claims.FirstOrDefault(x => x.Type == "Store")?.Value, out int storeId);
            if (storeId < 1)
            {
                throw new ClaimExpection("Claims could not find");
            }
            if (request.Entity == null)
            {
                request.Entity = new StoreOrdersView();
            }
            request.Entity.StoreId = storeId;

            var filter = request.Entity != null ? ExpressionGenerator<StoreOrdersView, StoreOrdersView>.Generate(request.Entity) : null;
            var orders = _storeOrderViewRepository.GetAll(filter).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
            var count = _storeOrderViewRepository.GetAll(filter).Count();
            var hasNextPage = (request.Page * request.PageSize) < count;
            return new ResponseWrapperListing<StoreOrdersView>(orders, count, request.Page, null, hasNextPage);
        }
    }
}