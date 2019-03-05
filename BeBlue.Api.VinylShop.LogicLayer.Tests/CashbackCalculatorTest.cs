using AutoFixture;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace BeBlue.Api.VinylShop.LogicLayer.Tests
{
	public class CashbackCalculatorTest
	{
		private CashbackCalculator cashbackCalculator;
		private Fixture fixture;
		private IUnitOfWork unitOfWork;

		public CashbackCalculatorTest()
		{
			this.fixture = new Fixture();
			this.unitOfWork = Substitute.For<IUnitOfWork>();

			this.cashbackCalculator = new CashbackCalculator(this.unitOfWork);
		}

		[Fact]
		public void An_attempt_to_create_cashback_calculator_withou_unit_of_work_should_throw_argument_null_exception()
		{
			//Arrange -> Act
			var result = Record.Exception(() =>
				new CashbackCalculator(null)
			);

			//Assert
			Assert.IsType<ArgumentNullException>(result);
		}

		[Fact]
		public async void Given_an_album_must_calculate_today_cashback()
		{
			//Arrange
			var album = this.fixture.Create<Album>();

			this.fixture.Customize<GenreCashbackSettings>(g => g.With(x => x.Genre, album.Genre));
			var genreCashbackSetting = this.fixture.Create<GenreCashbackSettings>();

			this.unitOfWork.CashbackSettingsRepository.GetByGenreAsync(Arg.Any<Genres>()).Returns(genreCashbackSetting);

			//Act
			var cashback = await this.cashbackCalculator.ApplyCashback(album);

			//Assert
			var todayCashback = genreCashbackSetting.Cashbacks.FirstOrDefault(x => x.DayOfWeek == DateTime.Today.DayOfWeek).Value;
			Assert.Equal((album.Price * todayCashback) / 100, cashback);
		}

		[Fact]
		public async void Given_an_album_if_there_is_no_cashback_for_requested_day_of_week_should_return_zero()
		{
			//Arrange
			var dayOfWeek = this.fixture.Create<DayOfWeek>();
			this.fixture.Customize<Cashback>(c => c.With(x => x.DayOfWeek, DayOfWeek.Friday));

			var album = this.fixture.Create<Album>();

			this.fixture.Customize<GenreCashbackSettings>(g => g.With(x => x.Genre, album.Genre));
			var genreCashbackSetting = this.fixture.Create<GenreCashbackSettings>();

			this.unitOfWork.CashbackSettingsRepository.GetByGenreAsync(Arg.Any<Genres>()).Returns(genreCashbackSetting);

			//Act
			var cashback = await this.cashbackCalculator.ApplyCashback(album);

			//Assert
			var todayCashback = genreCashbackSetting.Cashbacks.FirstOrDefault(x => x.DayOfWeek == dayOfWeek)?.Value;
			Assert.Equal(0, cashback);
		}

		[Fact]
		public async void Call_apply_cashback_without_a_album_as_parameter_should_throw_argument_null_exception()
		{
			//Arrange -> Act
			var result = await Record.ExceptionAsync(() =>
				this.cashbackCalculator.ApplyCashback(null)
			);

			//Assert
			Assert.IsType<ArgumentNullException>(result);
		}
	}
}
