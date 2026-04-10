namespace MaoTouGu.Foundation
{
    public static class DirectoryExt
    {
        public static string Combine(params string[] args)
        {
            var path = Path.Combine(args);

            return GetOrCreate(path);
        }
        
        
        public static string GetOrCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}