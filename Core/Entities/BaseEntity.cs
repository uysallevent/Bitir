using System;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public abstract class BaseEntity: IEntity
    {
        public virtual int Id { get; set; }

        [JsonIgnore]
        public virtual DateTime InsertDate { get; set; }

        [JsonIgnore]
        public virtual DateTime UpdateDate { get; set; }

        public int Status { get; set; }
    }
}
