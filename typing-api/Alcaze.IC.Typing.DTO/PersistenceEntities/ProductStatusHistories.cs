using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class ProductStatusHistories : BaseEntity
    {
        public int ProductStatusHistoryId { get; set; }
        public string TypingProcessId { get; set; }
        public int FormId { get; set; }
        public int TypingStatus { get; set; }
        public string Observations { get; set; }

        public TypingProcesses TypingProcesses { get; set; }
    }
}
