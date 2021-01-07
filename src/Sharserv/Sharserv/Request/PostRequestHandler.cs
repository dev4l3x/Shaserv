using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Files;
using Sharserv.Response;
using File = System.IO.File;

namespace Sharserv.Request
{
    public class PostRequestHandler : IRequestHandler
    {
        public HttpResponse Handle(HttpRequest request)
        {
            var path = PathManager.GetPathForResource(request.RequestedResource);
            if (File.Exists(path))
            {
                throw new ArgumentException(
                    "Resource already exits in the server. Use PUT instead of POST to modify the file.");
            }

            try
            {
                var stream = File.Create(path);
                stream.Write(Encoding.ASCII.GetBytes(request.Content));
                stream.Close();
            }
            catch
            {
                File.Delete(path);
                throw;
            }

            return HttpResponse.GetCreatedResponse();
        }
    }
}
