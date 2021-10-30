using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.AuthModuleEntities
{
    public class UserAddress : BaseEntity
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        [ForeignKey("UserId")]
        public UserAccount UserAccount { get; set; }
    }
}
