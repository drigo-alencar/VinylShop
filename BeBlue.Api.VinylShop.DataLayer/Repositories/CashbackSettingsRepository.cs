using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public class CashbackSettingsRepository : ICashbackSettingsRepository
	{
		private readonly IMongoDatabase database;

		public CashbackSettingsRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public async Task BulkInsertAsync(IList<GenreCashbackSettings> genresCashbackSettings)
		{
			await this.database.GetCollection<GenreCashbackSettings>("CashbackSettings").InsertManyAsync(genresCashbackSettings);
		}
	}
}
