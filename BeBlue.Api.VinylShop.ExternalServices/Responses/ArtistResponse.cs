using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BeBlue.Api.VinylShop.ExternalServices.Responses
{
	public class ArtistResponse
	{
		[JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
		public string Name { get; set; }
	}
}
