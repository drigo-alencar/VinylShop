using AutoFixture;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.Presentation.Controllers;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlue.Api.VinylShop.Tests.AlbumsControllerTests
{
	public class AlbumsControllerTests
	{
		protected AlbumsController controller;
		protected Fixture fixture;
		protected IUnitOfWork unitOfWork;

		public AlbumsControllerTests()
		{
			this.fixture = new Fixture();
			this.unitOfWork = Substitute.For<IUnitOfWork>();

			this.controller = new AlbumsController(this.unitOfWork);
		}
	}
}
