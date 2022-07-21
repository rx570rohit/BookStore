class : https://meet.google.com/afd-nmrn-dzi

https://xd.adobe.com/view/d096df37-ca37-4026-553f-8cfa6bec09ec-a6a7/grid

Mongodb-BookStore

cluster/DB_Name-BookStoreAppDB

UserName-Rohit
password-Rx@570soni


*******************************************mongodb to .net *****************************************************

1 To connect to a MongoDB instance at its default port 27017, you can use the default constructor of the MongoClient class as shown below.

var client = new MongoClient();
 
2.
  Create a database and collection
The following code listing shows how you can create a database and a collection inside it and then insert an object inside the collection.

static void Main(string[] args)
    {           
        var connectionString ="mongodb://localhost:27017";
        var client = new MongoClient(connectionString);           
        IMongoDatabase db = client.GetDatabase(“IDG”);
        Author author = new Author
        {
            Id = 1,
            FirstName ="Joydip",
            LastName ="Kanjilal"
        };
        var collection = db.GetCollection<Author>(“authors”);
        collection.InsertOne(author);
        Console.Read();
    }


display the names of the databases available in the instance of MongoDB running in your system.

var connectionString ="mongodb://localhost:27017";
var client = new MongoClient(connectionString);           
    using (var cursor = client.ListDatabases())
    {
        var databaseDocuments = cursor.ToList();
        foreach (var db in databaseDocuments)
        {
            Console.WriteLine(db[“name”].ToString());
        }
    }


*****************************auth*************
https://stackoverflow.com/questions/44513786/error-on-mongodb-authentication




{
  "password": "Rx@570rrr",
  "confirmPassword": "Rx@570rrr"
}