using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DTO.PersistenceEntities
{
    public partial class UserInForms
    {
        public int UserFunction { get; set; }
        public string UserName { get; set; }
        public int FormId { get; set; }

        public Forms Form { get; set; }
    }
}
