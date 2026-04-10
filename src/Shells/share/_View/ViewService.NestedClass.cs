using System.Diagnostics;

namespace MaoTouGu.Shells.Core
{
    partial class ViewService
    {
        

        private static readonly Lazy<ViewService> _lazyViewSRV = new Lazy<ViewService>(Ioc.Get<ViewService>);

        public static ViewService Instance => _lazyViewSRV.Value;
    }
}