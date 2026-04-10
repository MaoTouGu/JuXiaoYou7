namespace MaoTouGu.Shells.Controls
{
    partial class Flyout
    {
        #region Hint

        public static void SetHint(DependencyObject element, string value)
        {
            //
            // 向上查找IFlyoutElementRecipient
            if (element is FrameworkElement fe)
            {
                fe.Loaded += OnFlyoutElementInitialized;
            }

            element.SetValue(HintProperty, value);
        }

        public static string GetHint(DependencyObject element)
        {
            return (string)element.GetValue(HintProperty);
        }
        

        #endregion

        #region Index

        public static void SetIndex(DependencyObject element, int value)
        {
            element.SetValue(IndexProperty, value);
        }

        public static int GetIndex(DependencyObject element)
        {
            return (int)element.GetValue(IndexProperty);
        }
        

        #endregion

        #region AllowMultiple

        

        public static void SetAllowMultiple(DependencyObject element, bool value)
        {
            element.SetValue(AllowMultipleProperty, Boxing.Box(value));
        }

        public static bool GetAllowMultiple(DependencyObject element)
        {
            return (bool)element.GetValue(AllowMultipleProperty);
        }

        #endregion

        #region Placement

        

        public static void SetPlacement(DependencyObject element, Placement value)
        {
            element.SetValue(PlacementProperty, value);
        }

        public static Placement GetPlacement(DependencyObject element)
        {
            return (Placement)element.GetValue(PlacementProperty);
        }
        #endregion
        
    }
}