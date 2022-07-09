using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Zaabee.ZeroFormatter;

namespace Zaabee.AspNetCore.Formatters.ZeroFormatter
{
    public class ZeroOutputFormatter : OutputFormatter
    {
        public ZeroOutputFormatter(MediaTypeHeaderValue contentType) => SupportedMediaTypes.Add(contentType);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var data = context.Object.ToBytes(context.ObjectType);
            await response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}