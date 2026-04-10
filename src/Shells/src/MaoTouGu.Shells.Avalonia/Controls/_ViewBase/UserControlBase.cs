
using Avalonia.Interactivity;

namespace MaoTouGu.Shells.Controls
{
    public abstract class UserControlBase : UserControl, IGuideElementRecipient, IGuideService
    {
        protected readonly List<FrameworkElement> GuideElements;

        private bool _init;
        
        protected UserControlBase()
        {
            GuideElements = new List<FrameworkElement>(8);

            Loaded   += OnLoaded;
            Unloaded += OnUnloadedImpl;
        }
        
        private void OnUnloadedImpl(object sender, RoutedEventArgs e)
        {
            
        }
        

        #region IGuideElementRecipient

        void IGuideElementRecipient.Clear()
        {
            GuideElements.Clear();
        }

        void IGuideElementRecipient.Accept(FrameworkElement element)
        {
            GuideElements.Add(element);
        }

        #endregion
        
        
        #region Loaded

        internal void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_init)
            {
                return;
            }

            _init = true;
            
            //
            //
            var vm = ViewModel<ViewModelBase>();

            //
            //
            if (vm is not null)
            {
                if (vm.IsInitialized)
                {
                    vm.Resume();
                }
                else
                {
                    vm.Start();
                }
            }

            OnLoadedInternal();

            //
            //
            OnLoaded();
        }

        internal virtual void OnLoadedInternal()
        {

        }

        protected virtual void OnLoaded()
        {

        }

        #endregion

        #region Guide System

        

        public void ShowGuide()
        {
            //
            // 需要将所有FrameworkElement按照步骤进行组装。

            var window     = Xaml.FindVisualParent<MTGWindow>(this);
            var collection = new List<GuideObject>(8);

            if (window is null)
            {
                return;
            }

            foreach (var element in GuideElements)
            {
                BuildGuideObject(element, collection);
            }
            
            collection.Sort();
            window.ShowGuide(collection);
        }

        private void BuildGuideObject(FrameworkElement fe, List<GuideObject> collection)
        {
            var hint          = Guide.GetHint(fe);
            var allowMultiple = Guide.GetAllowMultiple(fe);
            var index         = Guide.GetIndex(fe);
            var placement     = Guide.GetPlacement(fe);

            if (string.IsNullOrEmpty(hint))
            {
                return;
            }


            if (allowMultiple)
            {
                foreach (var wizard in BuildGuideWizards(hint))
                {
                    wizard.Index     = index++;
                    wizard.View      = fe;
                    wizard.Placement = placement;
                    
                    //
                    //
                    if (string.IsNullOrEmpty(wizard.Color))
                    {
                        wizard.Color = "#FF5868C8";
                    }
                    
                    //
                    //
                    collection.Add(wizard);
                }
            }
            else
            {
                
                var obj = BuildGuideWizard(hint);
                
                if (string.IsNullOrEmpty(obj.Color))
                {
                    obj.Color = "#FF5868C8";
                }

                obj.View      = fe;
                obj.Index     = index;
                obj.Placement = placement;
                    
                //
                //
                collection.Add(obj);
            }


        }

        protected virtual GuideObject BuildGuideWizard(string hint) => null;
        
        protected virtual IEnumerable<GuideObject> BuildGuideWizards(string hint) => Array.Empty<GuideObject>();
        
        #endregion

        #region IBusyStateManager

        

        #endregion
        
        protected T ViewModel<T>() where T : ViewModelBase
        {
            return DataContext as T;
        }
    }
}