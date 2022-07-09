using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Zaabee.MsgPack;

namespace Zaabee.AspNetCore.Formatters.MsgPack
{
    public class MsgPackInputFormatter : InputFormatter
    {
        public MsgPackInputFormatter(MediaTypeHeaderValue contentType) => SupportedMediaTypes.Add(contentType);

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            MemoryStream stream = new MemoryStream();
            await request.Body.CopyToAsync(stream);
            stream.Position = 0;

            var result = stream.FromStream(context.ModelType);
            return await InputFormatterResult.SuccessAsync(result);
        }
    }
}