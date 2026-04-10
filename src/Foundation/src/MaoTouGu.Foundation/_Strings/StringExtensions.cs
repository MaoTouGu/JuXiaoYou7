using System.Text;

namespace MaoTouGu.Foundation
{
    public static class StringExtensions
    {
        public const string OnlyDigit      = "0123456789";
        public const string OnlyLetter     = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string DigitAndLetter = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static async Task<string> EncodeBase64(string fileName)
        {
            var buffer = await File.ReadAllBytesAsync(fileName);
            var base64 = Convert.ToBase64String(buffer);

            return base64;
        }
        public static  Task<string> EncodeBase64(byte[] buffer)
        {
            return Task.Run(() => Convert.ToBase64String(buffer));
        }

        static string RandomString(string pattern, int length)
        {
            var sb  = new StringBuilder();
            var len = pattern.Length - 1;

            length = Math.Clamp(length, 0, short.MaxValue);

            for (var i = 0; i < length; i++)
            {
                var idx = Random.Shared.Next(0, len);
                sb.Append(pattern[idx]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 随机生成一个纯数字的文本字符串。
        /// </summary>
        /// <param name="length">长度，0~32767之间。</param>
        /// <returns>返回一个纯数字的文本字符串。</returns>
        public static string RandomDigitString(int length) => RandomString(OnlyDigit, length);
        
        /// <summary>
        /// 随机生成一个纯英文字符的文本字符串。
        /// </summary>
        /// <param name="length">长度，0~32767之间。</param>
        /// <returns>返回一个纯英文字符的文本字符串。</returns>
        public static string RandomLetterString(int length) => RandomString(OnlyLetter, length);
        
        /// <summary>
        /// 随机生成一个由英文字符与数字组成的文本字符串。
        /// </summary>
        /// <param name="length">长度，0~32767之间。</param>
        /// <returns>随机生成一个由英文字符与数字组成的文本字符串。</returns>
        public static string RandomDigitAndLetterString(int length) => RandomString(DigitAndLetter, length);

        public static string Clamp(this string str, int length = 25)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            if (str.Length >= length)
            {
                return str.Substring(0, length);
            }

            return str;
        }
    }
}