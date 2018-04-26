using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class ProductStateHistory : BaseEntity
    {
        [Key]
        public int ProductStateHistoryId { get; set; }
        [Required]
        public int TypingProcessId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int TypingState { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Observations { get; set; }

        [ForeignKey("TypingProcessId")]
        public TypingProcesses TypingProcesses { get; set; }
    }
}
