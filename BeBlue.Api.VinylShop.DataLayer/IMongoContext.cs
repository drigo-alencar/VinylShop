using MongoDB.Driver;

namespace BeBlue.Api.VinylShop.DataLayer
{
	public interface IMongoContext
	{
		IMongoDatabase Database { get; }
	}
}