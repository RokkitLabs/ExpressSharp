using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExpressSharp;
namespace ExampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Express server = new Express();

			server.GET("/", (req, res) =>
			{
				res.Status(200).Json(new { Id = 123, Username = "Test Username" });
			});

			server.GET("/helloworld", (req, res) =>
			{
				res.Status(200).Send("Hello, World!");
			});

			server.Listen();
		}
	}
}
