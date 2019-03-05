﻿using BeBlue.Api.VinylShop.DomainModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public class AlbumsRepository : IAlbumsRepository
	{
		private const string ALBUMS_COLLECTION = "Albums";

		private IMongoDatabase database;

		public AlbumsRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public async Task BulkInsertAsync(IEnumerable<Album> albums)
		{
			await this.database.GetCollection<Album>(ALBUMS_COLLECTION).InsertManyAsync(albums);
		}

		public async Task<IReadOnlyList<Album>> GetByGenreAsync(string genre, int offset, int limit)
		{
			var filter = Builders<Album>.Filter.Eq(a => a.Genre, Enum.Parse<Genres>(genre.ToUpperInvariant()));
			var sort = Builders<Album>.Sort.Ascending(a => a.Name);

			return await this.database.GetCollection<Album>(ALBUMS_COLLECTION).Find(filter)
				.Sort(sort)
				.Skip(offset)
				.Limit(limit)
				.ToListAsync();
		}

		public async Task<Album> GetByIdAsync(string id)
		{
			var filter = Builders<Album>.Filter.Eq(a => a.Id, id);
			return await this.database.GetCollection<Album>(ALBUMS_COLLECTION).Find(filter).FirstOrDefaultAsync();
		}

		public async Task<IList<Album>> GetByIdsAsync(IList<string> albumsIds)
		{
			var filter = Builders<Album>.Filter.In(a => a.Id, albumsIds);

			return await this.database.GetCollection<Album>(ALBUMS_COLLECTION).Find(filter).ToListAsync();
		}
	}
}