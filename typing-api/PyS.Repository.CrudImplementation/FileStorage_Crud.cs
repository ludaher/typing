using Alcaze.API;
using Alcaze.Helper.Lambda;
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
    public class FileStorage_Crud : UnimplementCrud<FileStorage>
    {

        public FileStorage_Crud()
        {

        }

        public override async Task<FileStorage> InsertAsync(FileStorage entity)
        {
            return await _UploadFile(entity);
        }


        public override async Task<FileStorage> UpdateAsync(FileStorage entity)
        {
            if (string.IsNullOrWhiteSpace(entity.DataBase64))
                await _UpdateMetadata(entity);
            return entity;
        }

        public override async Task DeleteAsync(FileStorage entity)
        {
            using (var _context = new RepositoryContext())
            {
                if (string.IsNullOrWhiteSpace(entity.Id) == false)
                {
                    await _context.GridFsBucket.DeleteAsync(ObjectId.Parse(entity.Id));
                    return;
                }
            }
        }

        private async Task<GridFSFileInfo> GetFileInfoAsync(string fileName)
        {
            using (var _context = new RepositoryContext())
            {
                var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, fileName);
                var fileInfo = await _context.GridFsBucket.Find(filter).FirstOrDefaultAsync();
                return fileInfo;
            }
        }

        public override async Task<FindResult<object>> FindSelectAsync(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool ascending = true)
        {
            var filter = searchConditions.BuildMetadataFiltersGridFS();
            var projection = _BuildProjection(selectFields);
            var options = new FindOptions<GridFSFileInfo>
            {
                Limit = pageSize == 0 ? 50 : pageSize,
                Skip = pageSize == 0 ? 0 : (page - 1) * pageSize,
                Projection = projection
            };
            if (string.IsNullOrWhiteSpace(orderBy) == false)
                options.Sort = orderBy;
            using (var _context = new RepositoryContext())
            {
                var collectionName = $"{_context.GridFsBucket.Options.BucketName}.files";
                var filesCollection = _context.GetCollection<GridFSFileInfo>(collectionName);

                var query = await filesCollection
                            .FindAsync(filter, options);
                var total = await filesCollection.CountAsync(filter);
                var result = new FindResult<object>();
                var list = await query.ToListAsync();
                result.ResultList = list.Select(x => new
                {
                    FileName = x.Filename ?? null,
                    Id = x.Id.ToString(),
                    JsonMetadata = x.Metadata == null ? null : x.Metadata.ToJson(),
                    Length = x.Length,
                    MD5 = x.MD5 ?? null,
                    UploadDateTime = x.UploadDateTime
                }).ToList();
                return result;
            }

        }

        private ProjectionDefinition<GridFSFileInfo> _BuildProjection(string selectFields)
        {
            ProjectionDefinition<GridFSFileInfo> projectionDefinition = Builders<GridFSFileInfo>.Projection
                .Include(x => x.UploadDateTime)
                .Include(x => x.Filename)
                .Include(x => x.MD5)
                .Include(x => x.Length)
                ;
            foreach (var field in selectFields.Replace("metadata.", "").Split(','))
            {
                projectionDefinition = projectionDefinition.Include(x => x.Metadata[field]);

            }
            //if (projectionDefinition == null)
            //    projectionDefinition = .Include(x => x.Metadata[field]);
            //else
            return projectionDefinition;
        }

        public override async Task<FindResult<FileStorage>> FindAsync(Conditions searchConditions, int page, int pageSize, string orderBy, bool ascending = true, List<string> includes = null)
        {
            var filter = searchConditions.BuildMetadataFiltersGridFS();
            var options = new FindOptions<GridFSFileInfo>
            {
                Limit = pageSize == 0 ? 50 : pageSize,
                Skip = pageSize == 0 ? 0 : (page - 1) * pageSize,
            };
            if (string.IsNullOrWhiteSpace(orderBy) == false)
                options.Sort = orderBy;
            using (var _context = new RepositoryContext())
            {
                var collectionName = $"{_context.GridFsBucket.Options.BucketName}.files";
                var filesCollection = _context.GetCollection<GridFSFileInfo>(collectionName);

                var query = await filesCollection
                            .FindAsync(filter, options);
                var total = await filesCollection.CountAsync(filter);
                var result = new FindResult<FileStorage>();
                var list = await query.ToListAsync();
                result.ResultList = list.Select(x => new FileStorage()
                {
                    FileName = x.Filename,
                    Id = x.Id.ToString(),
                    JsonMetadata = x.Metadata.ToJson(),
                    Length = x.Length,
                    MD5 = x.MD5,
                    UploadDateTime = x.UploadDateTime
                });
                return result;
            }
        }

        public override void Dispose()
        {
        }

        #region private methods

        private async Task<FileStorage> _UploadFile(FileStorage entity)
        {
            using (var _context = new RepositoryContext())
            {
                var options = new GridFSUploadOptions();
                if (string.IsNullOrWhiteSpace(entity.JsonMetadata) == false)
                    options.Metadata = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(entity.JsonMetadata);
                var data = Convert.FromBase64String(entity.DataBase64);
                if (string.IsNullOrWhiteSpace(entity.Id))
                    entity.Id = (await _context.GridFsBucket
                    .UploadFromBytesAsync(entity.FileName, data, options)).ToString();
                else
                    await _context.GridFsBucket
                    .UploadFromBytesAsync(ObjectId.Parse(entity.Id), entity.FileName, data, options);
                return entity;
            }
        }

        private async Task<FileStorage> _UpdateMetadata(FileStorage entity)
        {
            using (var _context = new RepositoryContext())
            {
                var collectionName = $"{_context.GridFsBucket.Options.BucketName}.files";
                MongoDB.Bson.BsonDocument documentMetadata
                    = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(entity.JsonMetadata ?? "");
                var filesCollection = _context.GetCollection<GridFSFileInfo>(collectionName);
                var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
                var updateBuilder = Builders<GridFSFileInfo>.Update;
                var updates = new List<UpdateDefinition<GridFSFileInfo>>();
                updates.AddRange(_GetUpdates(documentMetadata,"metadata",updateBuilder));
                var result = await filesCollection.UpdateOneAsync(filter, updateBuilder.Combine(updates));
                return entity;
            }
        }

        private List<UpdateDefinition<GridFSFileInfo>> _GetUpdates(BsonValue value, string parentName, UpdateDefinitionBuilder<GridFSFileInfo> updateBuilder)
        {
            var updates = new List<UpdateDefinition<GridFSFileInfo>>();
            if (value.BsonType == BsonType.Document)
            {
                var document = value.AsBsonDocument;
                foreach (var item in document)
                {
                    updates.AddRange(_GetUpdates(item.Value, $"{parentName}.{item.Name}", updateBuilder));
                }
            }
            else
            {
                updates.Add(updateBuilder.Set(parentName, value));
            }
            return updates;
        }

        #endregion
    }
}
