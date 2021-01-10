using Sharserv.Request;
using Sharserv.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharserv
{
    public static class ExceptionFactory
    {
     
        public static HttpResponse GetResponse(Exception exception)
        {
            HttpResponse response;
            switch (exception)
            {
                case FileNotFoundException:
                    response = new HttpResponse("Not Found", 404, string.Empty, new HttpHeader[0]);
                    break;
                case ArgumentException e:
                    response = new HttpResponse("Bad Request", 400, e.Message, new HttpHeader[0]);
                    break;
                default:
                    response = new HttpResponse("Internal Server Error", 500, string.Empty, new HttpHeader[0]);
                    break;
            }

            return response;
        }
    }
}
