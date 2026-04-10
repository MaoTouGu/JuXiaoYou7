using System.Text;
using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.Languages
{
    public sealed class I18NWriter(Stream _stream) : Disposable
    {
        private readonly Encoding     _UTF8 = Encoding.UTF8;
        private readonly BinaryWriter _BW = new BinaryWriter(_stream);

        public void Begin()
        {
            _BW.Write(I18N.Header);
        }

        public void Write(string key, string value)
        {
            var kBuffer = _UTF8.GetBytes(key);
            var vBuffer = _UTF8.GetBytes(value);
            var kLen    = (byte)kBuffer.Length;
            var vLen    = (ushort)vBuffer.Length;
            
            _BW.Write(kLen);
            _BW.Write(vLen);
            _BW.Write(kBuffer);
            _BW.Write(vBuffer);
        }
        
        protected override void ReleaseUnmanagedResources()
        {
            _BW.Flush();
            _BW.Close();
        }
    }
}