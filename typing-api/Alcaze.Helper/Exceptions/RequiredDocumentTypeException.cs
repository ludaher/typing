using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class RequiredDocumentTypeException : Exception
    {
        public int Page { get; }
        public RequiredDocumentTypeException(string message, int page) : base(message)
        {
            Page = page;
        }
        public RequiredDocumentTypeException(string message, Exception ex, int page) : base(message,ex)
        {
            Page = page;
        }
    }
}
