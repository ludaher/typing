using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class FormDataHistoryDetails
    {
        public int FieldId { get; set; }
        public long FormDataId { get; set; }
        public string Value { get; set; }

        public FormDataHistories FormData { get; set; }
    }
}
