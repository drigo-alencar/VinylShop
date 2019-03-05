using System;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;

namespace BeBlue.Api.VinylShop.LogicLayer
{
	public class CashbackCalculator : ICashbackCalculator
	{
		private readonly IUnitOfWork unitOfWork;

		public CashbackCalculator(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public async Task<double> ApplyCashback(Album album)
		{
			if (album is null) { throw new ArgumentNullException(nameof(album)); }

			var genreCashback = await this.unitOfWork.CashbackSettingsRepository.GetByGenreAsync(album.Genre);

			var todayApplicableCashback = genreCashback.Cashbacks.FirstOrDefault(c => c.DayOfWeek == DateTime.Today.DayOfWeek);

			if (todayApplicableCashback == null) { return 0; }

			return album.Price * todayApplicableCashback.Value / 100; 
		}
	}
}