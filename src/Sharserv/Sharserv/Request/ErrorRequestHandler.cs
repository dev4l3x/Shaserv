using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Response;

namespace Sharserv.Request
{
    public class ErrorRequestHandler : IRequestHandler
    {
        public HttpResponse Handle(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
