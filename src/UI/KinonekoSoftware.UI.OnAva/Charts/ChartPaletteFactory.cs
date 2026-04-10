namespace KinonekoSoftware.UI.Charts
{
    public sealed class ChartPaletteFactory : IChartPaletteFactory
    {

        public IChartPalette Factory(string stroke, string color) => ChartPalette.Create(stroke, color);
    }
}