<Query Kind="Program">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
  <Namespace>MongoDB.Driver.Linq</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Attributes</Namespace>
</Query>

void Main()
{
	MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/school?safe=true"));

	var students = db.GetCollection<Student>("students");

	foreach (Student student in students.FindAll())
	{
		var withMinValue = student.Scores
			.Where(s => s.Type == "homework")
			.MinBy(s => s.Value);
		
		student.Scores.Remove(withMinValue);
		students.Save(student);
	}
}

// Define other methods and classes here
public class Student
{
	[BsonIdAttribute]
	public int Id {get; set;}
	[BsonElement("name")]
	public string Name { get; set; }
	[BsonElement("scores")]
	public List<Score> Scores { get; set; }
}

public class Score
{
	[BsonElement("type")]
	public string Type { get; set; }
	[BsonElement("score")]
	public double Value { get; set; }
}


public static class MoreEnumerable
{
	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> selector)
	{
		return source.MinBy(selector, Comparer<TKey>.Default);
	}
	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
		   Func<TSource, TKey> selector, IComparer<TKey> comparer)
	{
		using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
		{
			if (!sourceIterator.MoveNext())
			{
				throw new InvalidOperationException("Sequence was empty");
			}
			TSource min = sourceIterator.Current;
			TKey minKey = selector(min);
			while (sourceIterator.MoveNext())
			{
				TSource candidate = sourceIterator.Current;
				TKey candidateProjected = selector(candidate);
				if (comparer.Compare(candidateProjected, minKey) < 0)
				{
					min = candidate;
					minKey = candidateProjected;
				}
			}
			return min;
		}
	}
}