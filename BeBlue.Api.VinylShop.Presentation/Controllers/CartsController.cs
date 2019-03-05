using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using BeBlue.Api.VinylShop.LogicLayer;
using BeBlue.Api.VinylShop.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartsController : Controller
	{
		private readonly ICashbackCalculator cashbackCalculator;
		private readonly IUnitOfWork unitOfWork;

		public CartsController(ICashbackCalculator cashbackCalculator, IUnitOfWork unitOfWork)
		{
			this.cashbackCalculator = cashbackCalculator ?? throw new ArgumentNullException(nameof(cashbackCalculator));
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		[HttpPost]
		public async Task<ActionResult<Sale>> Post(CreateCartRequest request)
		{
			if (request is null) { return this.BadRequest(BadRequestMessages.CartRequestCantBeNull); }
			if (request.AlbumsIds is null || request.AlbumsIds.Any() == false) { return this.BadRequest(BadRequestMessages.CartMustHaveAListWithAlbumsIds); }

			var foundAlbums = await this.unitOfWork.AlbumsRepository.GetByIdsAsync(request.AlbumsIds);
			var albumsNotFound = this.CheckIfAnyAlbumWasNotFound(request.AlbumsIds, foundAlbums);

			if (albumsNotFound.Any())
			{
				var message = String.Format(NotFoundMessages.OneOrMoreAlbumsCantBeFound, String.Join(", ", albumsNotFound));
				return this.NotFound(message);
			}

			var sale = await this.CreateSale(foundAlbums);
			await this.unitOfWork.SalesRepository.SaveAsync(sale);

			return this.CreatedAtRoute("GetSaleById", new { sale.Id }, sale);
		}

		private async Task<Sale> CreateSale(IList<Album> foundAlbums)
		{
			var sale = new Sale();

			foreach (var album in foundAlbums)
			{
				var cashback = await this.cashbackCalculator.ApplyCashback(album);
				sale.Albums.Add(album);
				sale.Date = DateTime.Today;
				sale.TotalPrice += album.Price;
				sale.TotalCashback += cashback;
			}

			return sale;
		}

		private IEnumerable<string> CheckIfAnyAlbumWasNotFound(IList<string> albumsIds, IList<Album> foundAlbums)
		{
			var foundAlbumsIds = foundAlbums.Select(fa => fa.Id);
			return albumsIds.Except(foundAlbumsIds);
		}
	}
}
