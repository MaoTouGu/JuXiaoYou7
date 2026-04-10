// ----------------------------------------------------------
//            文件：DataSourceColumn.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 13:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    public class DataSourceColumn
    {
        public DataSourceColumn(){}
        public DataSourceColumn(string name) => Name = name;
        
        public string Name { get; init; }
    }
}