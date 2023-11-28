using AdminClient.Model.DataObjects;
using AdminClient.Utility.HttpHelper.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper
{
    internal class HttpNamesConnection : IHttpConnection<Name>
	{
		private const string _url = "http://localhost:5000/names/";

		public async Task<bool> Delete(string id)
		{
			string fullUrl = Path.Combine(_url, id);

			using (var client = new HttpClient())
			{
				var response = await client.DeleteAsync(fullUrl);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public async Task<List<Name>> GetAll()
		{
			List<Name> list = new List<Name>();
			string fullUrl = Path.Combine(_url, "all");

			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(fullUrl);

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

			return list;
		}

		public async Task<Name?> GetOne(string id)
		{
			string fullUrl = Path.Combine(_url, id);

			using (var client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(fullUrl);
				string json = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<Name>(json);
			}
		}
	}
}
