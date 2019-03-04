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
		private IUnitOfWork unitOfWork;

		public AlbumsController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
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
	}
}
