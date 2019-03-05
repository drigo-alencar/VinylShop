using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Api.VinylShop.DataLayer;
using BeBlue.Api.VinylShop.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeBlue.Api.VinylShop.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlbumsController : Controller
	{
		private const int DEFAULT_OFFSET = 0;
		private const int DEFAULT_PAGE_SIZE = 50;
		private const int MAXIMUM_PAGE_SIZE = 500;

		private IUnitOfWork unitOfWork;

		public AlbumsController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Album>> Get(string id)
		{
			if (String.IsNullOrWhiteSpace(id)) { return this.BadRequest(BadRequestMessages.MustProvideAlbumId); }

			try
			{
				var album = await this.unitOfWork.AlbumsRepository.GetByIdAsync(id);

				return this.Ok(album);
			}
			catch (Exception e)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpGet("genres")]
		public async Task<ActionResult<IList<Album>>> Get(string genre, int offset = DEFAULT_OFFSET, int limit = DEFAULT_PAGE_SIZE)
		{
			if (String.IsNullOrWhiteSpace(genre)) { return this.BadRequest(BadRequestMessages.MustProvideGenre); }
			if (offset < DEFAULT_OFFSET) { return this.BadRequest(BadRequestMessages.OffsetMustBeAPositiveNumber); }
			if (limit < DEFAULT_OFFSET || limit > MAXIMUM_PAGE_SIZE) { return this.BadRequest(BadRequestMessages.LimitMustBeBetweenZeroAndFiftyHundred); }

			try
			{
				var albums = await this.unitOfWork.AlbumsRepository.GetByGenreAsync(genre, offset, limit);

				return this.Ok(albums);
			}
			catch (Exception e)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}
