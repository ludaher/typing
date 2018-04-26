using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Section : BaseEntity
    {
        public Section()
        {
            Fields = new HashSet<Field>();
        }

        [Key]
        public int SectionId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string SectionName { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        public bool IsTable { get; set; }
        [Required]
        public int NumberOfRows { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
}
