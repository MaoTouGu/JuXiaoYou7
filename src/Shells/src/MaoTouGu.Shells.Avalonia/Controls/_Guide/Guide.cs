using MaoTouGu.Shells.Controls;
using MaoTouGu.Shells.Services;
using Microsoft.Xaml.Behaviors;

namespace MaoTouGu.Shells.Controls
{
    public static class Guide
    {
        sealed class GuideBehavior : Behavior<FrameworkElement>
        {
            private IGuideElementRecipient _recipient;

            protected override void OnAttached()
            {
                var parent = Xaml.FindVisualParent(AssociatedObject, x => x is IGuideElementRecipient);

                if (parent is IGuideElementRecipient recipient)
                {
                    _recipient = recipient;
                    _recipient.Accept(AssociatedObject);
                }
            }

            protected override void OnDetaching()
            {
                if (_recipient is null)
                {
                    var parent = Xaml.FindVisualParent(AssociatedObject, x => x is IGuideElementRecipient);

                    if (parent is IGuideElementRecipient recipient)
                    {
                        _recipient = recipient;
                    }
                }


                _recipient?.Clear();
            }

        }

        public static readonly DependencyProperty HintProperty;
        public static readonly DependencyProperty IndexProperty;
        public static readonly DependencyProperty AllowMultipleProperty;
        public static readonly DependencyProperty PlacementProperty;

        static Guide()
        {
            HintProperty          = DependencyProperty.RegisterAttached("ShadowHint", typeof(string), typeof(Guide), new PropertyMetadata(default(string)));
            IndexProperty         = DependencyProperty.RegisterAttached("Index", typeof(int), typeof(Guide), new PropertyMetadata(default(int)));
            PlacementProperty     = DependencyProperty.RegisterAttached("Placement", typeof(Placement), typeof(Guide), new PropertyMetadata(default(Placement)));
            AllowMultipleProperty = DependencyProperty.RegisterAttached("ShadowAllowMultiple", typeof(bool), typeof(Guide), new PropertyMetadata(Boxing.False));
        }


        private static void OnGuideElementInitialized(object sender, EventArgs e)
        {
            if (sender is not FrameworkElement fe)
            {
                return;
            }

            var behaviors = Interaction.GetBehaviors(fe);
            fe.Loaded -= OnGuideElementInitialized;

            behaviors.Add(new GuideBehavior());
        }

        public static void SetHint(DependencyObject element, string value)
        {
            //
            // 向上查找IGuideElementRecipient
            if (element is FrameworkElement fe)
            {
                fe.Loaded += OnGuideElementInitialized;
            }

            element.SetValue(HintProperty, value);
        }

        public static string GetHint(DependencyObject element)
        {
            return (string)element.GetValue(HintProperty);
        }

        public static void SetIndex(DependencyObject element, int value)
        {
            element.SetValue(IndexProperty, value);
        }

        public static int GetIndex(DependencyObject element)
        {
            return (int)element.GetValue(IndexProperty);
        }

        public static void SetAllowMultiple(DependencyObject element, bool value)
        {
            element.SetValue(AllowMultipleProperty, Boxing.Box(value));
        }

        public static bool GetAllowMultiple(DependencyObject element)
        {
            return (bool)element.GetValue(AllowMultipleProperty);
        }
        
        public static void SetPlacement(DependencyObject element, Placement value)
        {
            element.SetValue(PlacementProperty, value);
        }

        public static Placement GetPlacement(DependencyObject element)
        {
            return (Placement)element.GetValue(PlacementProperty);
        }
    }
}