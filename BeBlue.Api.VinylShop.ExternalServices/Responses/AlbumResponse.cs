using BeBlue.Api.VinylShop.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace BeBlue.Api.VinylShop.ExternalServices.Responses
{
	public class AlbumResponse
	{
		[JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
		public IList<ArtistResponse> Artists { get; set; }

		[JsonIgnore]
		public Genres Genre { get; set; }

		[JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
		public string Name { get; set; }

		[JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
		public string ReleaseDate { get; set; }

		[JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
		public int TotalTracks { get; set; }
	}
}
