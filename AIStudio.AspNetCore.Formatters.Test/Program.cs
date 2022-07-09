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

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

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
            var content = JsonConvert.SerializeObject(GetDtos());
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
            {
                Content = new StringContent(content)
            };

            httpRequestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // HTTP POST with Protobuf Request Body
            var responseForPost = client.SendAsync(httpRequestMessage);

            var result = JsonConvert.DeserializeObject<List<TestDto>>(responseForPost.Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        //public static void ProtobufPost()
        //{
        //    HttpClient client = new HttpClient();
        //    //var stream = new MemoryStream();
        //    //stream.PackBy(GetDtos());
        //    //stream.Seek(0, SeekOrigin.Begin);

        //    var dtos = GetDtos();
        //    var stream = dtos.ToStream();
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
        //    {
        //        Content = new StreamContent(stream)
        //    };
        //    httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-protobuf");
        //    httpRequestMessage.Headers.Add("Accept", "application/x-protobuf");

        //    // HTTP POST with Protobuf Request Body
        //    var responseForPost = client.SendAsync(httpRequestMessage);

        //    //var result = ProtobufHelper.FromStream<List<TestDto>>(
        //    //    responseForPost.Result.Content.ReadAsStreamAsync().Result);

        //    var result = responseForPost.Result.Content.ReadAsStreamAsync().Result.FromStream<List<TestDto>>();

        //    Console.WriteLine(JsonConvert.SerializeObject(result));
        //}

        //public static void JilPost()
        //{
        //    HttpClient client = new HttpClient();
        //    var dtos = GetDtos();
        //    var json = JSON.Serialize(dtos, new Options(dateFormat: DateTimeFormat.ISO8601,
        //        excludeNulls: true, includeInherited: true,
        //        serializationNameFormat: SerializationNameFormat.CamelCase));

        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
        //    {
        //        Content = new StringContent(json, Encoding.UTF8, "application/x-jil")
        //    };
        //    httpRequestMessage.Headers.Add("Accept", "application/x-jil");

        //    var response = client.SendAsync(httpRequestMessage).Result;

        //    var result =
        //        JSON.Deserialize<List<TestDto>>(response.Content.ReadAsStringAsync()
        //            .Result, new Options(dateFormat: DateTimeFormat.ISO8601,
        //            excludeNulls: true, includeInherited: true,
        //            serializationNameFormat: SerializationNameFormat.CamelCase));

        //    Console.WriteLine(JsonConvert.SerializeObject(result));
        //}

        //public static void MsgPackPost()
        //{
        //    HttpClient client = new HttpClient();
        //    var json = JsonConvert.SerializeObject(GetDtos());
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
        //    {
        //        Content = new StringContent(json, Encoding.UTF8, "application/x-msgpack")
        //    };
        //    httpRequestMessage.Headers.Add("Accept", "application/x-msgpack");

        //    // HTTP POST with Json Request Body
        //    var responseForPost = client.SendAsync(httpRequestMessage);

        //    var result =
        //        JsonConvert.DeserializeObject<List<TestDto>>(responseForPost.Result.Content.ReadAsStringAsync().Result);

        //    Console.WriteLine(JsonConvert.SerializeObject(result));
        //}

      

        //public static void ZeroFormatterPost()
        //{
        //    HttpClient client = new HttpClient();
        //    var json = JsonConvert.SerializeObject(GetDtos());
        //    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
        //    {
        //        Content = new StringContent(json, Encoding.UTF8, "application/x-zeroformatter")
        //    };
        //    httpRequestMessage.Headers.Add("Accept", "application/x-zeroformatter");

        //    // HTTP POST with Json Request Body
        //    var responseForPost = client.SendAsync(httpRequestMessage);

        //    var result =
        //        JsonConvert.DeserializeObject<List<TestDto>>(responseForPost.Result.Content.ReadAsStringAsync().Result);

        //    Console.WriteLine(JsonConvert.SerializeObject(result));
        //}

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
