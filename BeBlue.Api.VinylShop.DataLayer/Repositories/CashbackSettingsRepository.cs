using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public class CashbackSettingsRepository : ICashbackSettingsRepository
	{
		private const string CASHBACK_SETTINGS_COLLECTION = "CashbackSettings";

		private readonly IMongoDatabase database;

		public CashbackSettingsRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public async Task BulkInsertAsync(IList<GenreCashbackSettings> genresCashbackSettings)
		{
			var filter = Builders<GenreCashbackSettings>.Filter.In(g => g.Genre, genresCashbackSettings.Select(x => x.Genre));

			foreach (var genreCashbackSetting in genresCashbackSettings)
			{
				await this.database.GetCollection<GenreCashbackSettings>(CASHBACK_SETTINGS_COLLECTION).ReplaceOneAsync(filter, genreCashbackSetting);
			}
		}

		public async Task<GenreCashbackSettings> GetByGenre(Genres genre)
		{
			var filter = Builders<GenreCashbackSettings>.Filter.Eq(g => g.Genre, genre);

			return await this.database.GetCollection<GenreCashbackSettings>(CASHBACK_SETTINGS_COLLECTION).Find(filter).FirstOrDefaultAsync();
		}
	}
}
