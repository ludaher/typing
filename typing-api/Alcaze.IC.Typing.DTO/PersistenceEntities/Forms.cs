using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Forms: BaseEntity
    {
        public Forms()
        {
            Sections = new HashSet<Sections>();
            TypingProcesses = new HashSet<TypingProcesses>();
            UserInForms = new HashSet<UserInForms>();
        }

        public int FormId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        //[NotMapped]
        //public string TemplateBase64 { get; set; }
        public string TemplatePath { get; set; }
        public int TemplateHeight { get; set; }
        public int ProductStatus { get; set; }
        public short RequiredCaptures { get; set; }

        public Customers Customer { get; set; }
        public ICollection<Sections> Sections { get; set; }
        public ICollection<TypingProcesses> TypingProcesses { get; set; }
        public ICollection<UserInForms> UserInForms { get; set; }
    }
}
