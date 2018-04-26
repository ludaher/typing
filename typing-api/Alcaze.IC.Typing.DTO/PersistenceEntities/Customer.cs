using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
