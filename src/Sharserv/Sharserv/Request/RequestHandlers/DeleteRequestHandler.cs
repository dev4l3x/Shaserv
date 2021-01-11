using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Files;
using Sharserv.Response;
using SystemFile = System.IO.File;

namespace Sharserv.Request
{
    public class DeleteRequestHandler : IRequestHandler
    {
        public HttpResponse Handle(HttpRequest request)
        {
            var resourcePath = PathManager.GetPathForResource(request.RequestedResource);
            if (!SystemFile.Exists(resourcePath))
                throw new FileNotFoundException("Requested file for delete not found");

            FileDeleter.DeleteFileAt(resourcePath);

            return HttpResponse.GetOkResponse();
        }
    }
}
