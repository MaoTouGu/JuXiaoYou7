using System.Text;
using Microsoft.CodeAnalysis;

namespace MaoTouGu.Shells.Generator.Visitors
{
    public class FileVisitor
    {
        public const string BundleFileTemplate = @"
using System.Collections.Generic;
using MaoTouGu.Shells;
using MaoTouGu.Shells.Attributes;
{0}

namespace MaoTouGu.Shells.Generators
{{
    public partial class {3}BundleStateProvider: IViewBundleStateProvider
    {{
        public IEnumerable<ViewBundleState> Provide()
        {{
            var collection = new List<ViewBundleState>();
            AddViewModels(collection);

return collection;
        }}
                        
        public void AddViewModels(ICollection<ViewBundleState> collection)
        {{
            {1}
        }}
    }}
}};";

        public void Visit(SyntaxNode root)
        {
            WithNamespace.Visit(root);
            WithGeneratedTarget.Visit(root);
            WithAssociation.Visit(root);
        }

        public string GetCode()
        {
            var sb = new StringBuilder();

            foreach (var ns in WithNamespace.Namespaces)
            {
                sb.Append(ns);
            }

            var @namespace = sb.ToString();
            sb = sb.Clear();
            
            foreach (var ns in WithAssociation.ViewBundleStates)
            {
                sb.Append(ns);
            }

            var content = sb.ToString();

            return string.Format(BundleFileTemplate, @namespace, content, null, WithGeneratedTarget.Target);
        }

        public FileVisitorWithNamespace       WithNamespace       { get; } = new FileVisitorWithNamespace();
        public FileVisitorWithAssociation     WithAssociation     { get; } = new FileVisitorWithAssociation();
        public FileVisitorWithGeneratedTarget WithGeneratedTarget { get; } = new FileVisitorWithGeneratedTarget();
    }
}