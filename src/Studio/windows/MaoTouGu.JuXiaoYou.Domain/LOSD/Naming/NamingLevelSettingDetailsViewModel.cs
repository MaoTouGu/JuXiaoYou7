using MaoTouGu.Shells.Inputs;
using MaoTouGu.Studio;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.LOSD.Naming
{
    public class NamingLevelSettingDetailsViewModel : JuXiaoYouPage
    {
        private bool    _isCreationMode;
        private Moniker _moniker;
        private string  _text;

        public NamingLevelSettingDetailsViewModel()
        {
            MonikerService = GetService<MonikerService>();
            IsCreationMode = true;

            Create = new CreateMonikerCommand(this);
        }

        protected override async void OnStart()
        {
            await MonikerService.Start();
        }

        public string Text
        {
            get => _text;
            set => SetValue(ref _text, value);
        }
        public Moniker Moniker
        {
            get => _moniker;
            set => SetValue(ref _moniker, value);
        }
        public bool IsCreationMode
        {
            get => _isCreationMode;
            set => SetValue(ref _isCreationMode, value);
        }


        public MonikerService MonikerService { get; }

        public ViewList<Moniker> Collection => MonikerService.Collection;


        public ICommandEX Create { get; }
        public ICommandEX Save   { get; }
    }
}