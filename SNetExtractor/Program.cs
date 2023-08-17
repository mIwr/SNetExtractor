using System;
using System.IO;
using System.Text;

namespace SNetExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Downloading the latest SNet...");
            var snet = SNetUtil.RetrieveSNet(out var manifest);            
            if (snet == null)
            {
                Console.WriteLine("Error: SNet file is null");
                return;
            }
            if (manifest != null)
            {
                var manifestStr = Encoding.UTF8.GetString(manifest.Payload);
                Console.WriteLine("Manifest info:\n" + manifestStr + "\n");
                File.WriteAllBytes("snet.manifest", manifest.Payload);
            }
            File.WriteAllBytes("snet.jar", snet.Payload);
            Console.WriteLine("Success! Downloaded SafetyNet file saved as 'snet.jar'");
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}