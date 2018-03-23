using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class BadRequestException : LogicException
    {
        public BadRequestException(string message) : base(message)
        {

        }
        public BadRequestException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
