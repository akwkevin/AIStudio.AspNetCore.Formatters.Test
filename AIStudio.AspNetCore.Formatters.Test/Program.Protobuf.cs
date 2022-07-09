using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Zaabee.Protobuf;

namespace Zaabee.AspNetCore.Formatters.Test
{
	partial class Program
	{
		public static void ProtobufPost()
		{
			HttpClient client = new HttpClient();

			var dtos = GetDtos();
			var stream = dtos.ToStream();
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/Values/Post")
			{
				Content = new StreamContent(stream)
			};
			httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-protobuf");
			httpRequestMessage.Headers.Add("Accept", "application/x-protobuf");

			// HTTP POST with Protobuf Request Body
			var responseForPost = client.SendAsync(httpRequestMessage);

			var result = responseForPost.Result.Content.ReadAsStreamAsync().Result.FromStream<List<TestDto>>();

			Console.WriteLine(JsonConvert.SerializeObject(result));
		}
	}
}
