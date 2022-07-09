using Jil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace Zaabee.AspNetCore.Formatters.Test
{
    partial class Program
    {
        static List<TestDto> _dtos;
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            _dtos = GetDtos();
            Console.WriteLine("Original Data");
            Console.WriteLine(JsonConvert.SerializeObject(_dtos));

            JsonPost();

            ProtobufPost();

            JilPost();

            MsgPackPost();

            Utf8JsonPost();

            ZeroFormatterPost();

            Console.ReadLine();
        }

        public static void JsonPost()
        {
            HttpClient client = new HttpClient();
            var content = JsonConvert.SerializeObject(_dtos);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
            {
                Content = new StringContent(content)
            };

            httpRequestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // HTTP POST with Protobuf Request Body
            var responseForPost = client.SendAsync(httpRequestMessage);

            var result = JsonConvert.DeserializeObject<List<TestDto>>(responseForPost.Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine("JsonPost Result Data");
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        private static List<TestDto> GetDtos()
        {
            return new List<TestDto>
            {
                new TestDto
                {
                    Id = Guid.NewGuid(),
                    Tag = long.MaxValue,
                    CreateTime = DateTime.Now,
                    Name = "0",
                    Enum = TestEnum.Apple,
                },
                new TestDto
                {
                    Id = Guid.NewGuid(),
                    Tag = long.MaxValue - 3,
                    CreateTime = DateTime.Now,
                    Name = "1",
                    Enum = TestEnum.Apple,
                }
            };
        }
    }
}
