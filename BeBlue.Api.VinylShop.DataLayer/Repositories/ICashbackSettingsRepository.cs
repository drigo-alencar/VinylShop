using BeBlue.Api.VinylShop.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface ICashbackSettingsRepository
	{
		Task InsertAsync(GenreCashbackSettings genreCashbackSetting);

		Task<GenreCashbackSettings> GetByGenreAsync(Genres genre);
	}
}