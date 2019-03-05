using BeBlue.Api.VinylShop.ExternalServices.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.ExternalServices
{
	public interface ISpotifyClient
	{
		Task<IReadOnlyList<AlbumResponse>> RetrieveAlbums();
	}
}
