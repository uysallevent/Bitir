using Core.Entities;

namespace Bitir.Entity.TestDb.Product
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
