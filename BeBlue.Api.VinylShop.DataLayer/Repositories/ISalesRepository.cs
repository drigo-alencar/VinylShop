using BeBlue.Api.VinylShop.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface ISalesRepository
	{
		Task<IList<Sale>> GetByDatesAsync(DateTime startDate, DateTime endDate, int offset, int limit);

		Task<Sale> GetByIdAsync(string id);

		Task SaveAsync(Sale sale);
	}
}