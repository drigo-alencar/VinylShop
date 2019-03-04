using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlue.Api.VinylShop.ExternalServices.Responses
{
	public class ArtistResponse
	{
		[JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
		public string Name { get; set; }
	}
}
