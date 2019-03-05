using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using BeBlue.Api.VinylShop.ExternalServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.Presentation
{
	public class CatalogBootstrapper
	{
		private readonly ISpotifyClient spotifyClient;
		private readonly IUnitOfWork unitOfWork;

		public CatalogBootstrapper(ISpotifyClient spotifyClient, IUnitOfWork unitOfWork)
		{
			this.spotifyClient = spotifyClient;
			this.unitOfWork = unitOfWork;

			this.Execute().Wait();
		}

		private async Task Execute()
		{
			var spotifyAlbums = await this.spotifyClient.RetrieveAlbums();

			var albums = spotifyAlbums.Select(x => new Album
			{
				Artists = x.Artists.Select(a => a.Name).ToList(),
				Genre = x.Genre,
				Name = x.Name,
				Price = new Random().NextDouble(),
				ReleaseDate = x.ReleaseDate,
				Tracks = x.TotalTracks
			});

			await this.unitOfWork.AlbumsRepository.BulkInsertAsync(albums);
		}
	}
}