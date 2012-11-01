<Query Kind="Statements">
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

MongoDatabase testDb = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/reddit"));

MongoCollection<BsonDocument> stories = testDb.GetCollection("stories");

var stream = System.Net.HttpWebRequest.Create("http://www.reddit.com/r/technology.json")
	.GetResponse()
	.GetResponseStream();
var doc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(
	new StreamReader(stream).ReadToEnd());
foreach (var child in doc["data"].AsBsonDocument["children"].AsBsonArray)
{
	stories.Insert(child.AsBsonDocument["data"]);
}
