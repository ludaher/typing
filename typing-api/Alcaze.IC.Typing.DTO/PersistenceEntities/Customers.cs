using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class Customers : BaseEntity
    {
        public Customers()
        {
            Forms = new HashSet<Forms>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        public ICollection<Forms> Forms { get; set; }
    }
}
