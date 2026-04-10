
namespace KinonekoSoftware.UI.Charts
{
    public class ChartAxisCollection : ObservableCollection<ChartAxis>
    {
        private void OnTrackingObject(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionChanged(CollectionExt.ResetCollectionChanged);
            AxisChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Tracking(INotifyPropertyChanged instance, bool track)
        {
            if (track)
            {
                instance.PropertyChanged += OnTrackingObject;
            }
            else
            {
                instance.PropertyChanged -= OnTrackingObject;
            }
        }

        protected override void ClearItems()
        {
            foreach (var x in Items)
            {
                Tracking(x, false);
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, ChartAxis item)
        {
            if (item is null)
            {
                return;
            }
            
            Tracking(item, true);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            var item = Items[index];
            if (item is null)
            {
                return;
            }
            
            Tracking(item, false);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, ChartAxis item)
        { 
            if (item is null)
            {
                return;
            }
            
            var oldItem = Items[index];
            if (oldItem is null)
            {
                return;
            }
            
            Tracking(oldItem, false);
            Tracking(item, true);
            base.SetItem(index, item);
        }
        
        public event EventHandler AxisChanged;
    }
}