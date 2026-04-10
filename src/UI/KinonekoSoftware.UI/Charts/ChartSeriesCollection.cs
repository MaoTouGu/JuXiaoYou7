using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace KinonekoSoftware.UI.Charts
{
    public class ChartSeriesCollection : ObservableCollection<ChartSeries>, IDataSourceTracker
    {
        private double _min = 0d;
        private double _max = 100d;

        private void OnTrackingObject(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnCollectionChanged(e);
            DataChanged?.Invoke(sender, EventArgs.Empty);
        }

        private void Tracking(ChartSeries instance, bool track)
        {
            if (track)
            {
                instance.CollectionChanged += OnTrackingObject;
                instance.DataChanged       += OnDataChanged;
            }
            else
            {
                instance.CollectionChanged -= OnTrackingObject;
                instance.DataChanged       -= OnDataChanged;
            }
        }
        
        private void OnDataChanged(object sender, EventArgs e)
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void ClearItems()
        {
            foreach (var x in Items)
            {
                Tracking(x, false);
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, ChartSeries item)
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

        protected override void SetItem(int index, ChartSeries item)
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

        public ChartSeries First
        {
            get
            {
                if (Count == 0)
                {
                    var first = new ChartSeries
                    {
                        new Series { Value = 0 },
                        new Series { Value = 6 },
                        new Series { Value = 0 },
                        new Series { Value = 10 },
                        new Series { Value = 4 },
                    };
                    first.Maximum = 10;
                    first.Minimum = 0;
                    Add(first);
                    return first;
                }

                return this[0];
            }
        }


        public double Maximum
        {
            get => _max;
            set
            {
                _max = value;
                foreach (var collection in Items)
                {
                    collection.MaximumInternal = value;
                }
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public double Minimum
        {

            get => _min;
            set
            {
                _min = value;

                foreach (var collection in Items)
                {
                    collection.MinimumInternal = value;
                }

                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler DataChanged;
    }
}