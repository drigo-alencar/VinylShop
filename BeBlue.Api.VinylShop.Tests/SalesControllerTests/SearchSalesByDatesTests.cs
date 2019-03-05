using AutoFixture;
using BeBlue.Api.VinylShop.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BeBlue.Api.VinylShop.Tests.SalesControllerTests
{
	public class SearchSalesByDatesTests : SalesControllerTests
	{
		[Fact]
		public async void Given_a_valid_request_should_get_sales_from_sales_repository()
		{
			//Arrange
			var startDate = this.fixture.Create<DateTime>();
			var endDate = this.fixture.Create<DateTime>();

			//Act
			var response = (await this.controller.Get(startDate, endDate)).Result as OkObjectResult;

			//Assert
			await this.unitOfWork.SalesRepository.Received().GetByDatesAsync(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>(), Arg.Any<int>());
		}

		[Fact]
		public async void Given_a_valid_request_should_return_a_list_of_sales()
		{
			//Arrange
			var startDate = this.fixture.Create<DateTime>();
			var endDate = this.fixture.Create<DateTime>();

			var sales = this.fixture.CreateMany<Sale>().ToList();

			this.unitOfWork.SalesRepository
				.GetByDatesAsync(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>(), Arg.Any<int>())
				.Returns(sales);

			//Act
			var response = (await this.controller.Get(startDate, endDate)).Result as OkObjectResult;

			//Assert
			Assert.NotNull(response);
			Assert.IsType<List<Sale>>(response.Value);
			Assert.NotEmpty(response.Value as IList<Sale>);
		}

		[Fact]
		public async void Given_a_valid_request_should_if_an_error_occur_should_return_internal_server_error()
		{
			//Arrange
			var startDate = this.fixture.Create<DateTime>();
			var endDate = this.fixture.Create<DateTime>();

			this.unitOfWork.SalesRepository
				.GetByDatesAsync(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<int>(), Arg.Any<int>())
				.Throws(new Exception());


			//Act
			var response = (await this.controller.Get(startDate, endDate)).Result as ObjectResult;

			//Assert
			Assert.NotNull(response);
			Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
		}

		[Fact]
		public async void Given_a_request_with_offset_negative_should_return_bad_request_response()
		{
			//Arrange
			var startDate = this.fixture.Create<DateTime>();
			var endDate = this.fixture.Create<DateTime>();

			//Act
			var response = (await this.controller.Get(startDate, endDate, Int32.MinValue)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		[Fact]
		public async void Given_a_request_with_limit_greater_than_500_should_return_bad_request_response()
		{
			//Arrange
			var startDate = this.fixture.Create<DateTime>();
			var endDate = this.fixture.Create<DateTime>();

			//Act
			var response = (await this.controller.Get(startDate, endDate, limit: 501)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		[Fact]
		public async void Given_a_request_with_negative_limit_should_return_bad_request_response()
		{
			//Arrange
			var startDate = this.fixture.Create<DateTime>();
			var endDate = this.fixture.Create<DateTime>();

			//Act
			var response = (await this.controller.Get(startDate, endDate, limit: Int32.MinValue)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}
	}
}
