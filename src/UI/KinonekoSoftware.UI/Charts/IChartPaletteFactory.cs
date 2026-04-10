namespace KinonekoSoftware.UI.Charts
{
    public interface IChartPaletteFactory
    {
        IChartPalette Factory(string stroke, string color);
    }
}