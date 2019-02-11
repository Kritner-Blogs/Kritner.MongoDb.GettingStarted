using MongoDB.Driver;
using System;

namespace Kritner.MongoDb.GettingStarted.ConsoleApp
{
    public class MyHelloWorldMongoThing
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string MyDatas { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What would you like to enter for MyDatas?");

            // Get some input from user
            var myDatas = Console.ReadLine();

            // Create an object with the data that was entered
            var obj = new MyHelloWorldMongoThing()
            {
                MyDatas = myDatas
            };

            // get a mongoclient using the default connection string
            var mongo = new MongoClient();

            // get (and create if doesn't exist) a database from the mongoclient
            var db = mongo.GetDatabase("MyHelloWorldDb");

            // get a collection of MyHelloWorldMongoThings (and create if it doesn't exist)
            var collection = db.GetCollection<MyHelloWorldMongoThing>("myHelloWorldCollection");

            // Count the items in the collection prior to insert
            // Using an empty filter so that everything is considered in the filter.
            var count = collection.CountDocuments(new FilterDefinitionBuilder<MyHelloWorldMongoThing>().Empty);
            Console.WriteLine($"Number of items in the collection before insert: {count}");

            // Add the entered item to the collection
            collection.InsertOne(obj);

            // Count the items in the collection post insert
            count = collection.CountDocuments(new FilterDefinitionBuilder<MyHelloWorldMongoThing>().Empty);
            Console.WriteLine($"Number of items in the collection after insert: {count}");
        }
    }
}
