using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace BeBlue.Api.VinylShop.DomainModel
{
	public class Sale
	{
		public Sale()
		{
			this.Albums = new List<Album>();
		}

		public IList<Album> Albums { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
		public DateTime Date { get; set; }

		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public string Id { get; set; }

		public double TotalCashback { get; set; }

		public double TotalPrice { get; set; }
	}
}