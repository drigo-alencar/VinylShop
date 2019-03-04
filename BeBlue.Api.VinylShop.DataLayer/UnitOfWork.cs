using BeBlue.Api.VinylShop.DataLayer.Repositories;

namespace BeBlue.Api.VinylShop.DataLayer
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IMongoContext context;

		public UnitOfWork(IMongoContext context)
		{
			if (context == null) { throw new System.ArgumentNullException(nameof(context)); }
			this.context = context;
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
	}
}