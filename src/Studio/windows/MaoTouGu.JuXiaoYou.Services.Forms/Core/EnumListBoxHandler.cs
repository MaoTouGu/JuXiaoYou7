// ----------------------------------------------------------
//            文件：EnumListBoxHandler.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public class EnumListBoxHandler : ICFListBoxHandler
    {
        public EnumListBoxHandler(Type enumType)
        {
            ItemsSource = Enum.GetValues(enumType)
                              .Cast<object>()
                              .ToArray();

            ValueSelector  = SelectSelf;
            ObjectSelector = SelectSelf;
            Template       = DataTemplateBuilder.BuildEnumTemplate();
        }

        static object SelectSelf(object x) => x;
        static object SelectSelf(IEnumerable<object> collection, object x) => x;
        
        
        public ICFObjectContext GetContext(int index) => null;

        public IEnumerable<object>     ItemsSource    { get; }
        public CFListBoxValueSelector  ValueSelector  { get; }
        public CFListBoxObjectSelector ObjectSelector { get; }
        
        public DataTemplate Template { get; }
    }
}