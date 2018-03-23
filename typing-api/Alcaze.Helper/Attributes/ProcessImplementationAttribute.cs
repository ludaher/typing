using System;
using System.Collections.Generic;
using System.Text;

namespace Alcaze.API.Attributes
{
    public class ProcessImplementationAttribute : Attribute
    {
        public string ImplementationNamespace { get; set; }
        public ProcessImplementationAttribute(string implementationNamespace)
        {
            this.ImplementationNamespace = implementationNamespace;
        }
    }
}
