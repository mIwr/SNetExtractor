using System;

namespace SNetExtractor.Model
{
    /// <summary>
    /// Represents downloaded from google binary file
    /// </summary>
    public class GBinFile
    {
        private const byte _sigSizeBlock = 4;

        public readonly byte[] Metadata;
        /// <summary>
        /// File contents
        /// </summary>
        public readonly byte[] Payload;

        internal GBinFile(byte[] metadata, byte[] payload)
        {            
            Metadata = metadata;
            Payload = payload;
        }

        public static GBinFile From(byte[] raw)
        {
            var arr = new byte[_sigSizeBlock];
            Array.Copy(raw, arr, _sigSizeBlock);
            Array.Reverse(arr);
            var sigSize = BitConverter.ToUInt32(arr);
            var metadata = new byte[sigSize];
            Array.Copy(raw, sourceIndex: _sigSizeBlock, metadata, destinationIndex: 0, sigSize);
            var payload = new byte[raw.Length - _sigSizeBlock - sigSize];
            Array.Copy(raw, sourceIndex: _sigSizeBlock + sigSize, payload, destinationIndex: 0, payload.Length);
            return new GBinFile(metadata, payload);
        }
    }
}
