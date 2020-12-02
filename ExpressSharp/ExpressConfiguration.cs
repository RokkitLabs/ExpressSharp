using System;
using System.Collections.Generic;
using System.Net;
using ExpressSharp.Exceptions;

namespace ExpressSharp
{
	internal class ExpressConfiguration
	{
		public HttpListener server;
		public List<Action<Request, Response, Action>> actions; //Middleware that is specified via use
		public Dictionary<string, Action<Request, Response>> bindings;

		public string baseUrl = "http://*:{0}/"; //Base URL is required, this will be a wildcard so that we can dynamically set port when Listen is called.
		public string baseHttpsUrl = "https://*:{0}/"; //Same as base URL but for https
		public bool listening = false; //Set to false, HttpListener uses GetContext so we will have to loop and create new threads per request.

		private ushort _port = 3000; //UInt16 as that is the largest a port can be

		public void SetPort(ushort? port)
		{
			if (port != null)
			{
				if (port == 0)
					throw new InvalidPortException();
				_port = port.GetValueOrDefault();
			}
		}

		public void InitServer()
		{
			listening = true;
			server.Prefixes.Clear();
			server.Prefixes.Add(string.Format(baseUrl, _port));
			server.Start();
		}
		public void Use(Action<Request, Response, Action> method, bool first = false)
		{
			if (first)
				actions.Insert(0, method);
			else
				actions.Add(method);
		}
		public void Bind(string path, Action<Request, Response> callback)
		{
			bindings.Add(path, callback);
		}
	}
}
