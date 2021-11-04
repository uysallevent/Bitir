using Core.Entities;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int UserAddressId { get; set; }
        public string Note { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("UserId")]
        public UserAccount UserAccount { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
