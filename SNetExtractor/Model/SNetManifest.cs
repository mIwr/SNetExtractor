using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SNetExtractor.Model
{
    /// <summary>
    /// SafetyNet manifest
    /// </summary>
    public class SNetManifest
    {
        public byte Percent { get; private set; }
        public string Url { get; private set; }

        public SNetManifest(byte percent, string url)
        {
            Percent = percent;
            Url = url;
        }

        public static SNetManifest? From(byte[] payload)
        {
            var json = Encoding.UTF8.GetString(payload);
            var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json) ?? new Dictionary<string, JsonElement>();
            var manifest = From(dict);
            return manifest;
        }

        public static SNetManifest? From(Dictionary<string, JsonElement> json)
        {
            if (!json.ContainsKey("url")) {
                return null;
            }
            var url = json["url"].Deserialize<string>() ?? string.Empty;
            byte percent = 100;
            if (json.ContainsKey("percent"))
            {
                percent = json["percent"].Deserialize<byte>();
            }
            return new SNetManifest(percent, url);
        }

    }
}
