using Alcaze.API;
using Alcaze.Helper.Lambda;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyS.Repository.Crud
{
    public static class GridFSUtil
    {

        public static FilterDefinition<GridFSFileInfo> BuildMetadataFiltersGridFS(this Conditions searchConditions)
        {
            var filters = new List<FilterDefinition<GridFSFileInfo>>();
            var builder = Builders<GridFSFileInfo>.Filter;
            foreach (var condition in searchConditions)
            {
                var property = condition.Item1.Replace("metadata.", "").ToString();
                var value = _GetValue(condition.Item3.ToString());
                if (condition.Item2.Equals(ComparisonOperator.Equal))
                    filters
                        .Add(builder.Eq(x => x.Metadata[property], value));
                else if (condition.Item2.Equals(ComparisonOperator.Contains))
                    filters
                        .Add(builder.Regex(x => x.Metadata[property], BsonRegularExpression.Create("/" + condition.Item3.ToString() + "/")));
                else if (condition.Item2.Equals(ComparisonOperator.GreaterThanOrEqual))
                    filters
                        .Add(builder.Gte(x => x.Metadata[property], value));
                else if (condition.Item2.Equals(ComparisonOperator.LessThanOrEqual))
                    filters
                        .Add(builder.Lte(x => x.Metadata[property], value));
                else if (condition.Item2.Equals(ComparisonOperator.GreaterThanOrEqual))
                    filters
                        .Add(builder.Ne(x => x.Metadata[property], value));
            }
            return builder.And(filters);
        }

        private static object _GetValue(string item3)
        {
            DateTime date;
            if (DateTime.TryParse(item3, out date))
                return date;

            double number;
            if (Double.TryParse(item3, out number))
                return number;

            Boolean boolean;
            if (Boolean.TryParse(item3, out boolean))
                return boolean;

            return item3;
        }

    }
}
