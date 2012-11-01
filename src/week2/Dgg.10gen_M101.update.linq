<Query Kind="Program">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

void Main()
{
	MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/school"));
	
	MongoCollection<BsonDocument> scores = db.GetCollection("scores");
	
	IMongoQuery homework = Query.And(
			Query.EQ("student_id", 1),
			Query.EQ("type", "homework"));
			
	removeReviewDate(scores);
	
	Save(scores, homework);
		
	removeReviewDate(scores);
	
	Wholesale(scores, homework);
	
	removeReviewDate(scores);
	
	Selective(scores, homework);
}

// Define other methods and classes here

private void removeReviewDate(MongoCollection<BsonDocument> scores)
{
	var result = scores.Update(Query.Null, Update.Unset("review_date"), UpdateFlags.Multi);
	result.Dump();
}

private void Save(MongoCollection<BsonDocument> scores, IMongoQuery homework)
{
	var score = scores.FindOne(homework);
	score.Dump("before Save()", 1);
	score["review_date"] = DateTime.UtcNow;
	scores.Save(score);
	score.Dump("after Save()", 1);
		
	scores.Find(homework)
		.Dump("saved score", 2);
}

private void Wholesale(MongoCollection<BsonDocument> scores, IMongoQuery homework)
{
	var score = scores.FindOne(homework);
	score.Dump("before wholesale Update()", 1);
	
	score["review_date"] = DateTime.UtcNow;
	scores.Update(homework, Update.Replace(score));
	score.Dump("after wholesale Update()", 1);
	
	scores.Find(homework)
		.Dump("wholesale updated score", 2);
}

private void Selective(MongoCollection<BsonDocument> scores, IMongoQuery homework)
{
	var score = scores.FindOne(homework);
	score.Dump("before selective Update()", 1);
	
	scores.Update(homework, Update.Set("review_date", DateTime.UtcNow));
	
	scores.Find(homework)
		.Dump("selective updated score", 2);
}
