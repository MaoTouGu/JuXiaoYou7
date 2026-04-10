using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Xaml.Interactivity;

namespace KinonekoSoftware.UI
{
    public sealed class DataTemplateBehavior : Behavior<TemplatedControl>
    {
        private static readonly Dictionary<string, DataTemplates> _shared;

        static DataTemplateBehavior()
        {
            _shared = new Dictionary<string, DataTemplates>();
        }

        protected override void OnAttached()
        {
            // avares://KinonekoSoftware.JuXiaoYou.Views.OnAva/Contents/Styles/PropertyStyles.axaml
            if (AssociatedObject is null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Resource))
            {
                return;
            }

            DataTemplates templates;

            if (!_shared.TryGetValue(Resource, out templates))
            {
                templates = AvaloniaXamlLoader.Load(new Uri(Resource)) as DataTemplates;
                _shared.TryAdd(Resource, templates);
            }

            if (templates is null)
            {
                return;
            }

            foreach (var template in templates)
            {
                AssociatedObject.DataTemplates.Add(template);
            }

            base.OnAttached();
        }

        public string Resource { get; set; }
    }
}