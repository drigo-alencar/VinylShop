using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BeBlue.Api.VinylShop.DomainModel
{
	public class Cashback
	{
		[BsonRepresentation(BsonType.String)]
		[JsonConverter(typeof(StringEnumConverter))]
		public DayOfWeek DayOfWeek { get; set; }

		public double Value { get; set; }
	}
}
