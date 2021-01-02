using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharserv.Request
{
    public class RequestHandlerFactory
    {
        public static IRequestHandler GetHandlerForRequest(HttpRequest request)
        {
            IRequestHandler handler = request.Method switch
            {
                Method.Post => new PostRequestHandler(),
                Method.Get => new GetRequestHandler(),
                Method.Put => new PutRequestHandler(),
                Method.Delete => new DeleteRequestHandler(),
                _ => new ErrorRequestHandler()
            };
            return handler;
        }
    }
}
