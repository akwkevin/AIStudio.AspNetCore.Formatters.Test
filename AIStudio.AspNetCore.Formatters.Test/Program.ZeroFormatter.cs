using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Zaabee.ZeroFormatter;

namespace Zaabee.AspNetCore.Formatters.Test
{
    partial class Program
    {
        public static void ZeroFormatterPost()
        {
            HttpClient client = new HttpClient();
            var stream = _dtos.ToStream();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
            {
                Content = new StreamContent(stream)
            };
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-zeroformatter");
            httpRequestMessage.Headers.Add("Accept", "application/x-zeroformatter");

            // HTTP POST with Json Request Body
            var responseForPost = client.SendAsync(httpRequestMessage);

            var result = responseForPost.Result.Content.ReadAsStreamAsync().Result.FromStream<List<TestDto>>();

            Console.WriteLine("ZeroFormatterPost Result Data");
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
