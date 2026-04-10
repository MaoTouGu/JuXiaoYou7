namespace KinonekoSoftware.UI.Core
{
    public sealed class ResourceHelper : IResourceHelper
    {
        public object FindResource(string key)
        {

            if (Application.Current.TryFindResource(key, out var r))
            {
                return r;
            }

            return null;
        }
    }
}