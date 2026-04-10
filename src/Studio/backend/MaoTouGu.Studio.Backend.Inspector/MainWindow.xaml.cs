using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiteDB;
using MaoTouGu.Studio.Database;
using Microsoft.Win32;

namespace MaoTouGu.Studio.BackendInspector;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void Button_Open(object sender, RoutedEventArgs e)
    {
        var opendlg = new OpenFileDialog();

        if (opendlg.ShowDialog() != true)
        {
            return;
        }

        try
        {
            var buffer     = File.ReadAllBytes(opendlg.FileName);
            var collection = LiteCollectionExtensions.DeserializeCollection(buffer);

            var iterator = collection.Select(x => JsonSerializer.Serialize(x));
            var sb       = new StringBuilder();

            foreach (var json in iterator)
            {
                sb.Append(json);
                sb.Append(Environment.NewLine);
            }

            Text.Text = sb.ToString();

        }
        catch(Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}