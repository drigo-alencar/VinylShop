using AutoFixture;
using BeBlue.Api.VinylShop.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using Xunit;

namespace BeBlue.Api.VinylShop.Tests.SalesControllerTests
{
	public class SearchSaleByIdTests : SalesControllerTests
	{
		[Fact]
		public async void Given_a_null_sale_id_must_return_bad_request_response()
		{
			//Arrange -> Act
			var response = (await this.controller.Get(null)).Result as BadRequestObjectResult;

			//Assert
			Assert.NotNull(response);
		}

		[Fact]
		public async void Given_a_valid_sale_id_must_return_a_sale_as_response()
		{
			//Arrange 
			var sale = this.fixture.Create<Sale>();

			this.unitOfWork.SalesRepository.GetByIdAsync(Arg.Any<string>()).Returns(sale);

			//Act
			var response = (await this.controller.Get(sale.Id)).Result as OkObjectResult;

			//Assert
			Assert.NotNull(response);
			Assert.IsType<Sale>(response.Value);
		}

		[Fact]
		public async void Given_a_valid_sale_id_must_call_sales_repository_to_retrieve_it()
		{
			//Arrange 
			var sale = this.fixture.Create<Sale>();

			this.unitOfWork.SalesRepository.GetByIdAsync(Arg.Any<string>()).Returns(sale);

			//Act
			var response = (await this.controller.Get(sale.Id)).Result as OkObjectResult;

			//Assert
			await this.unitOfWork.SalesRepository.Received().GetByIdAsync(Arg.Any<string>());
		}

		[Fact]
		public async void Given_a_valid_sale_id_if_any_error_occur_should_return_internal_server_error()
		{
			//Arrange 
			var id = this.fixture.Create<string>();

			this.unitOfWork.SalesRepository.GetByIdAsync(Arg.Any<string>()).Throws(new Exception());

			//Act
			var response = (await this.controller.Get(id)).Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
		}
	}
}
