using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ExpressSharp.Middleware
{
	internal class MiddlewareHandler
	{

		private readonly List<Action<Request, Response, Action>> _actions;
		private readonly Request _req;
		private readonly Response _res;
		private readonly Action<Request, Response> _callback;


		private int middlewarePos = 0;


		public MiddlewareHandler(HttpListenerContext context,
			Action<Request, Response> callback,  
			List<Action<Request, Response, Action>> actions)
		{
			_callback = callback;
			_req = new Request(context.Request);
			_res = new Response(context.Response);
			_actions = actions;
			if (_actions.Count >= 1)
				actions[0].Invoke(_req, _res, next);
			else
				callback(_req, _res);
		}

		private void next()
		{
			middlewarePos++;
			if (middlewarePos == _actions.Count)
			{
				_callback(_req, _res);
				return;
			}
			_actions[middlewarePos](_req, _res, next);
		}
	}
}
