using SNetExtractor.Model;
using System;
using System.IO;
using System.Net.Http;

namespace SNetExtractor
{
    public static class SNetUtil
    {
        private const string _manifestUrl = "https://www.gstatic.com/android/snet/snet.flags";

        public static GBinFile? RetrieveSNet(out GBinFile? manifestData)
        {
            manifestData = null;
            var manifest = DownloadManifest(out manifestData);
            if (manifest == null)
            {
                return null;
            }
            var snet = DownloadSNet(manifest.Url);
            if (snet == null)
            {
                return null;
            }

            return snet;
        }

        private static SNetManifest? DownloadManifest(out GBinFile gfile)
        {
            var data = Download(_manifestUrl);
            gfile = GBinFile.From(data);            
            var manifest = SNetManifest.From(gfile.Payload);

            return manifest;
        }

        private static GBinFile DownloadSNet(string url)
        {
            var data = Download(url);
            var gfile = GBinFile.From(data);

            return gfile;
        }

        private static byte[] Download(string url)
        {            
            var client = new HttpClient();
            var httpReq = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            var response = client.Send(httpReq);
            var stream = new BinaryReader(response.Content.ReadAsStream());
            var raw = new byte[stream.BaseStream.Length];
            var buff = new byte[4096];
            var index = 0;
            int read;
            do
            {
                read = stream.Read(buff, index: 0, buff.Length);
                if (read <= 0)
                {
                    break;
                }
                Array.Copy(buff, sourceIndex: 0, raw, destinationIndex: index, length: read);
                index += read;
            } while (read > 0);
            stream.Close();
            stream.Dispose();

            return raw;
        }
    }
}
