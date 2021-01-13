using System;
using System.IO;
using System.Net;
namespace ExpressSharp
{
	public class Request
	{
		private readonly HttpListenerRequest _request;
		public object Body { get; set; }

		public string StringFromStream(Stream stream) => new StreamReader(stream).ReadToEnd();
		internal Request(HttpListenerRequest request, ExpressConfiguration config) 
		{
			_request = request;
			if (!request.HasEntityBody)
			{
				return;
			}
			string body = StringFromStream(request.InputStream);
			this.Body = body;
		}

		public string Header(string header) => _request.Headers.Get(header);

		public string Param(string param)
		{
			//TODO
			return "";
		}
	}
}
