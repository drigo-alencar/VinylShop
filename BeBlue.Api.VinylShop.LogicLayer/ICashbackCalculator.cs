using System.Threading.Tasks;
using BeBlue.Api.VinylShop.DomainModel;

namespace BeBlue.Api.VinylShop.LogicLayer
{
	public interface ICashbackCalculator
	{
		Task<double> ApplyCashback(Album album);
	}
}