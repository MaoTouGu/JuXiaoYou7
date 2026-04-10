using MaoTouGu.Shells.Core;
using NLog;
using System.Windows.Interop;

namespace MaoTouGu.Shells.Controls
{
    public partial class DialogHost : IDialogService
    {
        public void OpenDialog(DialogBase target) => ShowDialog(target);
        
        public void ShowDialog(DialogBase target)
        {
            if (target is null)
            {
                return;
            }

            //
            //
            try
            {
                //
                // 判断是否已经有打开的对话框，如果有调用Suspend方法。
                if (_stack.Count > 0)
                {
                    _stack.Peek().Suspend();
                }

                target.CloseHandler = CloseDialogImpl;
                _stack.Push(target);
            }
            catch(Exception ex)
            {
                Logger.Warn(ex.Message);
            }
            finally
            {
                //
                //
                GUI.RunOnUIThread(() =>
                {
                    //
                    // Show
                    var v = ViewService.Instance.GetView(target);
                
                    //
                    //
                    v.Focus();
                
                    //
                    // raise dialog
                    IsOpened = true;
                    Dialog   = v;
                });
            }
        }

        public void CloseDialog(DialogBase target) => CloseDialogImpl(target);

        private void CloseDialogImpl(DialogBase dc)
        {
            if (_stack.Count == 0)
            {
                //
                //
                GUI.RunOnUIThread(() =>
                {
                    //
                    // raise dialog
                    IsOpened = false;
                    Dialog = null;
                });
                return;
            }

            var current = _stack.Pop();

            if (!ReferenceEquals(current, dc))
            {
                Logger.Warn("对话框VM实例不等价");
                return;
            }

            if (_stack.Count == 0)
            {
                //
                //
                GUI.RunOnUIThread(() =>
                {
                    //
                    // raise dialog
                    IsOpened = false;
                    Dialog = null;
                });
                return;
            }
            
            current = _stack.Peek();
            current.Resume();
                
            //
            //
            GUI.RunOnUIThread(() =>
            {
                //
                // Show
                var v = ViewService.Instance.GetView(current);
                
                //
                //
                v.Focus();
                
                //
                // raise dialog
                IsOpened = true;
                Dialog   = v;
            });
        }

        public ILogger Logger => _lazyLogger.Value;
    }
}