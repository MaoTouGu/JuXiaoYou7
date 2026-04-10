using System.Collections.ObjectModel;
using System.ComponentModel;
using KinonekoSoftware.UI.Charts;

namespace KinonekoSoftware.UI.Charts
{
    public class ChartSeries : ObservableCollection<Series>, IDataSourceTracker
    {
        private double _min = 0d;
        private double _max = 100d;
        
        private void OnTrackingObject(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionChanged(CollectionExt.ResetCollectionChanged);
            DataChanged?.Invoke(sender, EventArgs.Empty);
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
            
            DataChanged?.Invoke(instance, EventArgs.Empty);
        }

        protected override void ClearItems()
        {
            foreach (var x in Items)
            {
                Tracking(x, false);
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, Series item)
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

        protected override void SetItem(int index, Series item)
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


        public double Maximum
        {
            get => _max;
            set
            {
                _max = value;
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public double Minimum
        {
            
            get => _min;
            set
            {
                _min = value;
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        
        internal double MaximumInternal
        {
            set => _max = value;
        }

        internal double MinimumInternal
        {
            set => _min = value;
        }
        
        public event EventHandler DataChanged;
    }
}