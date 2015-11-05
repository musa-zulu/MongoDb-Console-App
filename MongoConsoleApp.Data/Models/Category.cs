using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConsoleApp.Data.Models
{
    public class Category
    {
        public Category()
        {
            Expenses = new List<Expense>();
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}