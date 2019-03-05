using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

		[HttpGet]
		public async Task<ActionResult<IList<Sale>>> Get(DateTime startDate, DateTime endDate, int offset = 0, int limit = 50)
		{
			if (offset < 0) { return this.BadRequest(BadRequestMessages.OffsetMustBeAPositiveNumber); }
			if (limit < 0 || limit > 500) { return this.BadRequest(BadRequestMessages.LimitMustBeBetweenZeroAndFiftyHundred); }

			try
			{
				var sales = await this.unitOfWork.SalesRepository.GetByDatesAsync(startDate, endDate, offset, limit);

				return this.Ok(sales);
			}
			catch (Exception e)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
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
