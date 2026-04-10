using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using MaoTouGu.Foundation;
using MaoTouGu.Shells;

namespace MaoTouGu.JuXiaoYou.Services.Imaging.Caching
{
    public class ImageCacheService : Disposable, IImageCacheService
    {
        internal static readonly ConcurrentDictionary<string, WeakReference<BitmapImage>> _NoneThumb  = new();
        internal static readonly ConcurrentDictionary<string, WeakReference<BitmapImage>> _360pThumb  = new();
        internal static readonly ConcurrentDictionary<string, WeakReference<BitmapImage>> _720pThumb  = new();
        internal static readonly ConcurrentDictionary<string, WeakReference<BitmapImage>> _1080pThumb = new();

        private static readonly ConcurrentDictionary<string, Cache>    _AsyncImageLoadMap   = new();
        private static readonly ConcurrentDictionary<string, Download> _AsyncDownloadMap    = new();
        private static readonly BlockingCollection<Cache>              _AsyncImageLoadQueue = new();
        private static readonly BlockingCollection<Download>           _AsyncDownloadQueue  = new();

        private static readonly CancellationTokenSource _AsyncCTS = new();

        internal static Task<BitmapImage> RequireLocal(string id, string fileName, ThumbnailLevel fallback)
        {
            var table = fallback switch
            {
                ThumbnailLevel.Of360p  => _360pThumb,
                ThumbnailLevel.Of720p  => _720pThumb,
                ThumbnailLevel.Of1080p => _1080pThumb,
                _                      => _NoneThumb,
            };

            if (!_AsyncImageLoadMap.TryGetValue(id, out var task))
            {
                task = new Cache
                {
                    Id       = id,
                    FileName = fileName,
                    Image    = new TaskCompletionSource<BitmapImage>(),
                    Table    = table,
                };

                _AsyncImageLoadMap.TryAdd(id, task);
                _AsyncImageLoadQueue.Add(task);
            }

            return task.Image.Task;
        }

        internal static Task RequireDownload(string id, string dir, string fileName)
        {
            if (!_AsyncDownloadMap.TryGetValue(id, out var task))
            {
                task = new Download
                {
                    Id     = id,
                    Output = fileName,
                    Dir    = dir,
                    File   = new TaskCompletionSource(),
                };

                _AsyncDownloadMap.TryAdd(id, task);
                _AsyncDownloadQueue.Add(task);
            }

            return task.File.Task;
        }

        static void CreateBitmap(MemoryStream ms, Cache work)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource  = ms;
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bi.EndInit();

            //
            // Add LRU
            if (work.Table
                    .TryAdd(work.Id,
                            new WeakReference<BitmapImage>(bi)))
            {
                work.Image
                    .SetResult(bi);

                _AsyncImageLoadMap.TryRemove(work.Id, out var task);
            }
        }

        static async void ImageLoadQueueLoop(object _)
        {
            foreach (var work in _AsyncImageLoadQueue.GetConsumingEnumerable())
            {
                if (_AsyncCTS.IsCancellationRequested)
                {
                    break;
                }

                if (!File.Exists(work.FileName))
                {
                    Debug.WriteLine($"未找到文件 = {work.FileName}");
                    work.Image
                        .SetResult(null);
                    continue;
                }

                await using var stream = File.OpenRead(work.FileName);

                var ms = new MemoryStream();

                try
                {
                    //
                    // Copy
                    await stream.CopyToAsync(ms);

                    //
                    // Reset
                    ms.Seek(0, SeekOrigin.Begin);

                    //
                    // In Sync Thread Create BI
                    GUI.RunOnUIThread(() => CreateBitmap(ms, work));

                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }

        static async void ImageDownloadQueueLoop(object _)
        {
            foreach (var task in _AsyncDownloadQueue.GetConsumingEnumerable())
            {
                if (_AsyncCTS.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    if (!Ioc.IsRegistered<IImageDownloadService>())
                    {
                        task.File.SetResult();
                        continue;
                    }

                    var service = Ioc.Get<IImageDownloadService>();
                    var result  = await service.DownloadImageAsStream(task.Id, task.Dir);

                    if (!result.IsFinished)
                    {
                        task.File.SetResult();
                        continue;
                    }

                    await using var stream = result.Value;
                    await using var fs     = new FileStream(task.Output, FileMode.Create, FileAccess.ReadWrite);


                    //
                    //
                    await stream.CopyToAsync(fs);

                    //
                    //
                    task.File.SetResult();

                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }



        public void Start()
        {
            _AsyncCTS.TryReset();

            //
            //
            ThreadPool.QueueUserWorkItem(ImageLoadQueueLoop);
            ThreadPool.QueueUserWorkItem(ImageDownloadQueueLoop);
        }

        public void Stop()
        {
            _AsyncCTS.Cancel();
        }
    }
}