using MongoDB.Driver;

namespace BeBlue.Api.VinylShop.DataLayer
{
	public class MongoContext : IMongoContext
	{
		public MongoContext(MongoSettings mongoSettings)
		{
			var mongoClient = new MongoClient($"mongodb://{mongoSettings.Host}:{mongoSettings.Port}");
			this.Database = mongoClient.GetDatabase(mongoSettings.Database);
		}

		public IMongoDatabase Database { get; }
	}
}
