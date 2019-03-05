using BeBlue.Api.VinylShop.DataLayer.Repositories;

namespace BeBlue.Api.VinylShop.DataLayer
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IMongoContext context;

		public UnitOfWork(IMongoContext context)
		{
			this.context = context ?? throw new System.ArgumentNullException(nameof(context));
		}

		private IAlbumsRepository albumsRepository;

		public IAlbumsRepository AlbumsRepository
		{
			get
			{
				if (this.albumsRepository == null) { this.albumsRepository = new AlbumsRepository(this.context.Database); }
				return this.albumsRepository;
			}
		}

		private ICashbackSettingsRepository cashbackSettingsRepository;

		public ICashbackSettingsRepository CashbackSettingsRepository
		{
			get
			{
				if (this.cashbackSettingsRepository == null) { this.cashbackSettingsRepository = new CashbackSettingsRepository(this.context.Database); }
				return this.cashbackSettingsRepository;
			}
		}
	}
}