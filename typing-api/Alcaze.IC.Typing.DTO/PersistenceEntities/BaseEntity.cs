using System;
using System.Collections.Generic;
using System.Text;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public class BaseEntity
    {
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
