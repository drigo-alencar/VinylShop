using BeBlue.Api.VinylShop.DataLayer.Repositories;

namespace BeBlue.Api.VinylShop.DataLayer
{
	public interface IUnitOfWork
	{
		IAlbumsRepository AlbumsRepository { get; }

		ICashbackSettingsRepository CashbackSettingsRepository { get; }

		ISalesRepository SalesRepository { get; }
	}
}