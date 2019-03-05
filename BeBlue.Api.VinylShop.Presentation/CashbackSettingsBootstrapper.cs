using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.Presentation
{
	public class CashbackSettingsBootstrapper
	{
		private const string GENRE_CASHBACK_SETTINGS_FILE = @"Settings\GenresCashbackSettings.json";

		private readonly IUnitOfWork unitOfWork;

		public CashbackSettingsBootstrapper(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			this.Execute().Wait();
		}

		private async Task Execute()
		{
			var cashbackSettingsFile = File.ReadAllText(GENRE_CASHBACK_SETTINGS_FILE);
			var cashbackSettings = JsonConvert.DeserializeObject<IList<GenreCashbackSettings>>(cashbackSettingsFile);
			await this.unitOfWork.CashbackSettingsRepository.BulkInsertAsync(cashbackSettings);
		}
	}
}
