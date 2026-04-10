using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MaoTouGu.Shells.Generator
{
    public static class GeneratorHelper
    {
        public static string GetTrimmingText(this NameSyntax syntax)
        {
            return syntax.ToFullString()
                         .Replace("\r", "")
                         .Replace("\n", "")
                         .Trim();
        }
    }
}