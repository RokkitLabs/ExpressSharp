using System;
using System.Net;
using System.Text;
using ExpressSharp.Extensions;
using Newtonsoft.Json;

namespace ExpressSharp
{
	public class Response
	{
		private readonly HttpListenerResponse _response;
		private readonly ExpressConfiguration _config;
		internal Response(HttpListenerResponse response, ExpressConfiguration config)
		{
			_response = response;
			_config = config;
		}

		public Response Status(int code)
		{
			_response.StatusCode = code;
			return this;
		}

		public Response Send(string data)
		{
			_response.SendString(data);
			return this;
		}

		public Response Json<T>(T obj)
		{
			_response.ContentType = "application/json";
			_response.SendString(JsonConvert.SerializeObject(obj));
			return this;
		}

		public Response Set(string key, string value) {
			_response.AddHeader(key, value);
			return this;
		}
	}
}
