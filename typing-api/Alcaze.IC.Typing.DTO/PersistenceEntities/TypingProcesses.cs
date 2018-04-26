using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class TypingProcesses : BaseEntity
    {
        public TypingProcesses()
        {
            CaptureHistories = new HashSet<CaptureHistory>();
            Captures = new HashSet<Capture>();
            ProductStateHistories = new HashSet<ProductStateHistory>();
        }

        [Key]
        //[Column(Order = 10)]
        public int TypingProcessId { get; set; }
        //[Key]
        //[Column(Order = 20)]
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FileName { get; set; }
        [Required]
        public int TypingStatus { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Observations { get; set; }
        [Required]
        public int Priority { get; set; }
        public DateTime ProductionDate { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public ICollection<CaptureHistory> CaptureHistories { get; set; }
        public ICollection<Capture> Captures { get; set; }
        public ICollection<ProductStateHistory> ProductStateHistories { get; set; }
    }
}
