using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharserv.Files
{
    public static class FileDeleter
    {
        public static void DeleteFileAt(string path)
        {
            System.IO.File.Delete(path);
        }
    }
}
