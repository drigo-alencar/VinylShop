using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SalesController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public SalesController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		[HttpGet("{id}", Name = "GetSaleById")]
		public async Task<ActionResult<Sale>> Get(string id)
		{
			if (String.IsNullOrWhiteSpace(id)) { return this.BadRequest(BadRequestMessages.MustProvideSaleId); }

			try
			{
				var sale = await this.unitOfWork.SalesRepository.GetByIdAsync(id);

				return this.Ok(sale);
			}
			catch (Exception e)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}
