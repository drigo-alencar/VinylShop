using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace BeBlue.Api.VinylShop.DomainModel
{
	public class Album
	{
		public IList<string> Artists { get; set; }

		[BsonRepresentation(MongoDB.Bson.BsonType.String)]
		public Genres Genre { get; set; }

		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public string Id { get; set; }

		public string Name { get; set; }

		public string ReleaseDate { get; set; }

		public int Tracks { get; set; }
	}
}