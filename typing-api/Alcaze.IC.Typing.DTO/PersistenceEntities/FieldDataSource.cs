using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class FieldDataSource : BaseEntity
    {
        public FieldDataSource()
        {
            FieldDataSourceDetails = new HashSet<FieldDataSourceDetail>();
        }

        [Key]
        public int FieldDataSourceId { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Description { get; set; }

        public ICollection<FieldDataSourceDetail> FieldDataSourceDetails { get; set; }
    }
}
