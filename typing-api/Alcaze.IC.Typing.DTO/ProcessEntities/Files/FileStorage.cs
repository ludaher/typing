using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alcaze.IC.Typing.DTO.ProcessEntities.Files
{
    public class FileStorage : BaseEntity
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string MyProperty { get; set; }
    }
}
