using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MaoTouGu.Shells.Generator.Visitors
{
    public sealed class FileVisitorWithGeneratedTarget : CSharpSyntaxWalker
    {
        public FileVisitorWithGeneratedTarget()
        {
        }

        public override void VisitAttributeList(AttributeListSyntax node)
        {
            foreach (var attr in node.Attributes)
            {
                if (attr.Name.ToFullString() == "GeneratedTarget" && attr.ArgumentList is not null)
                {
                    foreach (var arg in attr.ArgumentList.Arguments)
                    {
                        Target = arg.Expression.ToFullString().Replace("\"", "");
                    }
                }
                
            }
            base.VisitAttributeList(node);
        }
        public string Target { get; private set; }
    }
}