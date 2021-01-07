using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Files;
using Sharserv.Request;

namespace Sharserv.Response
{
    public static class HttpHeaderRetriever
    {
        public static IEnumerable<HttpHeader> GetHttpHeadersForFile(File file)
        {
            var contentType = new HttpHeader("Content-Type", file.Type);
            var length = new HttpHeader("Content-Length", file.Content.Length.ToString());

            return new[]
            {
                contentType, length
            };
        }
    }
}
