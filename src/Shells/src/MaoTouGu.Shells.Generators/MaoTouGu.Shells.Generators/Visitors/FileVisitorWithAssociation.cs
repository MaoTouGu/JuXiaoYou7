using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MaoTouGu.Shells.Generator.Visitors
{
    
    public class Page
    {
        public string View      { get; set; }
        public string ViewModel { get; set; }
    }
    
    public sealed class FileVisitorWithAssociation : CSharpSyntaxWalker
    {
        
        private const    string       PropertyDefaultString = "\"\"";
        private readonly List<string> _tokenInfo;
        private readonly List<string> _metadata;

        private const string add_view = "collection.Add(new ViewBundleState(typeof({0}),typeof({1})));\n";
        private const string add_property = "collection.Add(new ViewBundleState{{ Id = {0}, Group = {1}, Name = {2}, UseResourceKey = {3} }});\n";

        public FileVisitorWithAssociation()
        {
            _tokenInfo = new List<string>(128);
            _metadata = new List<string>(128);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            foreach (var pageInfo in node.AttributeLists
                                         .Select(attr => attr.Attributes.FirstOrDefault(x => x.Name.ToFullString() == "Associate"))
                                         .Where(pageInfo => !(pageInfo?.ArgumentList is null)))
            {
                GetAssociateAttribute(pageInfo.ArgumentList);
            }
        }

        private void GetAssociateAttribute(AttributeArgumentListSyntax info)
        {
            var pInfo = new Page();

            foreach (var arg in info.Arguments)
            {
                var name = arg.NameEquals?.ToFullString().Replace("=", "").Trim();
                var value = arg.Expression.ToFullString();

                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                switch (name)
                {
                    case "ViewModel":
                        pInfo.ViewModel = value;
                        break;
                    case "View":
                        pInfo.View = value;
                        break;
                }
            }
            _tokenInfo.Add(string.Format(
                add_view,
                GetTypeOfString(pInfo.View),
                GetTypeOfString(pInfo.ViewModel)));
        }

        private static string IsDefault(string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        private static string GetTypeOfString(string raw)
        {
            return raw.Length <= 8 ? raw : raw.Substring(7, raw.Length - 8);
        }

        public IEnumerable<string> ViewBundleStates => _tokenInfo;
    }
}