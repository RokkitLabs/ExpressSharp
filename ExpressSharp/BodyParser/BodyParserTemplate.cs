using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressSharp.BodyParser
{
	public abstract class BodyParserTemplate
	{
		public abstract object Parse(string body);
	}
}
