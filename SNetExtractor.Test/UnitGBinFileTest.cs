using System;
using System.IO;

using SNetExtractor.Model;

namespace SNetExtractor.Test
{
    public class UnitGBinFileTest
    {
        byte[] _dummyData = Array.Empty<byte>();
        [SetUp]
        public void Setup()
        {
            _dummyData = File.ReadAllBytes(TestConstants.SNetFilename);
        }

        [Test(Description = "Parsing from binary test")]
        public void TestRawParsing()
        {
            var gfile = GBinFile.From(_dummyData);
            Assert.True(gfile.Metadata.Length > 0, message: "Incorrect metadata parsing - empty data");
            Assert.True(gfile.Payload.Length > 0, message: "Incorrect payload data parsing - empty data");
        }
    }
}