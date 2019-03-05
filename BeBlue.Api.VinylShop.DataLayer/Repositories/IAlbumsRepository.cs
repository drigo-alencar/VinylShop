﻿using BeBlue.Api.VinylShop.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface IAlbumsRepository
	{
		Task BulkInsertAsync(IEnumerable<Album> albums);

		Task<IReadOnlyList<Album>> GetByGenreAsync(string genre, int offset, int limit);

		Task<Album> GetByIdAsync(string id);

		Task<IList<Album>> GetByIdsAsync(IList<string> albumsIds);
	}
}