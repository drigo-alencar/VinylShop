using AutoFixture;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.Presentation.Controllers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BeBlue.Api.VinylShop.Tests.SalesControllerTests
{
	public class SalesControllerTests
	{
		protected SalesController controller;
		protected Fixture fixture;
		protected IUnitOfWork unitOfWork;

		public SalesControllerTests()
		{
			this.fixture = new Fixture();

			this.unitOfWork = Substitute.For<IUnitOfWork>();

			this.controller = new SalesController(this.unitOfWork);
		}

		[Fact]
		public void Attempt_to_create_sales_controller_without_unit_of_work_must_throw_an_argument_null_exception()
		{
			//Arrange -> Act
			var result = Record.Exception(() => new SalesController(null));

			//Assert
			Assert.IsType<ArgumentNullException>(result);
		}
	}
}
