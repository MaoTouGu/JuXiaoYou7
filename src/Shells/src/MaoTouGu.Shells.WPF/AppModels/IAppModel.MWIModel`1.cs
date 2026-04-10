namespace MaoTouGu.Shells.AppModels
{
    public abstract class MultipleWindowModel<TMainWindow, THostWindow> : MultipleWindowModel
        where TMainWindow : MTGWindow, new()
        where THostWindow : MTGWindow, new()
    {
        protected sealed override Window CreateNewWindowContentHost() => new THostWindow();

        protected sealed override bool IsMainWindow(Window window) => window is TMainWindow;
    }
}