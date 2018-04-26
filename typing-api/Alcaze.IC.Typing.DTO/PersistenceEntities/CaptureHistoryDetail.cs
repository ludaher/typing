using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class CaptureHistoryDetail
    {
        [Key]
        [Column(Order = 10)]
        public int FieldId { get; set; }
        [Key]
        [Column(Order = 20)]
        public long CaptureId { get; set; }
        [Column(TypeName = "varchar(2000)")]
        public string Value { get; set; }

        [ForeignKey("CaptureId")]
        public CaptureHistory CaptureHistory { get; set; }
    }
}
