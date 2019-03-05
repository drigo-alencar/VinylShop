using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace BeBlue.Api.VinylShop.DomainModel
{
	public class Sale
	{
		public Sale()
		{
			this.Albums = new List<Album>();
		}

		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public string Id { get; set; }

		public IList<Album> Albums { get; set; }

		public double TotalCashback { get; set; }

		public double TotalPrice { get; set; }
	}
}