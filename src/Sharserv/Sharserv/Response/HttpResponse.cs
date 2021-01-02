using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Request;

namespace Sharserv.Response
{
    public class HttpResponse : HttpMessage
    {
        public string ReasonPhrase { get; set; }
        public int StatusCode { get; set; }

        public HttpResponse(string reasonPhrase, int statusCode, string content, params HttpHeader[] headers) :
            base(content, headers) => (ReasonPhrase, StatusCode, httpVersion) = (reasonPhrase, statusCode, "HTTP/1.1");


        public override string ToString()
        {
            var headerLine = $"{httpVersion} {StatusCode} {ReasonPhrase}\r\n";

            var rest = base.ToString();

            return headerLine + rest;
        }
    }
}
