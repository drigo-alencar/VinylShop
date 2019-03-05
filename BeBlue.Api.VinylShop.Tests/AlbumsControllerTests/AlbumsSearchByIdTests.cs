using AutoFixture;
using BeBlue.Api.VinylShop.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BeBlue.Api.VinylShop.Tests.AlbumsControllerTests
{
	public class AlbumsSearchByIdTests : AlbumsControllerTests
	{
		[Fact]
		public async void Should_return_bad_request_if_searching_with_invalid_id()
		{
			//Arrange -> Act
			var response = await this.controller.Get(null);

			//Assert
			Assert.IsType<BadRequestObjectResult>(response.Result);
		}

		[Fact]
		public async void Should_return_ok_response_if_searching_with_valid_id()
		{
			//Arrange
			var album = this.fixture.Create<Album>();
			this.unitOfWork.AlbumsRepository.GetByIdAsync(Arg.Any<string>()).Returns(album);

			//Act
			var response = (await this.controller.Get(album.Id)).Result as OkObjectResult;
			var result = response.Value as Album;

			//Assert
			Assert.NotNull(response);
			Assert.IsType<Album>(result);
		}

		[Fact]
		public async void Should_call_album_repository_get_by_id_async()
		{
			//Arrange
			var id = this.fixture.Create<string>();

			//Act
			var response = await this.controller.Get(id);

			//Assert
			await this.unitOfWork.AlbumsRepository.Received().GetByIdAsync(Arg.Any<string>());
		}

		[Fact]
		public async void Should_return_the_album_with_the_same_id()
		{
			//Arrange
			var album = this.fixture.Create<Album>();

			this.unitOfWork.AlbumsRepository.GetByIdAsync(Arg.Any<string>()).Returns(album);

			//Act
			var response = (await this.controller.Get(album.Id)).Result as OkObjectResult;

			//Assert
			Assert.Equal(album, response.Value);
		}

		[Fact]
		public async void Should_return_internal_server_error_if_any_error_occur()
		{
			//Arrange
			var id = this.fixture.Create<string>();

			this.unitOfWork.AlbumsRepository.GetByIdAsync(Arg.Any<string>()).Throws(new Exception());

			//Act
			var response = (await this.controller.Get(id)).Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
			Assert.NotNull(response.Value);
		}
	}
}
