/****************************************
 *
 *          Author: Luoyisi
 *         Created: 2025/11/23
 *
 ****************************************/



using System.Diagnostics;
using MaoTouGu.Foundation.Core;
using NLog;

namespace MaoTouGu.Shells.Core
{
    public sealed partial class ViewService : Disposable, IViewLocator, IViewAmbient2
    {
        private static readonly Type _VType;
        private static readonly Type _VMType;

        static ViewService()
        {
            _VType  = typeof(FrameworkElement);
            _VMType = typeof(ViewModelBase);
        }

        private readonly Dictionary<Type, ViewBundleState> _nonGenericDictionary;
        private readonly Dictionary<Type, ViewBundleState> _genericDictionary;
        private readonly Dictionary<Type, ViewBundleState> _genericCacheDictionary;


        private readonly ILogger _logger;

        public ViewService()
        {
            _nonGenericDictionary   = new Dictionary<Type, ViewBundleState>();
            _genericDictionary      = new Dictionary<Type, ViewBundleState>();
            _genericCacheDictionary = new Dictionary<Type, ViewBundleState>();

            _logger = LoggerExt.GetLogger<ViewService>();
        }

        //-------------------------------------------------------------
        //
        //                       GetView
        //
        //-------------------------------------------------------------
        #region GetView

        object IViewLocator.GetView(ViewModelBase target) => GetView(target);

        public FrameworkElement GetView(ViewModelBase target)
        {
            if (target is null)
            {
                _logger.Warn("无法为值为null的VM返回对应的View。");
                return null;
            }

            var vmType = target.GetType();
            var vmName = vmType.Name;

            FrameworkElement view  = null;
            ViewBundleState  state = null;

            if (vmType.IsGenericType)
            {
                //
                // 例如：
                // 注册时使用的是EnumPickerRoot<>，但是在实际获取的时候会变成EnumPickerRoot<string>
                // 这时候只有Type.Name都是一致的。

                if (!_genericCacheDictionary.TryGetValue(vmType, out state))
                {
                    var baseType = _genericDictionary.Values.FirstOrDefault(x => x.ViewModel.Name == vmName);

                    if (baseType is not null)
                    {
                        _genericCacheDictionary.Add(vmType, baseType);
                        _logger.Info($"将VM{vmName}与{baseType.ViewModel}对应。");
                        state = baseType;

                    }
                    else
                    {
                        _logger.Warn($"无法找到此VM：{vmName}对应的View。");
                    }
                }
            }
            else if (!_nonGenericDictionary.TryGetValue(vmType, out state))
            {
                _logger.Warn("无法找到此VM对应的View。");
            }

            if (state is not null)
            {
                view = Activator.CreateInstance(state.View) as FrameworkElement;

                if (view is not null)
                {
                    view.DataContext = target;
                }
            }

            return view;
        }

        #endregion



        //-------------------------------------------------------------
        //
        //                     InstallView
        //
        //-------------------------------------------------------------
        #region InstallView

        static bool Verify(ViewBundleState state) => state?.Verify(_VType, _VMType) ?? false;

        void InstallViewImpl(ViewBundleState state)
        {
            var dict = state.ViewModel.IsGenericType ? _genericDictionary : _nonGenericDictionary;

            dict.TryAdd(state.View, state);
            dict.TryAdd(state.ViewModel, state);
        }

        public void InstallView(ViewBundleState state)
        {
            if (state is null)
            {
                return;
            }

            if (!Verify(state))
            {
                return;
            }

            InstallViewImpl(state);
        }

        public void InstallView(Type vType, Type vmType)
        {
            if (vType is null || vmType is null)
            {
                return;
            }

            var state = new ViewBundleState(vType, vmType);

            if (!Verify(state))
            {
                return;
            }

            InstallViewImpl(state);
        }

        public void InstallView(IViewBundleStateProvider provider)
        {
            if (provider is null)
            {
                return;
            }

            foreach (var state in provider.Provide())
            {
                InstallView(state);
            }
        }

        #endregion

        public int Count => _genericDictionary.Count + _nonGenericDictionary.Count;

    }
}