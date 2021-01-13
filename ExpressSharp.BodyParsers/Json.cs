using System;
using ExpressSharp;
using Newtonsoft.Json;
namespace ExpressSharp.BodyParser
{
	internal static class Json
	{ 
		public static void Parse(Request req, Response res)
		{
			try
			{
				if (req.Body == null)
					return;
				req.Body = JsonConvert.DeserializeObject(req.Body as string);
				return;
			}
			catch
			{
				req.Body = null;
				return;
			}
		}
	}
}
