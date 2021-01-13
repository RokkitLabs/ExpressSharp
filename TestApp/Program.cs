using System;
using ExpressSharp;
using ExpressSharp.Extensions;
using ExpressSharp.BodyParser;
using Newtonsoft.Json.Linq;
namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Express server = new Express();

			server.Use(BodyParser.Json);

			server.GET("/", (req, res) =>
			{
				if (req.Body == null)
				{
					res.Status(400).Send("Expected body");
					return;
				}
				User user = req.ToType<User>();

				res.Send($"{user.Id} {user.Name}");
			});

			server.Listen();
		}
	}
}
