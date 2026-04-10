using System.Diagnostics;
using System.Windows.Forms;
using MaoTouGu.Shells.Languages;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace MaoTouGu.Shells.Interops
{
    public static class Interop
    {
        public static Result<string> OpenFolderBrowserAsync()
        {
            var opendlg = new FolderBrowserDialog();

            if (opendlg.ShowDialog() != DialogResult.OK)
            {
                return Result<string>.Failure;
            }
            
            return Result<string>.Success(opendlg.SelectedPath);
        }
        
        public static Result<string> SaveFileAsync(string filter, string ext, string fileName = null)
        {
            var opendlg = new SaveFileDialog
            {
                Filter       = filter,
                DefaultExt   = ext,
                FileName     = fileName,
                AddExtension = true,
            };
            
            if (opendlg.ShowDialog() != true)
            {
                return Result<string>.Failure;
            }

            try
            {
                return Result<string>.Success(opendlg.FileName);
            }
            catch(Exception ex)
            {
                return Result<string>.Failed(ex.Message);
            }
        }
        public static Result<string> OpenFileAsync(string filter)
        {
            var opendlg = new OpenFileDialog
            {
                Filter = filter,
            };
            
            if (opendlg.ShowDialog() != true)
            {
                return Result<string>.Failure;
            }
            
            return Result<string>.Success(opendlg.FileName);
        }
        
        public static Result<IEnumerable<string>> OpenFilesAsync(string filter)
        {
            var opendlg = new OpenFileDialog
            {
                Multiselect = true,
                Filter      = filter,
            };
            
            if (opendlg.ShowDialog() != true)
            {
                return Result<IEnumerable<string>>.Failure;
            }
            
            return Result<IEnumerable<string>>.Success(opendlg.FileNames);
        }
        
        public static void OpenLink(string uri, bool useKey = false)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return;
            }

            if (useKey)
            {
                uri = I18N.GetText(uri);
            }

            if (string.IsNullOrEmpty(uri))
            {
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName        = "explorer.exe",
                Arguments       = uri,
            });
        }
    }
}