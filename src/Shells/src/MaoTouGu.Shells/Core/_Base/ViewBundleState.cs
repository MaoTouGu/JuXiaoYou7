namespace MaoTouGu.Shells
{
    /// <summary>
    /// <see cref="ViewBundleState"/>类型用于表示视图模型绑定状态，决定了View与ViewModel之间的对应关系。
    /// </summary>
    public sealed class ViewBundleState
    {
        public static ViewBundleState Get<TView, TViewModel>() where TView : class
                                                               where TViewModel : ViewModelBase
        {
            return new ViewBundleState(typeof(TView), typeof(TViewModel));
        }

        public ViewBundleState()
        {
        }

        public ViewBundleState(Type v, Type vm)
        {
            View      = v;
            ViewModel = vm;
        }

        public bool IsGenericTypeViewModel => ViewModel.IsGenericType;

        public bool Verify()
        {
            return View is not null && ViewModel is not null;
        }

        public bool Verify(Type vType, Type vmType) => Verify()                   &&
                                                       View.IsAssignableTo(vType) &&
                                                       ViewModel.IsAssignableTo(vmType);

        public Type View      { get; init; }
        public Type ViewModel { get; init; }
    }
}