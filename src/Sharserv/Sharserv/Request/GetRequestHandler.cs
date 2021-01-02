using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Files;
using Sharserv.Response;

namespace Sharserv.Request
{
    public class GetRequestHandler : IRequestHandler
    {
        public HttpResponse Handle(HttpRequest request)
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsFolder, "Sharserv", request.RequestedResource);
            var resource = FileFinder.GetFileAt(path);
            //var rp = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: 12\n\nHello world!";
            var contentType = new HttpHeader("Content-Type", "text/plain");
            var length = new HttpHeader("Content-Length", resource.Content.Length.ToString());
            var response = new HttpResponse("OK", 200, resource.Content, contentType, length);
            return response;
        }
    }
}
