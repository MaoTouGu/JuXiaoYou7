// ----------------------------------------------------------
//            文件：ITextTarget.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database.Topology
{
    public interface ITextTarget : INotifyPropertyChangedEX
    {
        public Int32Thickness Padding { get; set; }

        public Int32Thickness BorderThickness { get; set; }
        
        public string BorderBrush { get; set; }

        public Int32CornerRadius CornerRadius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Top</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Bottom</para>
        /// </remarks>
        public int VerticalAlignment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Left</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Right</para>
        /// </remarks>
        public int HorizontalAlignment { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Left</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Right</para>
        /// <para>3，代表Justify</para>
        /// </remarks>
        public int TextAlignment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///
        /// <para>0 = Thin </para>
        /// <para>1 = Light </para>
        /// <para>2 = Normal </para>
        /// <para>3 = Bold </para>
        /// <para>4 = Black </para>
        /// </remarks>
        public int FontWeight { get; set; }

        public int FontSize  { get; set; }
        public bool IsBold { get; set; }

        public string FontFamily { get; set; }

        public string Foreground { get; set; }

        public string Background { get; set; }
    }
}