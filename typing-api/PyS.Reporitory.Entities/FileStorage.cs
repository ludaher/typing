using System;
using System.Collections.Generic;
using System.Text;

namespace PyS.Repository.Entities
{
    public class FileStorage
    {
        public string FileName { get; set; }

        public string Id { get; set; }

        public long Length { get; set; }

        public string MD5 { get; set; }

        public string JsonMetadata { get; set; }

        public DateTime UploadDateTime { get; set; }

        public string DataBase64 { get; set; }

        public byte[] Data{ get; set; }
    }
}
