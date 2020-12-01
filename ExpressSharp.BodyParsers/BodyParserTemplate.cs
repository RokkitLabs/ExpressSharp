using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressSharp.BodyParser
{
	internal abstract class BodyParserTemplate
	{
		public abstract object Parse(string body);
	}
}
