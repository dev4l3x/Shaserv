using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharserv.Files
{
    public class FileFinder
    {
        public static File GetFileAt(string path)
        {
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            if (!fileStream.CanRead)
            {
                throw new UnauthorizedAccessException("Can't read the requested file");
            }

            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer);

            fileStream.Close();

            var content = Encoding.ASCII.GetString(buffer);

            var file = new File()
            {
                Content = content
            };

            return file;
        }
    }
}
