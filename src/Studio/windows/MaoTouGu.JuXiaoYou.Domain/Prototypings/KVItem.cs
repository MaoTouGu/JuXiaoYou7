namespace MaoTouGu.JuXiaoYou.Prototypings
{
    public class KVItem : ObservableObject
    {

        private string _name;
        private string _value;

        public string Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}