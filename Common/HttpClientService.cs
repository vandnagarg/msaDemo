using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common
{
    public class HttpClientService
    {
        public async static Task<HttpResponseMessage> getAsyncMethod(string _apiUrl, string accessToken)
        {
            
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", accessToken);

                return await client.GetAsync(_apiUrl);
            }
        }

        public async static Task<HttpResponseMessage> PutAsyncMethodCall(string _apiUrl, string accessToken,int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", accessToken.ToString());

                return await client.PutAsJsonAsync<int>(_apiUrl, id);
            }
        }
    }
}
 