<Query Kind="Statements">
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
</Query>

MongoDatabase testDb = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/school"));

MongoCollection<BsonDocument> scores = testDb.GetCollection("scores");

var allScores = scores.FindAll().SetLimit(20).Dump("all scores", 2);
var score = scores.FindOne().Dump("one", 1);