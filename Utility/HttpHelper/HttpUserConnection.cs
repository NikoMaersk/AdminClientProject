using AdminClient.Model.DataObjects;
using AdminClient.Utility.HttpHelper.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper
{
	internal class HttpUserConnection : IHttpConnection<Users>, IDisposable
	{
		private const string _url = "http://localhost:5000/users/";
		private readonly HttpClient _client;
		private string _token;


		public HttpUserConnection(string token)
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(_url);
			_token = token;
			_client.DefaultRequestHeaders.Add("Cookie", _token);
		}


		public void UpdateToken(string token)
		{
			_token = token;
			_client.DefaultRequestHeaders.Remove("Cookie");
			_client.DefaultRequestHeaders.Add("Cookie", _token);
		}


		public async Task<bool> Delete(string id)
		{
			try
			{
				var response = await _client.DeleteAsync(id);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred during request: {ex.Message}");
				return false;
			}
		}


		public async Task<List<Users>> GetAll()
		{
			List<Users> users = new List<Users>();

			try
			{
				
				var response = await _client.GetAsync("");

				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					var data = JsonConvert.DeserializeObject<List<Users>>(json);

					if (data != null)
					{
						users.AddRange(data);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred during request: {ex.Message}");
			}
			
			return users;
		}


		public async Task<Users?> GetOne(string id)
		{
			try
			{
				var response = await _client.GetAsync(id);
				string json = await response.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<Users>(json);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred during request: {ex.Message}");
				return null;
			}
		}
		public async Task<bool> Patch(Users user, string password, string oldEmail) 
		{
			string s = _url +"email/"+ oldEmail;
            await _client.PatchAsJsonAsync<Users>(s, user);
            s = _url + oldEmail;
            await _client.PatchAsJsonAsync<Users>(s, user);
            s= _url+"pass/"+ oldEmail;
			if (password != null)
			{
                string passwordJson = "\"password\": \"" + password + "\"";
                await _client.PatchAsJsonAsync<string>(s, passwordJson);

            }

            return true;
        }


        public void Dispose()
		{
			_client.Dispose();
		}
	}
}
