using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class IncompleteException: LogicException
    {
        public IncompleteException(string message) : base(message)
        {
        }
        public IncompleteException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
