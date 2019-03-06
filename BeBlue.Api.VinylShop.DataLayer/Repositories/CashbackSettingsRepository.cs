using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task InsertAsync(GenreCashbackSettings genreCashbackSetting)
		{
			var filter = Builders<GenreCashbackSettings>.Filter.Eq(g => g.Genre, genreCashbackSetting.Genre);

			await this.database.GetCollection<GenreCashbackSettings>(CASHBACK_SETTINGS_COLLECTION).ReplaceOneAsync(filter, genreCashbackSetting, new UpdateOptions { IsUpsert = true });
		}

		public async Task<GenreCashbackSettings> GetByGenreAsync(Genres genre)
		{
			var filter = Builders<GenreCashbackSettings>.Filter.Eq(g => g.Genre, genre);

			return await this.database.GetCollection<GenreCashbackSettings>(CASHBACK_SETTINGS_COLLECTION).Find(filter).FirstOrDefaultAsync();
		}
	}
}
