using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using MaoTouGu.Shells.Controls;
using MaoTouGu.Shells.Core;
using MaoTouGu.Shells.Runer.ViewModels;

namespace MaoTouGu.Shells.Runer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ContentHost.ViewModel = new GuideTestViewModel();
        }
    }
}