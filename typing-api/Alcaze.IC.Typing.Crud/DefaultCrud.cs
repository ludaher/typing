using Alcaze.API.EntityFramework;
using Alcaze.IC.Typing.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alcaze.IC.Typing.Crud
{
    public class DefaultCrud<T> : GenericCrud<T, ImaginCrudContext>
        where T:class
    {
        public DefaultCrud() : base(new ImaginCrudContext())
        {
        }
    }
}
