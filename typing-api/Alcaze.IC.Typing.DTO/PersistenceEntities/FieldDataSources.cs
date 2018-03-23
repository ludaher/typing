using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class FieldDataSources : BaseEntity
    {
        public FieldDataSources()
        {
            FieldDataSourceDetails = new HashSet<FieldDataSourceDetails>();
        }

        public int FieldDataSourceId { get; set; }
        public string Description { get; set; }

        public ICollection<FieldDataSourceDetails> FieldDataSourceDetails { get; set; }
    }
}
