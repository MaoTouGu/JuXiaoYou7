// ----------------------------------------------------------
//            文件：ContextMenuAssist.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月11日 23:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using Button = System.Windows.Controls.Button;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace MaoTouGu.Shells
{
    public static class ContextMenuAssist
    {
        public static readonly DependencyProperty UseLeftButtonProperty =
            DependencyProperty.RegisterAttached(
                                                "UseLeftButton",
                                                typeof(bool),
                                                typeof(ContextMenuAssist),
                                                new PropertyMetadata(Boxing.False, OnUseLeftButtonChanged));

        public static readonly DependencyProperty UseClickProperty =
            DependencyProperty.RegisterAttached(
                                                "UseClick",
                                                typeof(bool),
                                                typeof(ContextMenuAssist),
                                                new PropertyMetadata(Boxing.False, OnUseClickChanged));

        public static readonly DependencyProperty ParentObjectProperty =
            DependencyProperty.RegisterAttached(
                                                "ParentObject", 
                                                typeof(object),
                                                typeof(ContextMenuAssist),
                                                new PropertyMetadata(default(object)));

        public static FrameworkElement GetParent(FrameworkElement fe)
        {
            return GetParentObject(Xaml.FindVisualParent<ContextMenu>(fe)) as FrameworkElement;
        }

        public static void SetParentObject(DependencyObject element, object value)
        {
            element.SetValue(ParentObjectProperty, value);
        }

        public static object GetParentObject(DependencyObject element)
        {
            return (object)element.GetValue(ParentObjectProperty);
        }

        private static void OnUseLeftButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool b && b)
            {
                Interaction.GetBehaviors(d).Add(new UseLeftButtonBehavior());
            }
            else
            {
                Interaction.GetBehaviors(d).Remove<Behavior, UseLeftButtonBehavior>();
            }
        }

        private static void OnUseClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool b && b)
            {
                Interaction.GetBehaviors(d).Add(new UseClickBehavior());
            }
            else
            {
                Interaction.GetBehaviors(d).Remove<Behavior, UseClickBehavior>();
            }
        }

        sealed class UseClickBehavior : Behavior<Button>
        {
            protected override void OnAttached()
            {
                AssociatedObject.Click += OnClick;
                base.OnAttached();
            }

            private void OnClick(object sender, RoutedEventArgs e)
            {
                if (AssociatedObject.ContextMenu is not null)
                {
                    AssociatedObject.ContextMenu.PlacementTarget = AssociatedObject;
                    AssociatedObject.ContextMenu.IsOpen          = true;
                    SetParentObject(AssociatedObject.ContextMenu, AssociatedObject);
                }
            }

            protected override void OnDetaching()
            {
                AssociatedObject.Click -= OnClick;
                base.OnDetaching();
            }
        }

        sealed class UseLeftButtonBehavior : Behavior<FrameworkElement>
        {
            protected override void OnAttached()
            {
                AssociatedObject.MouseDown += OnClick;
                base.OnAttached();
            }

            private void OnClick(object sender, MouseButtonEventArgs e)
            {
                if (AssociatedObject.ContextMenu is not null)
                {
                    AssociatedObject.ContextMenu.PlacementTarget = AssociatedObject;
                    AssociatedObject.ContextMenu.IsOpen          = true;
                    SetParentObject(AssociatedObject.ContextMenu, AssociatedObject);
                }
            }

            protected override void OnDetaching()
            {
                AssociatedObject.MouseDown -= OnClick;
                base.OnDetaching();
            }
        }

        public static void SetUseClick(Button element, bool value)
        {
            element.SetValue(UseClickProperty, value);
        }

        public static bool GetUseClick(Button element)
        {
            return (bool)element.GetValue(UseClickProperty);
        }

        public static void SetUseLeftButton(DependencyObject element, bool value)
        {
            element.SetValue(UseLeftButtonProperty, value);
        }

        public static bool GetUseLeftButton(DependencyObject element)
        {
            return (bool)element.GetValue(UseLeftButtonProperty);
        }
    }
}