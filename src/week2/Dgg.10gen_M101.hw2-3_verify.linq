<Query Kind="Statements">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/blog?safe=true"));
MongoCollection<BsonDocument> users = db.GetCollection("users");

string userName = "dgg";
users.FindOne(Query.EQ("_id", userName)).Dump(2);
try{
users.Insert(new {_id = "dgg", name = "DGG"}).Dump(2);
}
catch(MongoSafeModeException ex)
{
	ex.Dump("ex", 2);
	if (ex.CommandResult.Response["code"] != 11000) throw;
}

