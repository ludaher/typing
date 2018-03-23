using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Fields : BaseEntity
    {
        public Fields()
        {
            InverseParentField = new HashSet<Fields>();
        }

        public int FieldId { get; set; }
        public int SectionId { get; set; }
        public string FieldName { get; set; }
        public string Title { get; set; }
        public string DefaultValue { get; set; }
        public int FieldTypeId { get; set; }
        public bool Required { get; set; }
        public bool DobleCapture { get; set; }
        public string Options { get; set; }
        public int? ParentFieldId { get; set; }
        public string Validation { get; set; }
        public int Size { get; set; }
        public int MaxLength { get; set; }
        public int OrderInForm { get; set; }

        public Fields ParentField { get; set; }
        public Sections Section { get; set; }
        public ICollection<Fields> InverseParentField { get; set; }
    }
}
