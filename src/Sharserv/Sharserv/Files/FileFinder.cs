using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharserv.Settings;

namespace Sharserv.Files
{
    public class FileFinder
    {
        public static File GetFileAt(string path)
        {
            EnsureFileExits(path);

            var fileContent = GetContentFromFileAsString(path);

            var info = new FileInfo(path);

            var file = new File()
            {
                Content = fileContent,
                Extension = info.Extension,
                Name = info.Name,
                Type = GetMimeTypeOfFile(path)
            };

            return file;
        }

        private static string GetMimeTypeOfFile(string path)
        {
            FileInfo info = new FileInfo(path);
            return Configuration.Settings.GetMimeTypeForExtension(info.Extension.Replace(".", ""));
        }

        private static string GetContentFromFileAsString(string path)
        {
            var bytes = GetBytesFromFile(path);
            var content = Encoding.ASCII.GetString(bytes);
            return content;
        }

        private static byte[] GetBytesFromFile(string path)
        {
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            byte[] buffer = new byte[fileStream.Length];
            using (fileStream)
            {
                EnsureCanReadFile(fileStream);
                fileStream.Read(buffer);
            }

            return buffer;
        }

        private static void EnsureFileExits(string path)
        {
            var exists = System.IO.File.Exists(path);
            if (!exists)
            {
                throw new FileNotFoundException("File not found");
            }
        }

        private static void EnsureCanReadFile(FileStream fileStream)
        {
            if (!fileStream.CanRead)
            {
                throw new UnauthorizedAccessException("Can't read the requested file");
            }
        }
    }
}
