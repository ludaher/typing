using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Sections : BaseEntity
    {
        public Sections()
        {
            Fields = new HashSet<Fields>();
        }

        public int SectionId { get; set; }
        public int FormId { get; set; }
        public string SectionName { get; set; }
        public int Position { get; set; }
        public bool IsTable { get; set; }
        public int NumberOfRows { get; set; }

        public Forms Form { get; set; }
        public ICollection<Fields> Fields { get; set; }
    }
}
