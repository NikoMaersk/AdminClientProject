using System;
using System.Collections.Generic;

namespace AdminClient.Utility
{
	internal static class ServiceContainer
	{
		static readonly Dictionary<Type, Lazy<object>> services = new Dictionary<Type, Lazy<object>>();

		public static void Register<T>(Func<T> func)
		{
			if (func != null)
			{
				services[typeof(T)] = new Lazy<object>(() => func());
			}
			else
			{
				throw new Exception("Null delegate can't be added");
			}
		}

		public static T Resolve<T>()
		{
			return (T)Resolve(typeof(T));
		}

		public static object Resolve(Type type)
		{
			Lazy<object>? service;
			if (services.TryGetValue(type, out service))
			{
				return service.Value;
			}
			throw new Exception("Service not found");
		}
	}
}
