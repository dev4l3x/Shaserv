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
    public class PutRequestHandler : IRequestHandler
    {
        public HttpResponse Handle(HttpRequest request)
        {
            var path = PathManager.GetPathForResource(request.RequestedResource);
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException(
                    "Resource does not exists in server");
            }


            FileDeleter.DeleteFileAt(path);
            var stream = System.IO.File.OpenWrite(path);

            try
            {
                stream.Write(Encoding.ASCII.GetBytes(request.Content));
            }
            catch
            {
                throw;
            }
            finally
            {
                stream.Close();
            }

            return HttpResponse.GetOkResponse();
        }
    }
}
