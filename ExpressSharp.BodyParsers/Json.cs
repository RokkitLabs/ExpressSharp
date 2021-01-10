using System;
using ExpressSharp;
using Newtonsoft.Json;
namespace ExpressSharp.BodyParser
{
	internal class Json
	{ 
		public static void Parse(Request req, Response res)
		{
			try
			{
				if (req.Body == null)
					return;
				Console.WriteLine(req.Body as string);
				req.Body = JsonConvert.DeserializeObject(req.Body as string);
				return;
			}
			catch
			{
				return;
			}
		}
	}
}
