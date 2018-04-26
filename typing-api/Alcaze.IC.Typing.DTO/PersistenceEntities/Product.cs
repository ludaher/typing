using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Product: BaseEntity
    {
        public Product()
        {
            Sections = new HashSet<Section>();
            TypingProcesses = new HashSet<TypingProcesses>();
            UserInProducts = new HashSet<UserInProduct>();
        }
        [Key]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }

        //[NotMapped]
        //public string TemplateBase64 { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string TemplatePath { get; set; }
        [Required]
        public int TemplateHeight { get; set; }
        [Required]
        public int ProductState { get; set; }
        [Required]
        public short RequiredCaptures { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<TypingProcesses> TypingProcesses { get; set; }
        public ICollection<UserInProduct> UserInProducts { get; set; }
    }
}
