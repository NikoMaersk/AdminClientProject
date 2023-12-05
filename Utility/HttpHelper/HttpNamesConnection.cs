using AdminClient.Model.DataObjects;
using AdminClient.Utility.HttpHelper.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper
{
	internal class HttpNamesConnection : IHttpConnection<Name>, IDisposable
	{
		private const string _url = "http://localhost:5000/names/";
		private readonly HttpClient _client;
		private string _token;


		public HttpNamesConnection(string token)
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
			}
			
			return false;
		}


		public async Task<List<Name>> GetAll()
		{
			List<Name> list = new List<Name>();

			try
			{
				var response = await _client.GetAsync("all");

				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();
					var data = JsonConvert.DeserializeObject<List<Name>>(json);

					if (data != null)
					{
						list.AddRange(data);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred during request: {ex.Message}");
			}
			

			return list;
		}


		public async Task<Name?> GetOne(string id)
		{
			try
			{
				var response = await _client.GetAsync(id);
				string json = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<Name>(json);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"An error occurred during request: {ex.Message}");
				return null;
			}
			
		}


		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
