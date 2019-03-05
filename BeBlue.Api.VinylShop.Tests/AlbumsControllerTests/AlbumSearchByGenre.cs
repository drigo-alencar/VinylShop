using BeBlue.Api.VinylShop.DomainModel;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Http;

namespace BeBlue.Api.VinylShop.Tests.AlbumsControllerTests
{
	public class AlbumSearchByGenre : AlbumsControllerTests
	{
		[Fact]
		public async void Should_return_internal_server_error_if_any_error_occur()
		{
			//Arrange
			string genre = MockGenre();

			this.unitOfWork.AlbumsRepository.GetByGenreAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Throws(new Exception());

			//Act
			var response = (await this.controller.Get(genre, offset: 0)).Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
			Assert.NotNull(response.Value);
		}

		[Fact]
		public async void Should_return_bad_request_if_genre_is_not_specified()
		{
			//Arrange -> Act
			var response = (await this.controller.Get(null, offset: 0)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		[Fact]
		public async void Should_call_album_repository_get_by_genre_async()
		{
			//Arrange
			string genre = MockGenre();

			//Act
			var response = (await this.controller.Get(genre, offset: 0)).Result as OkObjectResult;
			var result = response.Value as IList<Album>;

			//Assert
			await this.unitOfWork.AlbumsRepository.Received().GetByGenreAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
		}

		[Fact]
		public async void Should_return_ok_response_if_only_genre_is_specified()
		{
			//Arrange
			string genre = MockGenre();

			var albums = this.fixture.CreateMany<Album>();
			this.unitOfWork.AlbumsRepository.GetByGenreAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(albums.ToList());

			//Act
			var response = (await this.controller.Get(genre, offset: 0)).Result as OkObjectResult;
			var result = response.Value as IList<Album>;

			//Assert
			Assert.NotNull(response);
			Assert.NotEmpty(result);
		}

		private static string MockGenre()
		{
			var genres = Enum.GetValues(typeof(Genres));
			var randomized = genres.GetValue(new Random().Next(genres.Length));
			return Enum.GetName(typeof(Genres), randomized);
		}
	}
}
