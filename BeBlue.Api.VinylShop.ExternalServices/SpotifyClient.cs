using BeBlue.Api.VinylShop.DomainModel;
using BeBlue.Api.VinylShop.ExternalServices.Exceptions;
using BeBlue.Api.VinylShop.ExternalServices.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeBlue.Api.VinylShop.ExternalServices
{
	public class SpotifyClient : ISpotifyClient
	{
		private readonly HttpClient client;
		private readonly SpotifySettings spotifySettings;

		public SpotifyClient(HttpClient httpClient, SpotifySettings spotifySettings)
		{
			this.client = httpClient;
			this.spotifySettings = spotifySettings;
			this.Authenticate().GetAwaiter().GetResult();
		}

		private async Task Authenticate()
		{
			try
			{
				string credentials = this.ClientCredentialsAsBase64();

				var parameters = new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") };

				this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), credentials);

				var response = await this.client.PostAsync(this.spotifySettings.TokenUri, new FormUrlEncodedContent(parameters));

				response.EnsureSuccessStatusCode();

				var clientAuthentication = JsonConvert.DeserializeObject<ClientCredentialAuthenticationResponse>(await response.Content.ReadAsStringAsync());

				this.SetAuthenticationTokenHeader(clientAuthentication);
			}
			catch (HttpRequestException e)
			{
				throw new SpotifyAuthenticationException("An error has occurred when authenticating on Spotify", e);
			}
		}

		private void SetAuthenticationTokenHeader(ClientCredentialAuthenticationResponse clientAuthentication)
		{
			this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(clientAuthentication.TokenType, clientAuthentication.AccessToken);
		}

		private string ClientCredentialsAsBase64()
		{
			var credentialBytes = Encoding.UTF8.GetBytes($"{this.spotifySettings.ClientId}:{this.spotifySettings.Secret}");
			return Convert.ToBase64String(credentialBytes);
		}

		public async Task<IReadOnlyList<AlbumResponse>> RetrieveAlbums()
		{
			try
			{
				var albums = new List<AlbumResponse>();
				foreach (Genres genre in Enum.GetValues(typeof(Genres)))
				{
					var response = await this.client.GetAsync(this.spotifySettings.SearchUri + $"?q={genre}&genre:{genre}&type=track&limit=50");

					response.EnsureSuccessStatusCode();

					var queryResult = JObject.Parse(await response.Content.ReadAsStringAsync());

					foreach (var album in queryResult["tracks"]["items"].Children().Select(x => x["album"].ToObject<AlbumResponse>()))
					{
						album.Genre = genre;
						albums.Add(album);
					}
				}
				return albums;
			}
			catch (HttpRequestException e)
			{
				throw new SpotifyAuthenticationException("An error has occurred when retrieving albums from Spotify", e);
			}
		}
	}
}
