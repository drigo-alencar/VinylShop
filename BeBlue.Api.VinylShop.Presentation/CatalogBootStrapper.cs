using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using BeBlue.Api.VinylShop.ExternalServices;
using Microsoft.Extensions.Hosting;

namespace BeBlue.Api.VinylShop.Presentation
{
	public class CatalogBootStrapper
	{
		private ISpotifyClient spotifyClient;
		private IUnitOfWork unitOfWork;

		public CatalogBootStrapper(ISpotifyClient spotifyClient, IUnitOfWork unitOfWork)
		{
			this.spotifyClient = spotifyClient;
			this.unitOfWork = unitOfWork;

			this.Execute().Wait();
		}

		protected async Task Execute()
		{
			var spotifyAlbums = await this.spotifyClient.RetrieveAlbums();

			var albums = spotifyAlbums.Select(x => new Album
			{
				Name = x.Name,
				ReleaseDate = x.ReleaseDate,
				Artists = x.Artists.Select(a => a.Name).ToList(),
				Genre = x.Genre,
				Tracks = x.TotalTracks
			});

			await this.unitOfWork.AlbumsRepository.BulkInsertAsync(albums);
		}
	}
}