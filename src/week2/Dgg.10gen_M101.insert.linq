<Query Kind="Statements">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/school"));

MongoCollection<BsonDocument> people = db.GetCollection("people");

var richard = new
{
	name = "Richard Kreuter",
	company = "10gen",
	interests = new[]{"horses", "skydiving", "fencing"}
};

var andrew = new BsonDocument();
andrew["_id"] = "erlichson";
andrew["name"] = "Andrew Erlichson";
andrew["company"] = "10gen";
andrew["interests"] = new BsonArray(new[]{"running", "cycling", "photography"});
andrew.Dump("before", 3);

people.Insert(richard);
people.Insert(andrew);

andrew.Dump("after", 3);

people.FindAll()
	.Dump("inserted people", 2);