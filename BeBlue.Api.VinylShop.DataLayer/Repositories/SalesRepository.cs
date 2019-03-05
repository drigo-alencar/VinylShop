using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public class SalesRepository : ISalesRepository
	{
		private IMongoDatabase database;

		public SalesRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public async Task Save(Sale sale)
		{
			await this.database.GetCollection<Sale>("Sales").InsertOneAsync(sale);
		}
	}
}