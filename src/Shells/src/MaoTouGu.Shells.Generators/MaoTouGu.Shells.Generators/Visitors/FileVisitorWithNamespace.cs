using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MaoTouGu.Shells.Generator.Visitors
{
    /// <summary>
    /// <see cref="FileVisitorWithNamespace"/> 文件遍历器，用于实现命名空间的搜集。
    /// </summary>
    public sealed class FileVisitorWithNamespace : CSharpSyntaxWalker
    {
        public readonly HashSet<string> _pool;

        public FileVisitorWithNamespace()
        {
            _pool = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public string GetNamespaceBlock()
        {
            var sb = new StringBuilder();
            foreach (var ns in _pool)
            {
                sb.Append(ns);
            }

            return sb.ToString();
        }
        
        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var ns = GetNamespace(node);
            _pool.Add($"using {ns};\n");
            base.VisitNamespaceDeclaration(node);
        }

        public static string GetNamespace(NamespaceDeclarationSyntax node)
        {
            return node?.Name.GetTrimmingText();
        }

        public IEnumerable<string> Namespaces => _pool;
    }
}