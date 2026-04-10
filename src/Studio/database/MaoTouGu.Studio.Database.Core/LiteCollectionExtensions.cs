// ----------------------------------------------------------
//            文件：LiteCollectionExtensions.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月22日 16:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Data;
using System.IO.Compression;
using LiteDB;

namespace MaoTouGu.Studio.Database
{
    public static class LiteCollectionExtensions
    {
        private const int LimitedSingleDocumentSize = 16 * 1048576;
        
        public static byte[] SerializeAsGZipStream(this IBsonDataReader reader)
        {
            var documentSize = 0;
            using var rawStream    = new MemoryStream();
            using var tempStream   = new MemoryStream();
            var binaryWriter = new BinaryWriter(rawStream);
            var tempWriter   = new BinaryWriter(tempStream);

            while (reader.Read())
            {
                var buffer = BsonSerializer.Serialize(reader.Current.AsDocument);
                var length = buffer.Length;
                
                tempWriter.Write(length);
                tempWriter.Write(buffer);
                documentSize++;
            }
            
            //
            // 先用tempStream保存，然后转到rawStream。
            binaryWriter.Write(documentSize);

            if (tempStream.Length > 0)
            {
                binaryWriter.Write(tempStream.ToArray());
            }

            using var compressedStream = new MemoryStream();
            
            using (var gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, leaveOpen: true))
            {
                gzip.Write(rawStream.ToArray());
            }

            // var ratio = (((double)compressedStream.Length / rawStream.Length) * 100).ToString("f2");
            
            // Console.WriteLine("------------------------------------------");
            // Console.WriteLine($"开始传输数据表，数据总数为 = {documentSize}个。");
            // Console.WriteLine($"未压缩前大小为 = {rawStream.Length}Bytes, 压缩后大小为 = {compressedStream.Length}Bytes, 压缩率为: {ratio}%");
            // Console.WriteLine("------------------------------------------");
            
            return compressedStream.ToArray();
        }
        
        public static byte[] SerializeAsGZipStream(this ILiteCollection<BsonDocument> collection)
        {
            var documentSize = collection.Count();
            var rawStream    = new MemoryStream();
            var binaryWriter = new BinaryWriter(rawStream);

            binaryWriter.Write(documentSize);
            
            
            foreach (var document in collection.FindAll())
            {
                var buffer = BsonSerializer.Serialize(document);
                var length = buffer.Length;
                
                binaryWriter.Write(length);
                binaryWriter.Write(buffer);
            }



            using var compressedStream = new MemoryStream();
            
            using (var gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, leaveOpen: true))
            {
                gzip.Write(rawStream.ToArray());
            }

            // var ratio = (((double)compressedStream.Length / rawStream.Length) * 100).ToString("f2");
            
            // Console.WriteLine("------------------------------------------");
            // Console.WriteLine($"开始传输数据表，数据总数为 = {documentSize}个。");
            // Console.WriteLine($"未压缩前大小为 = {rawStream.Length}Bytes, 压缩后大小为 = {compressedStream.Length}Bytes, 压缩率为: {ratio}%");
            // Console.WriteLine("------------------------------------------");
            
            return compressedStream.ToArray();
        }
        
        internal static MemoryStream Unzip(byte[] buffer)
        {
            var       decompressedStream = new MemoryStream();
            using var memoryStream       = new MemoryStream(buffer);
            using var gzipStream         = new GZipStream(memoryStream, CompressionMode.Decompress) ;
            
            gzipStream.CopyTo(decompressedStream);
            decompressedStream.Seek(0, SeekOrigin.Begin);

            return decompressedStream;
        }

        public static IEnumerable<BsonDocument> DeserializeCollection(byte[] buffer)
        {
            using var decompressedStream = Unzip(buffer);
            
            //
            //
            var binaryReader = new BinaryReader(decompressedStream);
            var documentSize = binaryReader.ReadInt32();

            if (documentSize < 0 || documentSize > int.MaxValue)
            {
                yield break; 
            }
            
            for (var i = 0; i < documentSize; i++)
            {
                var length = binaryReader.ReadInt32();

                if (length <= 0 || length > LimitedSingleDocumentSize)
                {
                    throw new DataException($"文档的数据数据长度为{length}，违反了限制单文档大小超出＜16MB的限制。");
                }
                
                var buffer2 = binaryReader.ReadBytes(length);
                yield return BsonSerializer.Deserialize(buffer2);
            }
        }
        
    }
}