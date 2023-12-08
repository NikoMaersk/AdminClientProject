using AdminClient.Utility.HttpHelper.Interfaces;
using System;
using System.Collections.Generic;

namespace AdminClient.Utility
{
	public class HttpConnectionFactory
	{
		private readonly Dictionary<Type, object> connectionMap = new Dictionary<Type, object>();
		private static readonly HttpConnectionFactory instance = new HttpConnectionFactory();

		public static HttpConnectionFactory Instance => instance;

		public void RegisterConnection<T>(IHttpConnection<T> connection)
		{
			connectionMap[typeof(T)] = connection;
		}

		public IHttpConnection<T>? CreateNewHttpConnection<T>()
		{
			if (connectionMap.TryGetValue(typeof(T), out var connection))
			{
				return (IHttpConnection<T>)connection;
			}

			return null;
		}
	}
}
