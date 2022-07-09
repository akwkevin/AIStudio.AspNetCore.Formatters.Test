using Jil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Zaabee.Jil;

namespace Zaabee.AspNetCore.Formatters.Test
{
	partial class Program
	{
        public static void JilPost()
        {
            HttpClient client = new HttpClient();

            var json = _dtos.ToJson(new Options(dateFormat: DateTimeFormat.ISO8601,
                excludeNulls: true, includeInherited: true,
                serializationNameFormat: SerializationNameFormat.CamelCase));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/x-jil")
            };
            httpRequestMessage.Headers.Add("Accept", "application/x-jil");

            var response = client.SendAsync(httpRequestMessage).Result;

            var result = response.Content.ReadAsStringAsync().Result.FromJson<List<TestDto>>(new Options(dateFormat: DateTimeFormat.ISO8601,
                    excludeNulls: true, includeInherited: true,
                    serializationNameFormat: SerializationNameFormat.CamelCase));

            Console.WriteLine("JsonPost Result Data");
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
