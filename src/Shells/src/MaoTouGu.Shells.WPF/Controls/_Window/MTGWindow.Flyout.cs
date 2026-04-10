using System.Windows.Documents;
using MaoTouGu.Foundation.Collections;
using MaoTouGu.Shells.Adorners;

namespace MaoTouGu.Shells.Controls
{
    partial class MTGWindow
    {
        // private readonly List<FlyoutObject> _flyoutObjects;
        //
        // private AdornerLayer          _layer;
        // private FlyoutObject          _current;
        // private FlyoutAdornerCanvas   _flyoutAdorner;
        // private ControlWrapperAdorner _wrapperAdorner;
        //
        // /// <summary>
        // /// 关闭当前的引导。
        // /// </summary>
        // internal void CloseFlyoutInternal()
        // {
        //     //
        //     // 关闭当前。
        //     CloseFlyoutImpl();
        //
        //     if (_current is null)
        //     {
        //         return;
        //     }
        //
        //     if (_flyoutObjects.Remove(_current))
        //     {
        //         _current = null;
        //     }
        //
        //     //
        //     // 呈现下一个
        //     if (_flyoutObjects.Count > 0 && _flyoutObjects[0] is not null)
        //     {
        //         var go = _flyoutObjects[0];
        //         ShowFlyout(go.View as FrameworkElement, go);
        //     }
        // }
        //
        // private void CloseFlyoutImpl()
        // {
        //     if (_layer is null)
        //     {
        //         return;
        //     }
        //
        //     if (_flyoutAdorner is not null)
        //     {
        //         _layer.Remove(_flyoutAdorner);
        //         _flyoutAdorner = null;
        //     }
        //
        //     if (_wrapperAdorner is not null)
        //     {
        //         _layer.Remove(_wrapperAdorner);
        //         _wrapperAdorner = null;
        //     }
        // }
        //
        // /// <summary>
        // /// 关闭当前所有引导，并清空所有选项。
        // /// </summary>
        // public void CloseFlyout()
        // {
        //     //
        //     // 清空所有。
        //     _flyoutObjects.Clear();
        //
        //     CloseFlyoutImpl();
        // }
        //
        // public void ShowFlyout(List<FlyoutObject> orderedList)
        // {
        //     //
        //     // 关闭当前的引导。
        //     CloseFlyoutImpl();
        //
        //     //
        //     //
        //     _flyoutObjects.AddMany(orderedList, true);
        //
        //     if (orderedList.Count <= 0)
        //     {
        //         return;
        //     }
        //
        //     var go = orderedList[0];
        //
        //     if (go is null)
        //     {
        //         return;
        //     }
        //
        //     if (!go.ShowNextStep && orderedList.IndexOf(go) < orderedList.Count - 2)
        //     {
        //         go.ShowNextStep = true;
        //     }
        //
        //     ShowFlyout(go.View as FrameworkElement, go);
        // }
        //
        // public void ShowFlyout(FrameworkElement needDecorated, FlyoutObject dataContext)
        // {
        //     if (PART_Content is null)
        //     {
        //         return;
        //     }
        //
        //     _layer   = AdornerLayer.GetAdornerLayer(PART_Content);
        //     _current = dataContext;
        //
        //     if (_layer is null)
        //     {
        //         return;
        //     }
        //
        //     //
        //     //
        //     dataContext.Window = this;
        //
        //     //
        //     //
        //     _flyoutAdorner = new FlyoutAdornerCanvas(PART_Content, dataContext, needDecorated);
        //
        //     //
        //     // 添加到
        //     _layer.Add(_flyoutAdorner);
        // }
        //
        // public void CloseOverlay()
        // {
        //     if (_wrapperAdorner is not null)
        //     {
        //         _layer.Remove(_wrapperAdorner);
        //         _wrapperAdorner = null;
        //     }
        // }
        //
        // public void Overlay(FrameworkElement target)
        // {
        //     if (PART_Content is null)
        //     {
        //         return;
        //     }
        //
        //     _layer = AdornerLayer.GetAdornerLayer(PART_Content);
        //
        //     if (_layer is null)
        //     {
        //         return;
        //     }
        //
        //     if (_wrapperAdorner is not null)
        //     {
        //         _layer.Remove(_wrapperAdorner);
        //         _wrapperAdorner = null;
        //     }
        //
        //     //
        //     //
        //     _wrapperAdorner = new ControlWrapperAdorner(PART_Content, target);
        //
        //     //
        //     // 添加到
        //     _layer.Add(_wrapperAdorner);
        // }
    }
}