using System;
using ExpressSharp;
using Newtonsoft.Json;
namespace ExpressSharp.BodyParser
{
	internal class Json : BodyParserTemplate
	{ 
		public override object Parse(string body)
		{
			return JsonConvert.DeserializeObject(body);
		}
	}
}
