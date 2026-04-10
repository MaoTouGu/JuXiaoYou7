// ----------------------------------------------------------
//            文件：SpriteWindow.xaml.cs
//            作者：Luoyisi&lt;acorisbk@qq.com&gt;
//            创建时间：2026年03月13日 11:48
//            版权所有：MaoTouGu Studio &amp; Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using MaoTouGu.Shells.Win32;

namespace MaoTouGu.Teams
{
    public partial class SpriteWindow
    {
        private bool _ForceExits;




        public SpriteWindow()
        {
            InitializeComponent();

            Title = "橘小柚";

            Loaded              += OnLoaded;
            MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AltTabHidden.HideAltTab(this);
        }

        //-------------------------------------------------------------
        //
        //          AddMany
        //
        //-------------------------------------------------------------
        static void ShowMainWindow()
        {
            // Application.Current
            //            .Windows
            //            .OfType<MainWindow>()
            //            .ForEach(x => x.LeaveMinimumState());
        }
        //-------------------------------------------------------------
        //
        //          AddMany
        //
        //-------------------------------------------------------------
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                TrayIcon.Visibility = Visibility.Visible;
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                ShowMainWindow();

                _ForceExits = false;
                Close();
                return;
            }

            DragMove();
        }


        private void Menu_ShowWindow(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void Menu_ShowMainWindow(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void Menu_HideWindow(object sender, RoutedEventArgs e)
        {
            _ForceExits = false;
            Close();
        }

        private void Menu_CloseWindow(object sender, RoutedEventArgs e)
        {
            _ForceExits = true;
            Close();
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !_ForceExits;
        }
    }
}