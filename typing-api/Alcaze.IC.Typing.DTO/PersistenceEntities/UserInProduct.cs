using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class UserInProduct
    {
        [Key]
        [Column(Order = 10)]
        public int UserFunction { get; set; }
        [Key]
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }
        [Key]
        [Column(Order = 30)]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
