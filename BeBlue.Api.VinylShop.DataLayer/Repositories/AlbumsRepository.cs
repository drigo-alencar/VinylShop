using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public class AlbumsRepository : IAlbumsRepository
	{
		private IMongoDatabase database;

		public AlbumsRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public Task<Album> GetByIdAsync(string id)
		{
			return null;
		}

		public async Task BulkInsertAsync(IEnumerable<Album> albums)
		{
			await this.database.GetCollection<Album>("Albums").InsertManyAsync(albums);
		}
	}
}