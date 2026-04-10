// ----------------------------------------------------------
//            文件：CompositableFormElementBuilder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 13:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Services.CFE;

namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public class CompositableFormElementBuilder
    {
        private static readonly List<CFElement> _Elements;

        static CompositableFormElementBuilder()
        {
            _Elements = new List<CFElement>
            {
                new CFSingleLineElement(),
                new CFMultiLineElement(),
                new CFToggleSwitchElement(),
                new CFCheckBoxElement(),
                new CFColorElement(),
                new CFComboBoxElement(),
                new CFUploadElement(),
                new CFStringObjectElement(),
            };
        }

        public static CFElement Build(ICollection<CFAttribute> attributes, PropertyInfo propertyInfo)
        {
            var cfeAttribute = attributes.OfType<CFEAttribute>().FirstOrDefault();
            var cfAttributes = attributes.Where(x => x is not CFEAttribute).ToArray();

            if (cfeAttribute is null)
            {
                Debug.WriteLine($"没有发现{propertyInfo.Name}中声明任何CFEAttribute。");
                return null;
            }

            var builder = _Elements.FirstOrDefault(x => x.CanAccept(cfeAttribute, propertyInfo));

            if (builder is null)
            {
                Debug.WriteLine($"发现了一个CFEAttribute = {cfeAttribute.GetType().Name}，但是没有对应的Builder。");
                return null;
            }

            var element = builder.Accept(cfeAttribute, propertyInfo);

            foreach (var attribute in cfAttributes)
            {
                builder.Accept(attribute);
            }

            return element;
        }
    }
}