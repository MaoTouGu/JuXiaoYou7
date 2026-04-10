using System.Collections.Concurrent;
using System.Windows.Media.Imaging;

namespace MaoTouGu.JuXiaoYou.Services.Imaging.Caching
{
    internal class Cache
    {
        public string Id       { get; init; }
        public string FileName { get; init; }
        public string Dir      { get; init; }

        public ConcurrentDictionary<string, WeakReference<BitmapImage>> Table { get; init; }

        public TaskCompletionSource<BitmapImage> Image { get; init; }
    }
}