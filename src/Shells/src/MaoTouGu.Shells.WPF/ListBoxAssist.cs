// ----------------------------------------------------------
//            文件：ListBoxAssist.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月24日 15:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Collections.Specialized;
using System.Reflection;
using ListBox = System.Windows.Controls.ListBox;

namespace MaoTouGu.Shells
{
    public static class ListBoxAssist
    {
        //
        // ListBoxItem.Tag 设置到任意VM中的属性。


        sealed class LetTagPropertyAssignBehavior : Behavior<ListBox>
        {
            protected override void OnAttached()
            {
                AssociatedObject.SelectionChanged += OnSelectionChanged;
                base.OnAttached();
            }


            protected override void OnDetaching()
            {
                AssociatedObject.SelectionChanged -= OnSelectionChanged;
                base.OnDetaching();
            }

            private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (sender is not ListBox listBox)
                {
                    return;
                }

                if (string.IsNullOrEmpty(PropertyName))
                {
                    return;
                }

                //
                //
                var container   = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem);
                var listBoxItem = (ListBoxItem)container;

                //
                //
                var tag = listBoxItem.Tag;
                var dc  = listBox.DataContext;

                if (dc is null)
                {
                    return;
                }

                if (dc is IPropertyRecipient ipr)
                {
                    ipr.SetValue(PropertyName, tag);
                }
                else
                {
                    _property ??= dc.GetType()
                                    .GetProperty(PropertyName, BindingFlags.Public |
                                                               BindingFlags.Instance);

                    if (_property is null)
                    {
                        return;
                    }

                    _property.SetValue(dc, tag);
                }
            }

            private PropertyInfo _property;

            public string PropertyName { get; init; }
        }

        public static readonly DependencyProperty PostTagToProperty =
            DependencyProperty.RegisterAttached("PostTagTo", 
                                                typeof(string), 
                                                typeof(ListBoxAssist), 
                                                new PropertyMetadata(default(string), OnPostTagToChanged));

        private static void OnPostTagToChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ListBox listBox)
            {
                return;
            }

            var oldVal = e.OldValue as string;
            var newVal = e.NewValue as string;

            var collection = Interaction.GetBehaviors(listBox);
            if (!string.IsNullOrEmpty(oldVal))
            {
                collection.Remove<Behavior, LetTagPropertyAssignBehavior>();
            }
            
            if (!string.IsNullOrEmpty(newVal))
            {
                collection.Add(new LetTagPropertyAssignBehavior
                {
                    PropertyName = newVal,
                });
            }
        }

        public static void SetPostTagTo(DependencyObject element, string value)
        {
            element.SetValue(PostTagToProperty, value);
        }

        public static string GetPostTagTo(DependencyObject element)
        {
            return (string)element.GetValue(PostTagToProperty);
        }
    }
}