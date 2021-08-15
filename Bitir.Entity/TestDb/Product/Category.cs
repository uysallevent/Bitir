using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bitir.Entity.TestDb.Product
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public IEnumerable<Category> Categories { get; set; }
    }
}
