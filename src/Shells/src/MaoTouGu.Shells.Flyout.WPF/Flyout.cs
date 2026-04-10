
using MaoTouGu.Shells.Controls;
using MaoTouGu.Shells.Services;
using Microsoft.Xaml.Behaviors;

namespace MaoTouGu.Shells.Controls
{
    public static partial class Flyout
    {
        sealed class FlyoutBehavior : Behavior<FrameworkElement>
        {
            private IFlyoutElementRecipient _recipient;

            /// <summary>
            /// 查找指定元素的视觉父级。
            /// </summary>
            /// <param name="dp">指定要查找视觉父级的元素，要求不为空。</param>
            /// <param name="expression">判断此元素是否复合条件。</param>
            /// <param name="maxDepth">最高的深度。</param>
            /// <returns>返回视觉父级，可能为空。</returns>
            static DependencyObject FindVisualParent(DependencyObject dp, Predicate<DependencyObject> expression, int maxDepth = 32)
            {
                var parent = VisualTreeHelper.GetParent(dp);
                var depth  = 0;

                if (expression is null)
                {
                    return null;
                }
            
                while (parent is not null && depth < maxDepth)
                {
                    if (expression(parent))
                    {
                        break;
                    }
                
                    parent = VisualTreeHelper.GetParent(parent);
                    depth++;
                }


                return parent;
            }
            
            protected override void OnAttached()
            {
                var parent = FindVisualParent(AssociatedObject, x => x is IFlyoutElementRecipient);

                if (parent is IFlyoutElementRecipient recipient)
                {
                    _recipient = recipient;
                    _recipient.Accept(AssociatedObject);
                }
            }

            protected override void OnDetaching()
            {
                if (_recipient is null)
                {
                    var parent = FindVisualParent(AssociatedObject, x => x is IFlyoutElementRecipient);

                    if (parent is IFlyoutElementRecipient recipient)
                    {
                        _recipient = recipient;
                    }
                }


                _recipient?.Clear();
            }

        }



        private static void OnFlyoutElementInitialized(object sender, EventArgs e)
        {
            if (sender is not FrameworkElement fe)
            {
                return;
            }

            var behaviors = Interaction.GetBehaviors(fe);
            fe.Loaded -= OnFlyoutElementInitialized;

            behaviors.Add(new FlyoutBehavior());
        }

    }
}