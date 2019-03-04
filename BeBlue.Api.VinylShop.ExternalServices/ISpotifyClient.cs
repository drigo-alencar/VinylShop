using BeBlue.Api.VinylShop.ExternalServices.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.ExternalServices
{
	public interface ISpotifyClient
	{
		Task<IReadOnlyList<AlbumResponse>> RetrieveAlbums();
	}
}
