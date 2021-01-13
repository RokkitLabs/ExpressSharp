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


		private int _middlewarePos = 0;


		public MiddlewareHandler(HttpListenerContext context,
			Action<Request, Response> callback,  
			ExpressConfiguration config)
		{
			_callback = callback;
			_req = new Request(context.Request, config);
			_res = new Response(context.Response, config);
			_actions = config.actions;
			if (_actions.Count >= 1)
				_actions[0].Invoke(_req, _res, next);
			else
				callback(_req, _res);
		}

		private void next()
		{
			_middlewarePos++;
			if (_middlewarePos == _actions.Count)
			{
				_callback(_req, _res);
				return;
			}
			_actions[_middlewarePos](_req, _res, next);
		}
	}
}
