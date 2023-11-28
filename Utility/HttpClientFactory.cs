using AdminClient.Model.DataObjects;
using AdminClient.Utility.HttpHelper;
using AdminClient.Utility.HttpHelper.Interfaces;
using System;
using System.Collections.Generic;

namespace AdminClient.Utility
{
    public static class HttpClientFactory
	{
		private static Dictionary<Type, object> connectionInstances = new Dictionary<Type, object>();

		public static IHttpConnection<T> CreateHttpConnection<T>()
		{
			Type connectionType = typeof(IHttpConnection<T>);

			if (connectionInstances.TryGetValue(connectionType, out var instance))
			{
				return (IHttpConnection<T>)instance;
			}

			var newConnection = CreateNewHttpConnection<T>();

			if (newConnection != null)
			{
				connectionInstances[connectionType] = newConnection;
			}

			return newConnection;
		}

		private static IHttpConnection<T>? CreateNewHttpConnection<T>()
		{
			if (typeof(T) == typeof(Name))
			{
				return new HttpNamesConnection() as IHttpConnection<T>;
			}
			else
			{
				return new HttpUserConnection() as IHttpConnection<T>;
			}
		}
	}
}
