using AdminClient.Model.DataObjects;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper
{
	internal class AdminLogin
    {
        private const string _url = "http://localhost:5000/login/admin";

        public static async Task<(bool success, string token)> Login(string email, string password)
        {
			string cookie = string.Empty;
			bool success = false;

			try
			{
				using (var client = new HttpClient())
				{
					

					LoginRequest request = new(email, password);
					string json = JsonConvert.SerializeObject(request);
					HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
					var response = await client.PostAsync(_url, content);

					if (response.IsSuccessStatusCode)
					{
						if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
						{
							cookie = cookieValues.FirstOrDefault() ?? string.Empty;
							success = true;
						}
					}
					else
					{
						Debug.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred during login: {ex.Message}");
			}
			return (success, cookie);
		}


		private static string ExtractTokenFromCookie(string? cookie)
		{
			if (cookie == null) { return string.Empty; }

			const string tokenPrefix = ".AspNetCore.token=";
			const char cookieDelimiter = ';';

			int tokenStartIndex = cookie.IndexOf(tokenPrefix);

			if (tokenStartIndex != -1)
			{
				tokenStartIndex += tokenPrefix.Length;

				int tokenEndIndex = cookie.IndexOf(cookieDelimiter, tokenStartIndex);

				if (tokenEndIndex != -1)
				{
					return cookie.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex);
				}
				else
				{
					return cookie.Substring(tokenStartIndex);
				}
			}

			return string.Empty;
		}
	}
}
