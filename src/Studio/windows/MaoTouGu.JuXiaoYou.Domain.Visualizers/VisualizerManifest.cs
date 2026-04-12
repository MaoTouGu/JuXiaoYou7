using MaoTouGu.JuXiaoYou.Visualizers.Blocks;
using MaoTouGu.JuXiaoYou.Visualizers.GravatarWide;

namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public class VisualizerManifest : PluginManifest
    {
        /*
         *






3D44E83D7EB445FB8D70E7A466840889
E5D139B163634E149C3D073B5510C5E3
343A75655BE14136A6D9E1073FFC9078
97E17BF4B1F040D3B30596EDE05BFEC2
FD05FE4FA7244FFFBD393EB4386F487C
E0267406B5814A51963E9F1663CFD8F5
EB412442AD3245BE9AB9476016A1A7B4
A5FEB560A0274E45B6675D5982BC36F5
27026A308C9F4F57BF55C319FE29C0A0
45572174839C4143B0A49E63D44C5857
53EC911FB4164F59B0224A2CBA36BC56
351A3AF9E883410DA8DEC4800FC92466
35117B3C1E6D4B7C87A9303CEB723BF7

         */
        public const string PlainText = "E198EF3727874F11AC31B35510052197";
        public const string Image     = "A7E89FEE3DFA4EC79ACBF018957417FB";

        public const string HorizontalDecorator = "1F6FF98F0203451BB40D19318F6216BD";
        public const string VerticalDecorator = "76CC5105BAEF42D6BBBA187F72946F20";

        public override void RegisterVisualManagers()
        {
        }

        public override void RegisterVisualizers()
        {
            // FeatureManager.AsVisualizer<StarInlineVisualizer>();
            // FeatureManager.AsVisualizer<StarInlineVisualizer>();
            // FeatureManager.AsVisualizer<CardDocumentVisualizer>();
            FeatureManager.AsVisualizer<ImageVisualizer>();
            FeatureManager.AsVisualizer<WithRarityGravatarVisualizer>();
            FeatureManager.AsVisualizer<RectangleVisualizer>();
        }

        public override void RegisterFeatures()
        {

        }
    }
}