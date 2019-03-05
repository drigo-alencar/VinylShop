using AutoFixture;
using BeBlue.Api.VinylShop.DomainModel;
using BeBlue.Api.VinylShop.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BeBlue.Api.VinylShop.Tests.CartsControllerTests
{
	public class CreateCartTests : CartsControllerTests
	{
		[Fact]
		public async void Given_a_valid_cart_request_should_return_ok_response_with_sale()
		{
			//Arrange
			var albums = this.fixture.CreateMany<Album>().ToList();
			this.unitOfWork.AlbumsRepository.GetByIdsAsync(Arg.Any<IList<string>>()).Returns(albums);

			var request = new CreateCartRequest { AlbumsIds = albums.Select(a => a.Id).ToList() };

			//Act
			var response = (await this.controller.Post(request)).Result as OkObjectResult;

			//Assert
			Assert.NotNull(response);
			Assert.IsType<Sale>(response.Value);
		}

		[Fact]
		public async void Should_return_not_found_response_if_any_album_id_was_not_present_on_database()
		{
			//Arrange
			var albums = this.fixture.CreateMany<Album>().ToList();
			this.unitOfWork.AlbumsRepository.GetByIdsAsync(Arg.Any<IList<string>>()).Returns(albums);

			var albumsIds = CreateAlbumIdOnlyListWithMoreOneId(albums);

			var request = new CreateCartRequest { AlbumsIds = albumsIds.ToList() };

			//Act
			var response = (await this.controller.Post(request)).Result as NotFoundObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		private IEnumerable<string> CreateAlbumIdOnlyListWithMoreOneId(List<Album> albums)
		{
			var albumsIds = albums.Select(a => a.Id);
			return albumsIds.Append(this.fixture.Create<string>());
		}

		[Fact]
		public async void Should_return_bad_request_response_given_an_null_cart_request()
		{
			//Arrange -> Act
			var response = (await this.controller.Post(null)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		[Fact]
		public async void Should_return_bad_request_response_given_a_cart_request_with_no_list_of_albums_ids()
		{
			//Arrange
			this.fixture.Customize<CreateCartRequest>(c => c.Without(x => x.AlbumsIds));
			var request = this.fixture.Create<CreateCartRequest>();

			//Act
			var response = (await this.controller.Post(request)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		[Fact]
		public async void Should_return_bad_request_response_given_a_cart_request_with_an_empty_list_of_albums_ids()
		{
			//Arrange
			this.fixture.Customize<CreateCartRequest>(c => c.With(x => x.AlbumsIds, new List<string>()));
			var request = this.fixture.Create<CreateCartRequest>();

			//Act
			var response = (await this.controller.Post(request)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}
	}
}
