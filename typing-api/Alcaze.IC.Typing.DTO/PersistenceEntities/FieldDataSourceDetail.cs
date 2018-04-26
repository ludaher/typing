using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class FieldDataSourceDetail : BaseEntity
    {
        [Key]
        public int FieldDataSourceDetailId { get; set; }
        public int FieldDataSourceId { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Value { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Label { get; set; }

        [ForeignKey("FieldDataSourceId")]
        public FieldDataSource FieldDataSource { get; set; }
    }
}
