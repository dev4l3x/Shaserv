using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Files;
using Sharserv.Response;
using Sharserv.Settings;

namespace Sharserv.Request
{
    public class GetRequestHandler : IRequestHandler
    {
        public HttpResponse Handle(HttpRequest request)
        {
            var path = PathManager.GetPathForResource(request.RequestedResource);
            var resource = FileFinder.GetFileAt(path);
            //var rp = "HTTP/1.1 200 OK\nContent-Type: text/plain\nContent-Length: 12\n\nHello world!";
            var contentType = new HttpHeader("Content-Type", resource.Type);
            var length = new HttpHeader("Content-Length", resource.Content.Length.ToString());
            var response = new HttpResponse("OK", 200, resource.Content, contentType, length);
            return response;
        }
    }
}
