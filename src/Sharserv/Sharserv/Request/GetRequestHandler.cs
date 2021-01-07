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
            var headers = HttpHeaderRetriever.GetHttpHeadersForFile(resource);
            var response = new HttpResponse("OK", 200, resource.Content, headers);
            return response;
        }
    }
}
