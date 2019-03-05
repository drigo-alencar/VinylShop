using BeBlue.Api.VinylShop.DomainModel;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.DataLayer.Repositories
{
	public interface ISalesRepository
	{
		Task Save(Sale sale);
	}
}