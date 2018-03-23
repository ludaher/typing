using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class LogicException: Exception
    {
        public LogicException(string message) : base(message)
        {
        }
        public LogicException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
