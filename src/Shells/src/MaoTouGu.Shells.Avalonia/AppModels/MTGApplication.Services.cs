using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.AppModels
{
    partial class MTGApplication
    {
        void RegisterForwardServices()
        {
            Ioc.Use<IViewLocator, IViewAmbient, IViewAmbient2, ViewService>(new ViewService());
            
        }
    }
}