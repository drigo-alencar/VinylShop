﻿using System;
using System.Runtime.Serialization;

namespace BeBlue.Api.VinylShop.ExternalServices.Exceptions
{
	[Serializable]
	internal class SpotifyException : Exception
	{
		public SpotifyException() { }

		public SpotifyException(string message) : base(message) { }

		public SpotifyException(string message, Exception innerException) : base(message, innerException) { }

		protected SpotifyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}