using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class DuplicatedEntityException : LogicException
    {
        public DuplicatedEntityException(string message) : base(message)
        {
        }

        public DuplicatedEntityException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
