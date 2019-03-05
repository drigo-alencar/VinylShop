using BeBlue.Api.VinylShop.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface ICashbackSettingsRepository
	{
		Task BulkInsertAsync(IList<GenreCashbackSettings> genresCashbackSettings);

		Task<GenreCashbackSettings> GetByGenreAsync(Genres genre);
	}
}