using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public class SalesRepository : ISalesRepository
	{
		private const string SALES_COLLECTION = "Sales";

		private IMongoDatabase database;

		public SalesRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public async Task<IList<Sale>> GetByDatesAsync(DateTime startDate, DateTime endDate, int offset, int limit)
		{
			var greaterThanStartDate = Builders<Sale>.Filter.Gte(a => a.Date, startDate);
			var lessThanEndDate = Builders<Sale>.Filter.Lte(a => a.Date, endDate);
			var filter = greaterThanStartDate & lessThanEndDate;

			var sort = Builders<Sale>.Sort.Descending(a => a.Date);

			return await this.database.GetCollection<Sale>(SALES_COLLECTION).Find(filter)
				.Sort(sort)
				.Skip(offset)
				.Limit(limit)
				.ToListAsync();
		}

		public async Task<Sale> GetByIdAsync(string id)
		{
			var filter = Builders<Sale>.Filter.Eq(s => s.Id, id);
			return await this.database.GetCollection<Sale>(SALES_COLLECTION).Find(filter).FirstOrDefaultAsync();
		}

		public async Task SaveAsync(Sale sale)
		{
			await this.database.GetCollection<Sale>(SALES_COLLECTION).InsertOneAsync(sale);
		}
	}
}