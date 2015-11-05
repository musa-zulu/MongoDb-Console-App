using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConsoleApp.Data.Models
{
    public class Expense
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Transaction { get; set; }
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date { get; set; }
        public float Amount { get; set; }
    }
}
