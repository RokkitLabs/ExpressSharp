using System;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using ExpressSharp.Middleware;
using ExpressSharp.Exceptions;

namespace ExpressSharp
{
	public class Express
	{
		private readonly ExpressConfiguration _config;

		public Express()
		{
			if (!HttpListener.IsSupported)
				throw new UnsupportedOperatingSystemException();
			_config = new ExpressConfiguration();
			_config.server = new HttpListener();
			_config.actions = new List<Action<Request, Response, Action>>();
			_config.bindings = new Dictionary<string, Action<Request, Response>>();
		}
		public void Use(Action<Request, Response, Action> method) => _config.Use(method);
		public void Use(Type bodyParser)
		{
			Action<Request, Response, Action> action = (req, res, next) =>
			{
				bodyParser.GetMethod("Parse").Invoke(null, new object[] { req, res });
				next();
			};
			_config.Use(action, true);
		}
		private void AcceptRequest(HttpListenerContext context)
		{
			HttpListenerResponse res = context.Response;
			HttpListenerRequest req = context.Request;
			Action<Request, Response> callback;

			if (!_config.bindings.TryGetValue($"{req.HttpMethod} {req.RawUrl}", out callback)) //Callback doesnt exist
			{
				res.StatusCode = 404;
				byte[] buffer = Encoding.UTF8.GetBytes($"Cannot {req.HttpMethod} {req.RawUrl}");
				res.ContentLength64 = buffer.Length;
				res.OutputStream.Write(buffer);
				return;
			}
			new MiddlewareHandler(context, callback, _config); ;
		}

		public void Listen()
		{
			_config.InitServer();
			while (_config.listening)
			{
				HttpListenerContext context = _config.server.GetContext();
				new Thread(() =>
				{
					AcceptRequest(context);
				}).Start();
			}
		}

		public void Listen(ushort port)
		{
			_config.SetPort(port);
			this.Listen();
		}
		public void GET(string path, Action<Request, Response> callback) => _config.Bind($"GET {path}", callback);
		public void POST(string path, Action<Request, Response> callback) => _config.Bind($"POST {path}", callback);
		public void PUT(string path, Action<Request, Response> callback) => _config.Bind($"PUT {path}", callback);
		public void DELETE(string path, Action<Request, Response> callback) => _config.Bind($"DELETE {path}", callback);
		public void PATCH(string path, Action<Request, Response> callback) => _config.Bind($"PATCH {path}", callback);
	}
}
