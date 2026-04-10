using System.Reflection;
using Avalonia.Input;

namespace KinonekoSoftware.UI.Inputs
{
    public class PointerPointPropertiesExtension
    {
        private static Type         _pointerType;
        private static PropertyInfo _pointerPropertyInfo;
        
        public static PointerPointProperties GetProperties(PointerEventArgs args)
        {
            _pointerType         ??= typeof(PointerEventArgs);
            _pointerPropertyInfo ??= _pointerType.GetProperty("Properties", BindingFlags.NonPublic | BindingFlags.Instance);
            return (PointerPointProperties)(_pointerPropertyInfo?.GetValue(args)!);
        }
    }
}