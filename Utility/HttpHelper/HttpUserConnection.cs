using AdminClient.Model.DataObjects;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdminClient.Utility.HttpHelper.Interfaces;

namespace AdminClient.Utility.HttpHelper
{
    internal class HttpUserConnection : IHttpConnection<Users>
	{
		private const string _url = "http://localhost:5000/users/";

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


		public async Task<List<Users>> GetAll()
		{
			List<Users> users = new List<Users>();

			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(_url);

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

			return users;
		}

		public async Task<Users?> GetOne(string id)
		{
			string fullUrl = Path.Combine(_url, id);

			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(fullUrl);

				string json = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<Users>(json);
			}

		}
	}
}
