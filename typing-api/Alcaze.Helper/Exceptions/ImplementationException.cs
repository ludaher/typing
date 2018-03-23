using System;
using System.Collections.Generic;
using System.Text;

namespace Alcaze.Helper.Exceptions
{
    public class ImplementationException : Exception
    {
        public ImplementationException(string message) : base(message)
        {
        }
        public ImplementationException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
