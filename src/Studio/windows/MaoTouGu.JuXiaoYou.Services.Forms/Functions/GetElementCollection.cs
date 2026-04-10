// ----------------------------------------------------------
//            文件：GetElementCollection.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 12:58
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Attributes.CFE;

namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public static partial class CompositableForm
    {
        private static readonly Dictionary<Type, CFElementCollection> _dictionary = new();

        private static CFElementCollection GetElementCollection(Type maybeFormElementType)
        {
            var properties = maybeFormElementType.GetProperties(BindingFlags.Instance | 
                                                                BindingFlags.Public | 
                                                                BindingFlags.GetProperty | 
                                                                BindingFlags.SetProperty);
            var collection = new CFElementCollection();
            
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes<CFAttribute>().ToArray();
                var element    = CompositableFormElementBuilder.Build(attributes, property);

                //
                // 只有CFAttr没有CFEAttr
                if (element is null)
                {
                    continue;
                }
                
                collection.Add(element);
            }

            return collection;
        }

        public static Result<CFElementCollection> GetElementCollection(object maybeFormElement)
        {
            if (maybeFormElement is null)
            {
                return Result<CFElementCollection>.Failed("对象为空");
            }

            var maybeFormElementType = maybeFormElement.GetType();
            
            //
            // 每次都从反射中构建，其实性能还是很差的
            // 要想办法从缓存中读取。
            if (_dictionary.TryGetValue(maybeFormElementType, out var collection))
            {
                return Result<CFElementCollection>.Success(collection.Clone());
            }

            collection = GetElementCollection(maybeFormElementType);
            
            if(collection.Count == 0)
            {
                return Result<CFElementCollection>.Failed("没有可供编辑的参数。");
            }
            
            _dictionary.TryAdd(maybeFormElementType, collection);
            
            return Result<CFElementCollection>.Success(collection.Clone());
        }
    }
}