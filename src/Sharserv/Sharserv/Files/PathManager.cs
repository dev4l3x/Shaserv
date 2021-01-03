using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharserv.Files
{
    public static class PathManager
    {
        private static readonly string _settingsFileName = "settings.json";
        private static readonly string _publicPath = "public";
        private static readonly string _serverDocumentsFolderName = "Sharserv";


        public static string DocumentsFolderPath => 
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        public static string GetPathForResource(string relativePath)
        {
            return Path.Combine(GetPublicFolderPath(), relativePath);
        }

        public static string GetPublicFolderPath()
        {
            return Path.Combine(DocumentsFolderPath, _serverDocumentsFolderName, _publicPath);
        }

        public static string GetSettingsFilePath()
        {
            return Path.Combine(DocumentsFolderPath, _serverDocumentsFolderName, _settingsFileName);
        }

    }
}
