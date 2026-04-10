using MaoTouGu.Shells.Languages;

namespace MaoTouGu.Shells
{
    public static class Dialog
    {
        /// <summary>
        /// 打开对话框。
        /// </summary>
        /// <param name="target">要打开的对话框</param>
        /// <typeparam name="T">对话框的类型。</typeparam>
        /// <returns>返回对话框实例。</returns>
        public static T AddDialog<T>(T target) where T : DialogBase
        {
            if (Ioc.SafeGet<IAppModel>() is not {} locator)
            {
                return target;
            }
            
            if (Ioc.SafeGet<IDialogService>() is not {} service)
            {

                target.Logger.Warn("无法弹出对话框，此应用不实现IDialogService服务");
                return target;
            }

            service.OpenDialog(target, locator);
            return target;
        }

        /// <summary>
        /// 打开对话框。
        /// </summary>
        /// <param name="page">要打开的对话框</param>
        /// <param name="dialog">要打开的对话框</param>
        /// <typeparam name="T">对话框的类型。</typeparam>
        /// <returns>返回对话框实例。</returns>
        internal static T AddDialog<T>(PageBase page, T dialog) where T : DialogBase
        {

            if (Ioc.SafeGet<IAppModel>() is not {} locator)
            {
                page.Logger.Warn("无法弹出对话框，此应用不实现IViewLocator服务");
                return dialog;
            }

            if (locator.GetDialogHost(page) is not {} service)
            {
                page.Logger.Warn("无法弹出对话框，此应用不实现IDialogService服务");
                return dialog;
            }

            dialog.Owner = page;
            service.OpenDialog(dialog, locator);

            return dialog;
        }

        /// <summary>
        /// 打开对话框。
        /// </summary>
        /// <param name="dialog">要打开的对话框</param>
        /// <param name="target">要打开的对话框</param>
        /// <typeparam name="T">对话框的类型。</typeparam>
        /// <returns>返回对话框实例。</returns>
        internal static T AddDialog<T>(DialogBase dialog, T target) where T : DialogBase
        {
            if (Ioc.SafeGet<IAppModel>() is not {} locator)
            {
                dialog.Logger.Warn("无法弹出对话框，此应用不实现IViewLocator服务");
                return target;
            }
            
            //
            //
            var page = dialog.Owner;
            
            target.Owner = dialog.Owner;
            
            //
            //
            if (locator.GetDialogHost(page) is not {} service)
            {
                page.Logger.Warn("无法弹出对话框，此应用不实现IDialogService服务");
                return target;
            }
            
            //
            //
            service.OpenDialog(target, locator);

            return target;
        }

        public static IViewBundleStateProvider UseBuiltinViews()
        {
            return new DialogBundle();
        }
        
        sealed class DialogBundle : IViewBundleStateProvider
        {

            public IEnumerable<ViewBundleState> Provide() => new []
            {
                new ViewBundleState(typeof(EnumPickerView), typeof(EnumPickerRoot<>)),
                new ViewBundleState(typeof(NumericInputView), typeof(NumericInputRoot)),
                new ViewBundleState(typeof(QuestionView), typeof(QuestionRoot)),
                new ViewBundleState(typeof(TextInputView), typeof(TextInputRoot)),
                new ViewBundleState(typeof(NotifyView), typeof(NotifyRoot)),
                new ViewBundleState(typeof(QuadOptionView), typeof(QuadOptionRoot)),
                new ViewBundleState(typeof(TripleOptionView), typeof(TripleOptionRoot)),
            };
        }
    }
}