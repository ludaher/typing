using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(HttpStatusCode statusCode, string message) : base(message)
        {

        }
    }
}
