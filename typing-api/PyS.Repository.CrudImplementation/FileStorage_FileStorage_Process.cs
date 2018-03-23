using Alcaze.API;
using Alcaze.Helper.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PyS.Repository.DataAccess;
using PyS.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PyS.Repository.Crud
{
    public class FileStorage_FileStorage_Process : IProcess<FileStorage, FileStorage>
    {
        public void Dispose()
        {

        }

        public async Task<FileStorage> Execute(FileStorage entity)
        {
            var objectId = ObjectId.Parse(entity.Id);
            using (var _context = new RepositoryContext())
            {
                var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", objectId);
                var fileInfo = await _context.GridFsBucket.Find(filter).FirstOrDefaultAsync();
                if (fileInfo == null)
                    throw new LogicException("Archivo no encontrado.");
                var file = await _context.GridFsBucket.DownloadAsBytesAsync(objectId);
                return new FileStorage()
                {
                    Id = entity.Id,
                    Data = file,
                    FileName = fileInfo.Filename,
                    UploadDateTime = fileInfo.UploadDateTime,
                    Length = fileInfo.Length
                };
            }
        }
    }
}