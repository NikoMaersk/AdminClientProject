using AdminClient.Model.DataObjects;
using AdminClient.Utility.HttpHelper.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper
{
    internal class AdminLoginUtil : IHttpLogin, IDisposable
    {
        private const string _url = "http://localhost:5000/login/admin";
        private const string _email = "admin@gmail.com";
        private const string _password = "admin";

        public async Task<string?> AdminLogin()
        {
            using (var client = new HttpClient())
            {
                LoginRequest request = new(_email, _password);
                string json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }

		public void Dispose()
		{
            
		}
	}
}
