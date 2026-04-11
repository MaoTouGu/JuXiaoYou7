// ----------------------------------------------------------
//            文件：DebuggerViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using KinonekoSoftware.UI.Controls;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class DebuggerViewModel : SystemPage
    {
        private string _setting;

        public DebuggerViewModel()
        {
            var v = new WithRarityGravatarVisualizer { RarityFrom = "稀有度" };

            Moniker          = Moniker.Create(string.Empty, new User { Id = ID.Get(), DisplayName = "Test" });
            Moniker.Name     = "测试";
            Moniker.Gravatar = @"C:\Users\Luoyisi\Pictures\Character.png";

            Options = v;
            Add     = new DelegateCommand(DoAdd);
            Update  = new DelegateCommand(DoEdit);
        }


        private async void DoAdd()
        {
            var r = await this.SingleLine("Add", string.Empty);

            if (!r.IsFinished)
            {
                return;
            }

            if (Moniker.Settings.TryAdd(r.Value, string.Empty))
            {
                Setting = r.Value;
            }
        }

        private async void DoEdit()
        {
            if (string.IsNullOrEmpty(Setting))
            {
                return;
            }

            var r = await this.SingleLine("Add", string.Empty);

            if (!r.IsFinished)
            {
                return;
            }

            if (!Moniker.Settings.TryAdd(Setting, r.Value))
            {
                Moniker.Settings[Setting] = r.Value;

                Setting = r.Value;
            }
        }

        private IVisualizerOptions _options;

        public IVisualizerOptions Options
        {
            get => _options;
            set => SetValue(ref _options, value);
        }

        public string Setting
        {
            get => _setting;
            set
            {
                SetValue(ref _setting, value);
                Update.RaiseUpdate();
            }
        }

        public ICommandEX Add    { get; }
        public ICommandEX Update { get; }

        public Moniker Moniker { get; }
    }
}