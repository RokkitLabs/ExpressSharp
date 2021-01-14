using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
namespace ExpressSharp.Handlers
{
	public class HeaderHandler
	{
		private HttpListenerRequest _request;
		internal HeaderHandler(HttpListenerRequest request)
		{
			_request = request;
		}
		public string Get(string headerName) => _request.Headers.Get(headerName);
		public string this[string headerName]
		{
			get { return Get(headerName); }
		}
		
	}
}
