using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class EntityNotFoundException : LogicException
    {
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}
