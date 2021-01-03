using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Sharserv.Files;
using File = Sharserv.Files.File;

namespace Sharserv.Settings
{
    using System.IO;

    public class SettingsFile
    {
        [JsonPropertyName("mime_types")]
        public Dictionary<string, List<string>> MimeTypesWithExtensions { get; set; }

        [JsonPropertyName("launch_port")]
        public int Port { get; set; }

        public string GetMimeTypeForExtension(string extension)
        {
            foreach (var (key, value) in MimeTypesWithExtensions)
            {
                if (value.Contains(extension))
                    return key;
            }

            return string.Empty;
        }
    }

    public static class Configuration
    {
        private static readonly string _settingsPath = "settings.json";

        public static SettingsFile Settings { get; }

        static Configuration()
        {

            EnsureFileExists();

            var jsonSettings = File.ReadAllText(PathManager.GetSettingsFilePath());

            Settings = JsonSerializer.Deserialize<SettingsFile>(jsonSettings);
        }

        private static void EnsureFileExists()
        {
            if (!File.Exists(PathManager.GetSettingsFilePath()))
            {
                throw new FileNotFoundException($"Cannot find settings file at {PathManager.GetSettingsFilePath()}");
            }
        }
    }
}