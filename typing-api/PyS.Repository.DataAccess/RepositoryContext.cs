using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyS.Repository.DataAccess
{
    public class RepositoryContext: IDisposable
    {
        MongoClient client;
        IMongoDatabase database;

        public IGridFSBucket GridFsBucket { get; }

        public RepositoryContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString =
                config.GetConnectionString("MongoConnection");
            var connection = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(connection);
            var client = new MongoClient(settings);
            database = client.GetDatabase(connection.DatabaseName);
            GridFsBucket = new GridFSBucket(database);

        }
        
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
