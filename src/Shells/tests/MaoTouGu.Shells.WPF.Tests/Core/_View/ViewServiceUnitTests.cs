using System.Windows;

namespace MaoTouGu.Shells.Core
{
    public class ViewServiceUnitTests
    {
        sealed class EnumPickerView : FrameworkElement {}
        sealed class EnumPickerRoot<T> : DialogBase where T : class{}
        sealed class EnumPickerViewModel<T> : DialogBase where T : class{}
        sealed class EnumPickerButErrorRoot<T> where T : class{}
        sealed class EnumPickerButErrorViewModel<T> where T : class{}
        sealed class EnumPickerRoot: DialogBase{}
        sealed class EnumPickerViewModel: DialogBase{}

        public ViewServiceUnitTests()
        {
            
        }
        
        [UIFact]
        public void Should_ReturnViewNotNull_GenericViewModel()
        {
            var srv = new ViewService();

            srv.InstallView(typeof(EnumPickerView), typeof(EnumPickerRoot<>));

            var v = srv.GetView(new EnumPickerRoot<string>());


            Assert.NotNull(v);
            Assert.NotNull(v.DataContext);
            Assert.Equal(typeof(EnumPickerRoot<string>), v.DataContext?.GetType());
        }
        
        [UIFact]
        public void Should_ReturnViewNotNull_NonGenericViewModel()
        {
            var srv = new ViewService();

            srv.InstallView(typeof(EnumPickerView), typeof(EnumPickerRoot));

            var v = srv.GetView(new EnumPickerRoot());


            Assert.NotNull(v);
            Assert.NotNull(v.DataContext);
            Assert.Equal(typeof(EnumPickerRoot), v.DataContext?.GetType());
        }
    }
}