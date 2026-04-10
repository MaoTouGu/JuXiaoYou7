// ----------------------------------------------------------
//            文件：CollectionTargetViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 23:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Reflection;
using MaoTouGu.Studio;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class CollectionTargetViewModel : SystemPage, IHostedWindowNavigation
    {
        private string _databaseName;
        private string _collectionName;

        public CollectionTargetViewModel()
        {
            DatabaseNamePreset   = new ViewList<string>(GetConstValues(typeof(EngineNames)));
            CollectionNamePreset = new ViewList<string>(GetConstValues(typeof(CollectionNames)));
            StartDebug           = new DelegateCommand(DoStartDebug, Correct);
        }

        bool Correct() => Correct(_databaseName)                        && Correct(_collectionName);
        static bool Correct(string name) => !string.IsNullOrEmpty(name) && name.All(char.IsLetterOrDigit);

        static IEnumerable<string> GetConstValues(Type t)
        {
            return t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .Select(x => x.GetValue(null))
                    .OfType<string>();
        }

        private async void DoStartDebug()
        {
            await Navigate(new DebuggerViewModel(_databaseName, _collectionName));
        }

        public ViewList<string> DatabaseNamePreset   { get; }
        public ViewList<string> CollectionNamePreset { get; }
        
        public ICommandEX StartDebug { get; }

        public string CollectionName
        {
            get => _collectionName;
            set
            {
                SetValue(ref _collectionName, value);
                StartDebug.RaiseUpdate();
            }
        }
        public string DatabaseName
        {
            get => _databaseName;
            set
            {
                SetValue(ref _databaseName, value);
                StartDebug.RaiseUpdate();
            }
        }
    }
}