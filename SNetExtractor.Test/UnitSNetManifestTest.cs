using System;
using System.IO;

using SNetExtractor.Model;

namespace SNetExtractor.Test
{
    public class UnitSNetManifestTest
    {
        byte[] _dummyData = Array.Empty<byte>();
        [SetUp]
        public void Setup()
        {
            _dummyData = File.ReadAllBytes(TestConstants.GBinManifestFilename);
        }

        [Test(Description = "Parsing manifest payload test")]
        public void TestPayloadParsing()
        {
            var gfile = GBinFile.From(_dummyData);
            Assert.That(gfile.Metadata.Length, Is.GreaterThan(0), message: "Incorrect metadata parsing - empty data");
            Assert.That(gfile.Payload.Length, Is.GreaterThan(0), message: "Incorrect payload data parsing - empty data");
            var manifest = SNetManifest.From(gfile.Payload);
            Assert.That(manifest, Is.Not.Null, message: "Incorrect manifest parsing - null result");
            Assert.DoesNotThrow(new TestDelegate(() =>
            {
                var url = new Uri(manifest.Url);
            }), message: "Incorrect parsed url - throws exception at init Uri instance");
        }
    }
}