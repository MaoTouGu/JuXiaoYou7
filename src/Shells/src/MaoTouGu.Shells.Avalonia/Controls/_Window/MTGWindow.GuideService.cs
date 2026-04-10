using System.Windows.Documents;
using MaoTouGu.Foundation.Collections;

namespace MaoTouGu.Shells.Controls
{
    partial class MTGWindow : IGuideElementRecipient
    {

        private readonly List<FrameworkElement> _guideElements;
        private readonly List<GuideObject>      _guideWizards;

        private AdornerLayer       _layer;
        private GuideObject        _current;
        private GuideAdornerCanvas _guideAdorner;

        /// <summary>
        /// 关闭当前的引导。
        /// </summary>
        internal void CloseGuideInternal()
        {
            //
            // 关闭当前。
            CloseGuideImpl();

            if (_current is null)
            {
                return;
            }

            if (_guideWizards.Remove(_current))
            {
                _current = null;
            }

            //
            // 呈现下一个
            if (_guideWizards.Count > 0 && _guideWizards[0] is not null)
            {
                var go = _guideWizards[0];
                ShowGuide(go.View as FrameworkElement, go, _guideWizards.Count > 1);
            }
        }

        private void CloseGuideImpl()
        {
            if (_layer is null)
            {
                return;
            }

            if (_guideAdorner is not null)
            {
                _layer.Remove(_guideAdorner);
            }
        }

        /// <summary>
        /// 关闭当前所有引导，并清空所有选项。
        /// </summary>
        public void CloseGuide()
        {
            //
            // 清空所有。
            _guideWizards.Clear();
            
            CloseGuideImpl();
        }

        public void ShowGuide(List<GuideObject> orderedList)
        {
            //
            // 关闭当前的引导。
            CloseGuideImpl();
            
            //
            //
            _guideWizards.AddMany(orderedList, true);

            if (orderedList.Count <= 0)
            {
                return;
            }
            
            var go = orderedList[0];

            if (go is null)
            {
                return;
            }

            ShowGuide(go.View as FrameworkElement, go, orderedList.Count > 1);
        }
        
        public void ShowGuide(FrameworkElement needDecorated, GuideObject dataContext, bool isNextStepCollapsed = false)
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
            dataContext.Window = this;

            //
            //
            _guideAdorner = new GuideAdornerCanvas(PART_Content, dataContext, needDecorated);

            //
            // 添加到
            _layer.Add(_guideAdorner);
        }

        void IGuideElementRecipient.Clear()
        {
            _guideElements.Clear();
        }
        
        void IGuideElementRecipient.Accept(FrameworkElement element)
        {
            _guideElements.Add(element);
        }
    }
}