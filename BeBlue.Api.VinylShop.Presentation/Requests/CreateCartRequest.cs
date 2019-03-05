using System.Collections.Generic;

namespace BeBlue.Api.VinylShop.Presentation.Requests
{
	public class CreateCartRequest
	{
		public IList<string> AlbumsIds { get; set; }
	}
}