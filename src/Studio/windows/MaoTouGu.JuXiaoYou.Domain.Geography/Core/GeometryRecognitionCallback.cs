// ----------------------------------------------------------
//            文件：GeometryRecognitionCallback.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月16日 14:48
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Domain.Geography.Core
{
    public delegate void GeometryRecognitionCallback(string fileName,string literalString, int w, int h, Geometry geometry);
}