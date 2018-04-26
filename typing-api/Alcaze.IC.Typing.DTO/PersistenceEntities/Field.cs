using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Field : BaseEntity
    {
        public Field()
        {
            InverseParentField = new HashSet<Field>();
        }

        [Key]
        public int FieldId { get; set; }
        [Required]
        public int SectionId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FieldName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string DefaultValue { get; set; }
        [Required]
        public int FieldTypeId { get; set; }
        [Required]
        public bool Required { get; set; }
        [Required]
        public bool DobleCapture { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Options { get; set; }
        public int? ParentFieldId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Validation { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public int MaxLength { get; set; }
        [Required]
        public int OrderInSection { get; set; }

        [ForeignKey("ParentFieldId")]
        public Field ParentField { get; set; }
        [ForeignKey("SectionId")]
        public Section Section { get; set; }
        public ICollection<Field> InverseParentField { get; set; }
    }
}
