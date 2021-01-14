using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ExpressSharp.Extensions
{
	static internal class Extensions
	{
		public static void SendString(this HttpListenerResponse response, string content)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(content);
			response.ContentLength64 = buffer.Length;
			response.OutputStream.Write(buffer);
		}
	}
}
