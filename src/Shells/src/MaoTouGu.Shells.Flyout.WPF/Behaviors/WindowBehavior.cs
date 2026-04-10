using System.Windows.Documents;
using MaoTouGu.Shells.Adorners;
using MaoTouGu.Shells.Controls.Adorners;

namespace MaoTouGu.Shells.Behaviors
{
    public class WindowBehavior : Behavior<Window>
    {

        private readonly List<FlyoutObject> _flyoutObjects = new List<FlyoutObject>();

        private ContentPresenter PART_Content;

        private AdornerLayer             _layer;
        private FlyoutObject             _current;
        private FlyoutAdornerCanvas      _flyoutAdorner;
        private SurroundingCanvasAdorner _surrounding;
        private ControlWrapperAdorner    _wrapperAdorner;

        /// <summary>
        /// 关闭当前的引导。
        /// </summary>
        internal void CloseFlyoutInternal()
        {
            //
            // 关闭当前。
            CloseFlyoutImpl();

            if (_current is null)
            {
                return;
            }

            if (_flyoutObjects.Remove(_current))
            {
                _current = null;
            }

            //
            // 呈现下一个
            if (_flyoutObjects.Count > 0 && _flyoutObjects[0] is not null)
            {
                var go = _flyoutObjects[0];
                Flyout(go.View as FrameworkElement, go);
            }
        }

        private void CloseFlyoutImpl()
        {
            if (_layer is null)
            {
                return;
            }

            if (_flyoutAdorner is not null)
            {
                _layer.Remove(_flyoutAdorner);
                _flyoutAdorner = null;
            }

            if (_wrapperAdorner is not null)
            {
                _layer.Remove(_wrapperAdorner);
                _wrapperAdorner = null;
            }

            if (_surrounding is not null)
            {
                _layer.Remove(_surrounding);
                _surrounding = null;
            }
        }

        protected override void OnAttached()
        {

            PART_Content = Xaml.FindVisualChild<ContentPresenter>(AssociatedObject, x => x.Name == nameof(PART_Content));
        }

        /// <summary>
        /// 关闭当前所有引导，并清空所有选项。
        /// </summary>
        public void CloseFlyout()
        {
            //
            // 清空所有。
            _flyoutObjects.Clear();

            CloseFlyoutImpl();
        }

        public void Flyout(List<FlyoutObject> orderedList)
        {
            //
            // 关闭当前的引导。
            CloseFlyoutImpl();

            //
            //
            _flyoutObjects.AddMany(orderedList, true);

            if (orderedList.Count <= 0)
            {
                return;
            }

            var go = orderedList[0];

            if (go is null)
            {
                return;
            }

            if (!go.ShowNextStep && orderedList.IndexOf(go) < orderedList.Count - 2)
            {
                go.ShowNextStep = true;
            }

            Flyout(go.View as FrameworkElement, go);
        }

        public void Flyout(FrameworkElement needDecorated, FlyoutObject dataContext)
        {
            if (PART_Content is null)
            {
                return;
            }

            _layer   = AdornerLayer.GetAdornerLayer(PART_Content);
            _current = dataContext;

            if (_layer is null)
            {
                return;
            }

            //
            //
            dataContext.Window = AssociatedObject;

            //
            //
            _flyoutAdorner = new FlyoutAdornerCanvas(PART_Content, dataContext, needDecorated, new FlyoutIndicator());

            //
            // 添加到
            _layer.Add(_flyoutAdorner);
        }


        public void Flyout(FrameworkElement needDecorated, FlyoutObject dataContext, FrameworkElement wrapper)
        {
            if (PART_Content is null)
            {
                return;
            }

            _layer   = AdornerLayer.GetAdornerLayer(PART_Content);
            _current = dataContext;

            if (_layer is null)
            {
                return;
            }

            //
            //
            dataContext.Window = AssociatedObject;

            //
            //
            _flyoutAdorner = new FlyoutAdornerCanvas(PART_Content, dataContext, needDecorated, wrapper);

            //
            // 添加到
            _layer.Add(_flyoutAdorner);
        }

