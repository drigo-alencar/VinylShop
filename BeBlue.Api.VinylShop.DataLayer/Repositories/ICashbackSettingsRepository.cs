using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Bson;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface ICashbackSettingsRepository
	{
		Task BulkInsertAsync(IList<GenreCashbackSettings> genresCashbackSettings);

		Task<GenreCashbackSettings> GetByGenreAsync(Genres genre);
	}
}