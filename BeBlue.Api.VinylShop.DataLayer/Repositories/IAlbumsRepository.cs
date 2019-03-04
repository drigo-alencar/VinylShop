using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Api.VinylShop.DomainModel;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface IAlbumsRepository
	{
		Task BulkInsertAsync(IEnumerable<Album> albums);

		Task<Album> GetByIdAsync(string id);
	}
}