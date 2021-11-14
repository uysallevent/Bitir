﻿using Core.Entities;
using Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.AuthModuleEntities
{
    public class Neighbourhood: BaseEntity
    {
        public string Name { get; set; }
        public string LocalityName { get; set; }
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }

        [NotMapped]
        public override Status? Status { get => base.Status; set => base.Status = value; }
        [NotMapped]
        public override DateTime? InsertDate { get => base.InsertDate; set => base.InsertDate = value; }
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
