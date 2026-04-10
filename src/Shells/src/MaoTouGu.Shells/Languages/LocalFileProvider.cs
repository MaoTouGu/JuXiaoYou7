using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.Languages
{
    public class LocalFileProvider : Disposable, ILanguageProvider
    {
        private readonly I18NReader _reader;

        public LocalFileProvider(Stream stream)
        {
            _reader = new I18NReader(stream);
        }
        
        public LocalFileProvider(string fileName) : this(File.OpenRead(fileName))
        {
            
        }
        
        public void Provide(IDictionary<string, string> dictionary)
        {
            if (!_reader.CanRead())
            {
                return;
            }

            while (!_reader.EndOfFile())
            {
                _reader.Read(dictionary);
            }
        }
    }
}