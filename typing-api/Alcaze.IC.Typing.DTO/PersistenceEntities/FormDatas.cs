using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class FormDatas : BaseEntity
    {
        public FormDatas()
        {
            FormDataDetails = new HashSet<FormDataDetails>();
        }

        public long FormDataId { get; set; }
        public string TypingProcessId { get; set; }
        public int FormId { get; set; }
        public int RegisterType { get; set; }
        public int CompletedSections { get; set; }
        public bool Completed { get; set; }

        public TypingProcesses TypingProcesses { get; set; }
        public ICollection<FormDataDetails> FormDataDetails { get; set; }
    }
}
