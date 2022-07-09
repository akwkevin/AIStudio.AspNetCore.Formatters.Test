using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zaabee.AspNetCore.Formatters.Test
{
    partial class Program
    {
        public static void Utf8JsonPost()
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(GetDtos());
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/x-utf8json")
            };
            httpRequestMessage.Headers.Add("Accept", "application/x-utf8json");

            // HTTP POST with Json Request Body
            var responseForPost = client.SendAsync(httpRequestMessage);

            var result =
                JsonConvert.DeserializeObject<List<TestDto>>(responseForPost.Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
