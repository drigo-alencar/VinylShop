using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace BeBlue.Api.VinylShop.DomainModel
{
	public class GenreCashbackSettings
	{
		[BsonIgnoreIfNull]
		[BsonId]
		public BsonObjectId Id { get; set; }

		[BsonRepresentation(BsonType.String)]
		[JsonConverter(typeof(StringEnumConverter))]
		public Genres Genre { get; set; }

		public IList<Cashback> Cashbacks { get; set; }
	}
}
