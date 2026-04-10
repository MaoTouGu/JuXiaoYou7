// ----------------------------------------------------------
//            文件：ServerHealth.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 17:58
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Net.Http;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    public static class ServerHealth
    {
        public static async Task<bool> IsAlive(string url)
        {
            var url2 = url.EndsWith('/') ? url[..^1] : url;
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                try
                {

                    var sr = await client.GetStringAsync($"{url2}/api/Data");
                    return sr == "Hi";
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}