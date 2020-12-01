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
			try
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

				server.POST("/TestPost", (req, res) =>
				{
					res.Status(200).Json(new { Id = 300 });
				});

				server.PATCH("/TestPatch", (req, res) =>
				{
					res.Status(200).Json(new { Id = 301 });
				});

				server.DELETE("/TestDelete", (req, res) =>
				{
					res.Status(200).Json(new { Id = 302 });
				});

				server.DELETE("/TestPut", (req, res) =>
				{
					res.Status(200).Json(new { Id = 303 });
				});

				server.Listen();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error occured starting server");
				Console.WriteLine(ex.ToString());
			}
			Console.WriteLine("Closing...");
		}
	}
}
