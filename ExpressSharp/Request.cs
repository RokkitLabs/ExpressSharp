using System;
using System.IO;
using System.Net;
using ExpressSharp.Handlers;
namespace ExpressSharp
{
	public class Request
	{
		private readonly HttpListenerRequest _request;
		public object Body { get; set; }
		public HeaderHandler Header;
		public string StringFromStream(Stream stream) => new StreamReader(stream).ReadToEnd();
		internal Request(HttpListenerRequest request, ExpressConfiguration config) 
		{
			_request = request;
			if (!request.HasEntityBody)
			{
				return;
			}
			string body = StringFromStream(request.InputStream);
			this.Header = new HeaderHandler(_request);
			this.Body = body;
		}
		//public string Header(string headerName) => _request.Headers.Get(headerName);

		public string Param(string paramName)
		{
			//TODO
			return "";
		}
	}
}
