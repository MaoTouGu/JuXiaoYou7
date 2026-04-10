// ----------------------------------------------------------
//            文件：WebApi.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 17:00
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using LiteDB;
using MaoTouGu.Foundation;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Shells.Core;
using MaoTouGu.Studio.Database;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    public partial class RemoteApi : Disposable, IImageDownloadService, IWebApi
    {

        public RemoteApi(string url, bool userProxy = true, string cookie = null)
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies      = true,
                UseProxy        = userProxy,
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
            };

            Client  = client;
            Handler = handler;

            if (!string.IsNullOrEmpty(cookie))
            {
                handler.CookieContainer.Add(new Cookie());
            }

            Url       = url;
            SafetyUrl = url.EndsWith('/') ? url[..^1] : url;
        }

        //-------------------------------------------------------------
        //
        //                     Protected
        //
        //-------------------------------------------------------------
        protected override void ReleaseUnmanagedResources()
        {
            Client.Dispose();
        }

        //-------------------------------------------------------------
        //
        //                     Helper
        //
        //-------------------------------------------------------------


        async Task<Result<User>> PostJsonAndReturnUser(string url, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                var r = await Client.PostAsync(url, content);

                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result<User>.Failed("禁止访问");
                }

                var reason = await r.Content.ReadAsStringAsync();
                var user = JSON.Deserialize<Result<User>>(reason);


                if (r.IsSuccessStatusCode)
                {
                    return user;
                }

                return Result<User>.Failed(user.Reason);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<User>.Failed(e.Message);
            }
        }

        async Task<Result> PostJsonAndReturnResult(string url, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                var r = await Client.PostAsync(url, content);

                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result.Failed("禁止访问");
                }

                if (!r.IsSuccessStatusCode)
                {
                    return Result.Failed("禁止访问");
                }

                var reason = await r.Content.ReadAsStringAsync();

                return JSON.Deserialize<Result>(reason);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result.Failed(e.Message);
            }
        }

        async Task<Result> GetAndReturnResult(string url)
        {
            try
            {
                var r = await Client.GetAsync(url);


                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result.Failed("禁止访问");
                }

                if (r.StatusCode == HttpStatusCode.OK)
                {
                    return Result.Success();
                }

                var reason = await r.Content.ReadAsStringAsync();

                return JSON.Deserialize<Result>(reason);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result.Failed(e.Message);
            }
        }

        async Task<Result<string>> GetAndReturnResultString(string url)
        {
            try
            {
                var r = await Client.GetAsync(url);


                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result<string>.Failed("禁止访问");
                }

                if (r.StatusCode != HttpStatusCode.OK)
                {
                    return Result<string>.Failure;
                }

                var reason = await r.Content.ReadAsStringAsync();

                return JSON.Deserialize<Result<string>>(reason);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<string>.Failed(e.Message);
            }
        }

        async Task<Result<User>> GetAndReturnUser(string url)
        {
            try
            {
                var r = await Client.GetAsync(url);

                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result<User>.Failed("禁止访问");
                }

                if (!r.IsSuccessStatusCode)
                {
                    return Result<User>.Failed(r.StatusCode.ToString());
                }

                var value = await r.Content.ReadAsStringAsync();
                return JSON.Deserialize<Result<User>>(value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<User>.Failed(e.Message);
            }
        }

        async Task<Result<List<User>>> GetAndReturnJson(string url)
        {
            try
            {
                var r = await Client.GetAsync(url);

                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result<List<User>>.Failed("禁止访问");
                }

                if (!r.IsSuccessStatusCode)
                {
                    return Result<List<User>>.Failed(r.StatusCode.ToString());
                }

                var value = await r.Content.ReadAsStringAsync();
                return JSON.Deserialize<Result<List<User>>>(value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<List<User>>.Failed(e.Message);
            }
        }

        async Task<Result<string>> GetAndReturnJsonString(string url)
        {
            try
            {
                var r = await Client.GetAsync(url);

                if (r.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Result<string>.Failed("未登录或者无权限");
                }

                if (r.StatusCode == HttpStatusCode.Forbidden)
                {
                    return Result<string>.Failed("禁止访问");
                }

                if (!r.IsSuccessStatusCode)
                {
                    return Result<string>.Failed(r.StatusCode.ToString());
                }

                var value = await r.Content.ReadAsStringAsync();
                return JSON.Deserialize<Result<string>>(value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<string>.Failed(e.Message);
            }
        }

        async Task<Result<IEnumerable<BsonDocument>>> GetBsonDocument(string url)
        {
            try
            {
                var r = await Client.GetAsync(url);

                if (r.IsSuccessStatusCode)
                {
                    var bytes     = await r.Content.ReadAsByteArrayAsync();
                    var documents = LiteCollectionExtensions.DeserializeCollection(bytes);

                    return Result<IEnumerable<BsonDocument>>.Success(documents);
                }
                return null;
            }
            catch(Exception e)
            {
                return Result<IEnumerable<BsonDocument>>.Failed(e.Message);
            }
        }


        static MemoryStream Unzip(byte[] buffer)
        {
            var       decompressedStream = new MemoryStream();
            using var memoryStream       = new MemoryStream(buffer);
            using var gzipStream         = new GZipStream(memoryStream, CompressionMode.Decompress);

            gzipStream.CopyTo(decompressedStream);
            decompressedStream.Seek(0, SeekOrigin.Begin);

            return decompressedStream;
        }

        //-------------------------------------------------------------
        //
        //                     Public
        //
        //-------------------------------------------------------------

        public Dictionary<string, string> GetCookie()
        {
            return Handler.CookieContainer
                          .GetAllCookies()
                          .Select(x => new KeyValuePair<string, string>(x.Name, x.Value))
                          .ToDictionary();
        }

        public string GetCookieAsJson()
        {
            return JSON.Serialize<Dictionary<string, string>>(GetCookie());
        }

        public byte[] GetCookieAsBinary()
        {
            var ms     = new MemoryStream();
            var buffer = Encoding.UTF8.GetBytes(GetCookieAsJson());
            var gzip   = new GZipStream(ms, CompressionLevel.SmallestSize);

            //
            //
            gzip.Write(buffer);
            gzip.Dispose();

            return ms.ToArray();
        }

        public List<Cookie> GetCookiesFromBinary(byte[] buffer)
        {
            using var ms   = Unzip(buffer);
            var       json = Encoding.UTF8.GetString(ms.ToArray());


            return GetCookiesFromJson(json);
        }

        public List<Cookie> GetCookiesFromJson(string json)
        {
            var dict           = JSON.Deserialize<Dictionary<string, string>>(json);
            var cookieSelector = dict.Select(x => new Cookie(x.Key, x.Value));

            return cookieSelector.ToList();
        }

        public void SaveCookieTo(string fileName)
        {
            File.WriteAllBytes(fileName, GetCookieAsBinary());
        }

        public void LoadCookieFrom(string fileName)
        {
            var buffer = File.ReadAllBytes(fileName);
            var list   = GetCookiesFromBinary(buffer);

            list.ForEach(Handler.CookieContainer.Add);
        }

        public void LoadCookieFromTemporary()
        {
            var temp     = Path.GetTempPath();
            var fileName = Path.Combine(temp, $"JuXiaoYou_7_{DateTime.Now:yyyy_MM_dd}.cookie.json");

            LoadCookieFrom(fileName);
        }

        public string SaveCookieToTemporary()
        {
            var temp     = Path.GetTempPath();
            var fileName = Path.Combine(temp, $"JuXiaoYou_7_{DateTime.Now:yyyy_MM_dd}.cookie.json");

            SaveCookieTo(fileName);

            return temp;
        }

        //-------------------------------------------------------------
        //
        //                     Helper
        //
        //-------------------------------------------------------------

        public bool   IsOnline => true;
        public string UserID   { get; set; }
        public User   User     { get; set; }

        public string Url       { get; }
        public string SafetyUrl { get; }

        //-------------------------------------------------------------
        //
        //                        Download
        //
        //-------------------------------------------------------------

        public HttpClient        Client  { get; }
        public HttpClientHandler Handler { get; }
    }
}