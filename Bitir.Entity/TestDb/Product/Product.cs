using Core.Entities;

namespace Bitir.Entity.TestDb.Product
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
