using System;
using System.Collections.Generic;
using System.Linq;
using MongoConsoleApp.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using PeanutButter.RandomGenerators;

namespace MongoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoClient = new MongoClient();
            var db = mongoClient.GetDatabase("Expense");
            var coll = db.GetCollection<Category>("Category");

            InsertOneAsyncTo(coll);

            Console.WriteLine("Please enter category name : ");
            var name = Console.ReadLine();
            
            FindAllAndPrint(coll, name);

            Console.ReadLine();
        }

        private static void FindAllAndPrint(IMongoCollection<Category> coll, string name)
        {
            var results = coll.Find(t => t.Name == name)
                              .Limit(10)
                              .ToListAsync().Result;

            Console.Write("List of categories\n**************************\n");

            foreach (var result in results)
            {
                Console.WriteLine("Name : " + result.Name);
                Console.WriteLine("Description : " + result.Description);
                Console.WriteLine();
               
            }

            foreach (var expensse in results.SelectMany(t => t.Expenses))
            {
                Console.WriteLine("Amount : " + expensse.Amount);
                Console.WriteLine("Date : " + expensse.Date);
                Console.WriteLine("Transaction : " + expensse.Transaction);

            }         
        }

        private static void InsertOneAsyncTo(IMongoCollection<Category> coll)
        {
            var category = new Category
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Category 1",
                Description = "Category Description",
                Expenses = new List<Expense>
                {
                    new Expense
                    {
                        Id = ObjectId.GenerateNewId(),
                       Amount = 12,
                       Date = DateTime.Today,
                       Transaction = RandomValueGen.GetRandomString(5)
                    }
                }
            };

            coll.InsertOneAsync(category).Wait();
        }

    }
}
