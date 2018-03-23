using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class FieldDataSourceDetails : BaseEntity
    {
        public int FieldDataSourceDetailId { get; set; }
        public int FieldDataSourceId { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }

        public FieldDataSources FieldDataSource { get; set; }
    }
}
