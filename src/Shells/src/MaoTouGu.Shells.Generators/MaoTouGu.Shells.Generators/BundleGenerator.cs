using System.Diagnostics;
using System.Text;
using MaoTouGu.Shells.Generator.Visitors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace MaoTouGu.Shells.Generator
{
    [Generator]
    public class BundleGenerator : ISourceGenerator
    {

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var visitor = new FileVisitor();

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var root = syntaxTree.GetRoot();
                visitor.Visit(root);
            }

            // var nsStub = nsVisitor.GetNamespaceBlock();
            // textBuilder.Clear();
            //
            // foreach (var ns in visitor.ViewBundleStates)
            // {
            //     if (string.IsNullOrEmpty(ns))
            //     {
            //         continue;
            //     }
            //
            //     textBuilder.Append(ns);
            // }
            //
            // var pageStub = textBuilder.ToString();
            // textBuilder.Clear();
            //
            // foreach (var bi in visitor.MetadataInfo)
            // {
            //     if (string.IsNullOrEmpty(bi))
            //     {
            //         continue;
            //     }
            //
            //     textBuilder.Append(bi);
            // }
            //
            // var propStub = textBuilder.ToString();
            // textBuilder.Clear();
            //
            //
            // var code = string.Format(Template, nsStub, pageStub, propStub);
            var code      = visitor.GetCode();
            var newSource = SourceText.From(code, Encoding.UTF8);
            
            context.AddSource("KinonekoSoftware.Shells.Generator.Generated.cs", newSource);
        }
    }
}