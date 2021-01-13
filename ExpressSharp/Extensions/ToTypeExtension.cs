using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ExpressSharp.Extensions
{
	public static class ToTypeExtension
	{
		public static T ToType<T>(this object obj)
		{
			if (obj is JObject jobj)
				return jobj.ToObject<T>();

			T tmp = Activator.CreateInstance<T>();

			foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
			{
				try
				{
					tmp.GetType().GetProperty(propertyInfo.Name)?.SetValue(tmp, propertyInfo.GetValue(obj, null));
				}
				catch { 
					//Shouldn't error but if it does don't halt whole program
				}
			}

			return tmp;
		}
	}
}
