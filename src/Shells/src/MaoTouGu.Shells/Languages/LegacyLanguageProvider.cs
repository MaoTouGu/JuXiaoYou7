using System.Text;
using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.Languages
{
    public class LegacyLanguageProvider(Stream _Stream) : Disposable, ILanguageProvider
    {
        public IReadOnlyList<string> GetUnformattedLines()
        {
            var list = new List<string>(128);

            if (_Stream is null)
            {
                return Array.Empty<string>();
            }

            using (var sr = new StreamReader(_Stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                        list.Add(line);
                }
            }

            return list;
        }

        public void Provide(IDictionary<string, string> dictionary)
        {
            var unformattedLines = GetUnformattedLines();
            var sb               = new StringBuilder();

            //
            //
            if (unformattedLines is null)
            {
                return;
            }

            foreach (var line in unformattedLines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                // 方法1 ：Formatted(sb, line, _Global);


                var segments = line.Split('=');

                if (string.IsNullOrEmpty(segments[0]) ||
                    string.IsNullOrEmpty(segments[1]))
                    continue;

                var key   = segments[0].Trim();
                var value = segments[1].Trim();

                sb.Append(value);
                sb.Replace(@"\x20", "\x20");
                sb.Replace(@"\t", "\t");
                sb.Replace(@"\n", Environment.NewLine);

                dictionary.TryAdd(key, sb.ToString());
                sb.Clear();
            }
        }
        
        public static LegacyLanguageProvider UseLegacyAssembly<E>(string manifestName) where E : class
        {
            if (string.IsNullOrEmpty(manifestName))
            {
                return null;
            }

            var assembly      = typeof(E).Assembly;
            var manifestSteam = assembly.GetManifestResourceStream(manifestName);

            if (manifestSteam is null)
            {
                return null;
            }

            return new LegacyLanguageProvider(manifestSteam);
        }
    }
}