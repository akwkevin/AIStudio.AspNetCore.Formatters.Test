using System.IO;
using System.Text;
using System.Threading.Tasks;
using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Zaabee.Jil;

namespace Zaabee.AspNetCore.Formatters.Jil
{
    public class JilInputFormatter : TextInputFormatter
    {
        private readonly Options _jilOptions;

        public JilInputFormatter(Options jilOptions, MediaTypeHeaderValue mediaTypeHeaderValue)
        {
            _jilOptions = jilOptions;
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);
            SupportedMediaTypes.Add(mediaTypeHeaderValue);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context,
            Encoding encoding)
        {
            var request = context.HttpContext.Request;
            MemoryStream stream = new MemoryStream();
            await request.Body.CopyToAsync(stream);
            stream.Position = 0;

            var result = stream.FromStream(context.ModelType, _jilOptions, encoding);
            return await InputFormatterResult.SuccessAsync(result);
        }
    }
}