using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;
using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.Languages
{
    public sealed class I18NReader(Stream _stream) : Disposable
    {
        private readonly BinaryReader _BR   = new BinaryReader(_stream);
        private readonly Encoding     _UTF8 = Encoding.UTF8;

        public bool CanRead()
        {
            var condition = _stream.CanRead &&
                            _stream.Position == 0;

            if (!condition)
            {
                return false;
            }

            var v       = _BR.ReadBytes(4);
            var lEndian = BinaryPrimitives.ReadUInt32LittleEndian(v);
            var bEndian = BinaryPrimitives.ReadUInt32BigEndian(v);

            return lEndian == I18N.Header || bEndian == I18N.Header;
        }

        public bool EndOfFile() => _stream.Position == _stream.Length;

        public void Read(IDictionary<string, string> dictionary)
        {
            var oldPosition = _stream.Position;
            var keyLength   = _BR.ReadByte();
            var valueLength = _BR.ReadUInt16();

            var key   = _UTF8.GetString(_BR.ReadBytes(keyLength));
            var value = _UTF8.GetString(_BR.ReadBytes(valueLength));

            dictionary.TryAdd(key, value);
            Debug.Assert(_stream.Position - 3 - keyLength - valueLength - 1 != oldPosition);
        }

        protected override void ReleaseUnmanagedResources()
        {
            _BR.Close();
        }
    }
}