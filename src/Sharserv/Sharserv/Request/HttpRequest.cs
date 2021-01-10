using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Sharserv.Request
{

    public record HttpHeader
    {
        public string Name { get; init; }
        public string Value { get; init; }

        public HttpHeader(string name, string value) => (Name, Value) = (name, value);

    }
    
    public enum Method
    {
        Get, Post, Put, Delete
    }

    public class HttpMessage
    {
        protected string httpVersion;
        protected string requestedResource;
        protected List<HttpHeader> headers;
        protected string? content;

        public string RequestedResource
        {
            get
            {
                return requestedResource;
            }
            set
            {
                requestedResource = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }


        public HttpMessage(string content, IEnumerable<HttpHeader> headers)
        {
            this.content = content;
            this.headers = new List<HttpHeader>(headers);
        }

        public HttpMessage(params HttpHeader[] headers)
        {
            content = null;
            this.headers = new List<HttpHeader>(headers);
        }

        public void AddHeader(HttpHeader header)
        {
            headers.Add(header);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var header in headers)
            {
                builder.Append($"{header.Name}: {header.Value}\r\n");
            }

            builder.Append("\r\n");
            builder.Append(content);

            return builder.ToString();
        }
    }

    public class HttpRequest : HttpMessage
    {

        protected Method method;

        public Method Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        public static HttpRequest FromString(string httpRequest)
        {
            if (string.IsNullOrWhiteSpace(httpRequest))
            {
                throw new ArgumentException("Parameter `httpRequest` must have an http request format");
            }

            HttpRequest request = new HttpRequest();

            var lines = httpRequest.Split("\r\n\r\n", 2);

            var headers = lines[0];
            var contentLines = lines[1];
            var firstLine = lines[0].Split(" ");
            var method = firstLine[0];
            request.method = (Method)Enum.Parse(typeof(Method), method, true);
            request.requestedResource = firstLine[1].Remove(0, 1);
            request.httpVersion = firstLine[2];

            var splitedHeaders = headers.Split("\r\n");
            for (int i = 1; i < splitedHeaders.Length; i++)
            {
                var splittedLine = splitedHeaders[i].Split(": ");
                var headerName = splittedLine[0];
                var headerValue = splittedLine[1];
                var header = new HttpHeader(headerName, headerValue);
                request.headers.Add(header);
            }

            var eofIndex = contentLines.IndexOf('\0');
            request.Content = lines.Last().Substring(0, eofIndex);

            return request;
        }
    }
}