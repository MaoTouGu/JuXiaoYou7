using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaoTouGu.Foundation
{
    public abstract class ObservableObject : IViewObject
    {
        private PropertyChangedEventHandler  PropertyChangedHandler;
        private PropertyChangingEventHandler PropertyChangingHandler;


        /// <summary>
        /// 触发某个属性的数据已经变化的通知（这个通知能够影响绑定）
        /// </summary>
        /// <param name="propertyName">指定数据已经变化的属性名</param>
        protected internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(propertyName, null);
        }
        
        /// <summary>
        /// 触发某个属性的数据已经变化的通知（这个通知能够影响绑定）
        /// </summary>
        /// <param name="propertyName">指定数据已经变化的属性名</param>
        protected internal void RaiseUpdated([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 触发某个属性的数据正在变化的通知（这个通知不能够影响绑定）
        /// </summary>
        /// <param name="propertyName">指定数据正在变化的属性名</param>
        protected void RaiseUpdating([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            PropertyChangingHandler?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// 设置指定属性的值，并且触发属性正在变更、属性已经变更通知。
        /// </summary>
        /// <param name="source">要触发的属性字段</param>
        /// <param name="value">新的值</param>
        /// <param name="name">属性名</param>
        /// <typeparam name="T">值的类型</typeparam>
        /// <returns>如果新的值和属性字段不一致，则触发属性变更通知并返回true，否则不触发返回false。</returns>
        protected bool SetValue<T>(ref T source, T value, [CallerMemberName] string name = "")
        {
            if (string.IsNullOrEmpty(name)) return false;


            if (EqualityComparer<T>.Default.Equals(source, value))
            {
                return false;
            }


            RaiseUpdating(name);
            source = value;
            RaiseUpdated(name);
            OnPropertyChanged(name, value);
            return true;
        }
        
        protected virtual void OnPropertyChanged(string name, object value)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => PropertyChangedHandler += value;
            remove => PropertyChangedHandler -= value;
        }

        public event PropertyChangingEventHandler PropertyChanging
        {
            add => PropertyChangingHandler += value;
            remove => PropertyChangingHandler -= value;
        }
    }
}