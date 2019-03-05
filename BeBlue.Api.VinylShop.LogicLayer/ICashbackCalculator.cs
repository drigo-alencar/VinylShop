using BeBlue.Api.VinylShop.DomainModel;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.LogicLayer
{
	public interface ICashbackCalculator
	{
		Task<double> ApplyCashback(Album album);
	}
}