using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Zaabee.MsgPack;

namespace Zaabee.AspNetCore.Formatters.MsgPack
{
    public class MsgPackOutputFormatter : OutputFormatter
    {
        public MsgPackOutputFormatter(MediaTypeHeaderValue contentType) => SupportedMediaTypes.Add(contentType);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var data = context.Object.ToBytes();
            await response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}