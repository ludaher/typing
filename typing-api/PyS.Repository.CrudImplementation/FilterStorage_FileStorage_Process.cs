using Alcaze.API;
using Alcaze.Helper.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PyS.Repository.DataAccess;
using PyS.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyS.Repository.Crud
{
    public class FilterStorage_FileStorage_Process : IProcess<FilterStorage, FileStorage>
    {
        public void Dispose()
        {

        }

        public async Task<FileStorage> Execute(FilterStorage entity)
        {
            using (var _context = new RepositoryContext())
            {

                var filter = entity.Filter.BuildMetadataFiltersGridFS();
                var collectionName = $"{_context.GridFsBucket.Options.BucketName}.files";
                var filesCollection = _context.GetCollection<GridFSFileInfo>(collectionName);
                var options = new FindOptions<GridFSFileInfo>
                {
                    Limit = 1,
                    Skip = 0
                };
                var query = await filesCollection
                            .FindAsync(filter, options);
                var fileInfo = (await query.ToListAsync()).FirstOrDefault();
                if (fileInfo == null)
                    throw new LogicException("Archivo no encontrado.");
                var file = await _context.GridFsBucket.DownloadAsBytesAsync(fileInfo.Id);
                return new FileStorage()
                {
                    Id = fileInfo.Id.ToString(),
                    Data = file,
                    FileName = fileInfo.Filename,
                    UploadDateTime = fileInfo.UploadDateTime,
                    Length = fileInfo.Length
                };
            }
        }
    }
}