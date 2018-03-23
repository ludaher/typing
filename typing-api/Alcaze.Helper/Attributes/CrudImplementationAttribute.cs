using System;
using System.Collections.Generic;
using System.Text;

namespace Alcaze.Helper.Attributes
{
    public class CrudImplementationAttribute: Attribute
    {
        public string CrudNamespace { get; set; }
        public string ValidatorNamespace { get; set; }
        public CrudImplementationAttribute(string crudNamespace)
        {
            this.CrudNamespace = crudNamespace;
        }
        public CrudImplementationAttribute(string crudNamespace, string validatorNamespace)
        {
            this.CrudNamespace = crudNamespace;
            this.ValidatorNamespace = validatorNamespace;
        }
    }
}
