using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PyS.Repository.Entities
{
    public class StreamStorage
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string JsonMetadata { get; set; }
        public string MD5 { get; set; }
        public DateTime UploadDateTime { get; set; }
        public long Length { get; set; }
        public Stream Stream { get; set; }
    }
}
