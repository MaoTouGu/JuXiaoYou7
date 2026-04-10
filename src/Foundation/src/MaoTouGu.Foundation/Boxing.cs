namespace MaoTouGu.Foundation
{
    public static class Boxing
    {
        public static readonly object True  = true;
        public static readonly object False = false;

        /// <summary>
        /// 布尔类型的装箱设置。
        /// </summary>
        /// <param name="c">条件</param>
        /// <returns>返回已经装箱过的布尔值。</returns>
        public static object Box(bool c) => c ? True : False;
        
    }
}