namespace MaoTouGu.Shells.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class LocalizedStringAttribute : Attribute
    {
        public LocalizedStringAttribute(){}
        
        public LocalizedStringAttribute(string lcid, string text)
        {
            LCID = lcid;
            Text = text;
        }
        
        public string LCID { get; init; }
        public string Text { get; init; }
    }
}