        public void Surrounding(FrameworkElement needDecorated, Surrounding dataContext)
        {
            if (PART_Content is null)
            {
                return;
            }

            _layer = AdornerLayer.GetAdornerLayer(PART_Content);

            if (_layer is null)
            {
                return;
            }
            
            
            var _targetRect = Xaml.GetPosition(PART_Content, needDecorated);
            _surrounding = new SurroundingCanvasAdorner(PART_Content, dataContext, needDecorated);
            
            if (!new Rect(PART_Content.RenderSize).Contains(_targetRect))
            {
                var scrollViewer = Xaml.FindVisualParent<ScrollViewer>(needDecorated);
                var rect         = Xaml.GetPosition(scrollViewer, needDecorated);

                scrollViewer.ScrollChanged += OnScrollChanged;

                if (rect.Y > 0)
                {
                    scrollViewer.ScrollToVerticalOffset(rect.Y);
                }
                else
                {
                    scrollViewer.ScrollToVerticalOffset(rect.Y + scrollViewer.ViewportHeight);
                }
                Debug.WriteLine($"Y = {rect.Y}");

            }
            else
            {
                //
                //
                _layer.Add(_surrounding);
            }

        }
        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ((ScrollViewer)sender).ScrollChanged -= OnScrollChanged;
            
            _layer.Add(_surrounding);
        }
        
        #region FlyoutObject

        public void ClearFlyoutObject()
        {
            if (_wrapperAdorner is not null)
            {
                _layer.Remove(_wrapperAdorner);
                _wrapperAdorner = null;
            }
        }

        public void FlyoutObject(FrameworkElement target)
        {
            if (PART_Content is null)
            {
                return;
            }

            _layer = AdornerLayer.GetAdornerLayer(PART_Content);

            if (_layer is null)
            {
                return;
            }

            if (_wrapperAdorner is not null)
            {
                _layer.Remove(_wrapperAdorner);
                _wrapperAdorner = null;
            }

            //
            //
            _wrapperAdorner = new ControlWrapperAdorner(PART_Content, target);

            //
            // 添加到
            _layer.Add(_wrapperAdorner);
        }

        #endregion

        public static void Flyout(Window window, List<FlyoutObject> orderedList)
        {
            if (window is null)
            {
                return;
            }
            var collection = Interaction.GetBehaviors(window);

            if (collection.Count == 0 || !collection.OfType<WindowBehavior>().Any())
            {
                collection.Add(new WindowBehavior());
            }

            collection.OfType<WindowBehavior>()
                      .ForEach(x => x.Flyout(orderedList));
        }

        public static void Flyout(Window window, FrameworkElement needDecorated, FlyoutObject dataContext)
        {
            if (window is null)
            {
                return;
            }
            var collection = Interaction.GetBehaviors(window);

            if (collection.Count == 0 || !collection.OfType<WindowBehavior>().Any())
            {
                collection.Add(new WindowBehavior());
            }

            collection.OfType<WindowBehavior>()
                      .ForEach(x => x.Flyout(needDecorated, dataContext));
        }

        public static void Surrounding(Window window, FrameworkElement needDecorated, Surrounding dataContext)
        {
            if (window is null)
            {
                return;
            }
            var collection = Interaction.GetBehaviors(window);

            if (collection.Count == 0 || !collection.OfType<WindowBehavior>().Any())
            {
                collection.Add(new WindowBehavior());
            }

            collection.OfType<WindowBehavior>()
                      .ForEach(x => x.Surrounding(needDecorated, dataContext));
        }

        public static void Flyout(Window window, FrameworkElement needDecorated, FlyoutObject dataContext, FrameworkElement wrapper)
        {
            if (window is null)
            {
                return;
            }
            var collection = Interaction.GetBehaviors(window);

            if (collection.Count == 0 || !collection.OfType<WindowBehavior>().Any())
            {
                collection.Add(new WindowBehavior());
            }

            collection.OfType<WindowBehavior>()
                      .ForEach(x => x.Flyout(needDecorated, dataContext, wrapper));
        }

        public static void CloseFlyout(Window window)
        {
            if (window is null)
            {
                return;
            }
            Interaction.GetBehaviors(window)
                       .OfType<WindowBehavior>()
                       .ForEach(x => x.CloseFlyout());
        }

        public static void ClearFlyoutObject(Window window)
        {
            if(window is null)
            {
                return;
            }

            Interaction.GetBehaviors(window)
                       .OfType<WindowBehavior>()
                       .ForEach(x => x.ClearFlyoutObject());
        }

        public static void FlyoutObject(Window window, FrameworkElement target)
        {
            if (window is null)
            {
                return;
            }
            var collection = Interaction.GetBehaviors(window);

            if (collection.Count == 0 || !collection.OfType<WindowBehavior>().Any())
            {
                collection.Add(new WindowBehavior());
            }

            collection.OfType<WindowBehavior>()
                      .ForEach(x => x.FlyoutObject(target));
        }
    }
}