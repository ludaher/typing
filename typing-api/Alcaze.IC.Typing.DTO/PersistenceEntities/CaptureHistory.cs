using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class CaptureHistory : BaseEntity
    {
        public CaptureHistory()
        {
            CaptureHistoryDetails = new HashSet<CaptureHistoryDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CaptureId { get; set; }
        [Required]
        public int TypingProcessId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int RegisterType { get; set; }
        [Required]
        public int CompletedSections { get; set; }
        [Required]
        public bool Completed { get; set; }

        [ForeignKey("TypingProcessId")]
        public TypingProcesses TypingProcesses { get; set; }
        public ICollection<CaptureHistoryDetail> CaptureHistoryDetails { get; set; }
    }
}
