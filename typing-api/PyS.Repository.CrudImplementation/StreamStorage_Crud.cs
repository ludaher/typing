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
    public class StreamStorage_Crud : UnimplementCrud<StreamStorage>
    {

        public StreamStorage_Crud()
        {

        }

        public override async Task<StreamStorage> InsertAsync(StreamStorage entity)
        {
            return await _UploadFile(entity);
        }


        public override async Task<StreamStorage> UpdateAsync(StreamStorage entity)
        {
            if (entity.Stream == null)
                await _UpdateMetadata(entity);
            else
                await _UploadFile(entity);
            return entity;
        }

        public override async Task DeleteAsync(StreamStorage entity)
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

        public override async Task<FindResult<StreamStorage>> FindAsync(Conditions searchConditions, int page, int pageSize, string orderBy, bool ascending = true, List<string> includes = null)
        {
            var filters = new List<FilterDefinition<GridFSFileInfo>>();
            var builder = Builders<GridFSFileInfo>.Filter;
            foreach (var condition in searchConditions)
            {
                if (condition.Item2.Equals(ComparisonOperator.Equal))
                    filters
                        .Add(string.Format("{ {0} : { $eq : {1} } }", condition.Item1, condition.Item3));
                else if (condition.Item2.Equals(ComparisonOperator.Contains))
                    filters
                        .Add(string.Format("{ {0} : { $regex : /{1}/ } }", condition.Item1, condition.Item3));
                else if (condition.Item2.Equals(ComparisonOperator.GreaterThanOrEqual))
                    filters
                        .Add(string.Format("{ {0} : { $gte : {1} } }", condition.Item1, condition.Item3));
                else if (condition.Item2.Equals(ComparisonOperator.LessThanOrEqual))
                    filters
                        .Add(string.Format("{ {0} : { $lte : {1} } }", condition.Item1, condition.Item3));
                else if (condition.Item2.Equals(ComparisonOperator.GreaterThanOrEqual))
                    filters
                        .Add(string.Format("{ {0} : { $ne : {1} } }", condition.Item1, condition.Item3));
            }

            var options = new FindOptions<GridFSFileInfo>
            {
                Limit = pageSize,
                Skip = (page - 1) * pageSize,
            };
            using (var _context = new RepositoryContext())
            {
                var collectionName = $"{_context.GridFsBucket.Options.BucketName}.files";
                var filesCollection = _context.GetCollection<GridFSFileInfo>(collectionName);
                var query = await filesCollection.FindAsync(builder.And(filters));

                var list = await query.ToListAsync();
                var result = new FindResult<StreamStorage>();
                result.ResultList = list.Select(x => new StreamStorage()
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

            //using (var _context = new RepositoryContext())
            //{
            //    var query = await _context.GridFsBucket
            //    .FindAsync(builder.And(filters), options);
            //    var list = await query.ToListAsync();
            //    var result = new FindResult<StreamStorage>();
            //    result.ResultList = list.Select(x => new StreamStorage()
            //    {
            //        FileName = x.Filename,
            //        Id = x.Id.ToString(),
            //        JsonMetadata = x.Metadata.ToJson(),
            //        Length = x.Length,
            //        MD5 = x.MD5,
            //        UploadDateTime = x.UploadDateTime
            //    });
            //    return result;
            //}
        }

        public override void Dispose()
        {
        }

        #region private methods

        private async Task<StreamStorage> _UploadFile(StreamStorage entity)
        {
            using (var _context = new RepositoryContext())
            {
                var options = new GridFSUploadOptions();
                if (string.IsNullOrWhiteSpace(entity.JsonMetadata) == false)
                    options.Metadata = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(entity.JsonMetadata);
                if (string.IsNullOrWhiteSpace(entity.Id))
                    entity.Id = (await _context.GridFsBucket
                    .UploadFromStreamAsync(entity.FileName, entity.Stream, options)).ToString();
                else
                    await _context.GridFsBucket
                    .UploadFromStreamAsync(ObjectId.Parse(entity.Id), entity.FileName, entity.Stream, options);
                return entity;
            }
        }

        private async Task<StreamStorage> _UpdateMetadata(StreamStorage entity)
        {
            using (var _context = new RepositoryContext())
            {
                var collectionName = $"{_context.GridFsBucket.Options.BucketName}.files";
                MongoDB.Bson.BsonDocument documentMetadata
                    = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(entity.JsonMetadata ?? "");
                var filesCollection = _context.GetCollection<BsonDocument>(collectionName);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", entity.Id);
                var updateBuilder = Builders<BsonDocument>.Update;
                var updates = new List<UpdateDefinition<BsonDocument>>();
                foreach (var item in documentMetadata)
                {
                    updates.Add(updateBuilder.Set($"metadata.{item.Name}", item.Value));
                }
                var result = await filesCollection.UpdateOneAsync(filter, updateBuilder.Combine(updates));

                return entity;
            }
        }


        #endregion
    }
}
