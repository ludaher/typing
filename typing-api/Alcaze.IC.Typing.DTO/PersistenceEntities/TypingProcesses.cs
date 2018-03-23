using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class TypingProcesses : BaseEntity
    {
        public TypingProcesses()
        {
            FormDataHistories = new HashSet<FormDataHistories>();
            FormDatas = new HashSet<FormDatas>();
            ProductStatusHistories = new HashSet<ProductStatusHistories>();
        }

        public string TypingProcessId { get; set; }
        public int FormId { get; set; }
        public int TypingStatus { get; set; }
        public string Observations { get; set; }
        public int Priority { get; set; }
        public DateTime ProductionDate { get; set; }

        public Forms Form { get; set; }
        public ICollection<FormDataHistories> FormDataHistories { get; set; }
        public ICollection<FormDatas> FormDatas { get; set; }
        public ICollection<ProductStatusHistories> ProductStatusHistories { get; set; }
    }
}
