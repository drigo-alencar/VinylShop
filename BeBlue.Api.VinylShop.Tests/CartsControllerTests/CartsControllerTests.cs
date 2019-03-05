using AutoFixture;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.LogicLayer;
using BeBlue.Api.VinylShop.Presentation.Controllers;
using NSubstitute;
using System;
using Xunit;

namespace BeBlue.Api.VinylShop.Tests.CartsControllerTests
{
	public class CartsControllerTests
	{
		protected readonly ICashbackCalculator cashbackCalculator;
		protected CartsController controller;
		protected Fixture fixture;
		protected IUnitOfWork unitOfWork;

		public CartsControllerTests()
		{
			this.cashbackCalculator = Substitute.For<ICashbackCalculator>();

			this.fixture = new Fixture();

			this.unitOfWork = Substitute.For<IUnitOfWork>();

			this.controller = new CartsController(this.cashbackCalculator, this.unitOfWork);
		}

		[Fact]
		public void Attempt_to_create_carts_controller_without_cashback_calculator_must_throw_an_argument_null_exception()
		{
			//Arrange -> Act
			var result = Record.Exception(() => new CartsController(null, this.unitOfWork));

			//Assert
			Assert.IsType<ArgumentNullException>(result);
		}

		[Fact]
		public void Attempt_to_create_carts_controller_without_unit_of_work_must_throw_an_argument_null_exception()
		{
			//Arrange -> Act
			var result = Record.Exception(() => new CartsController(this.cashbackCalculator, null));

			//Assert
			Assert.IsType<ArgumentNullException>(result);
		}
	}
}
