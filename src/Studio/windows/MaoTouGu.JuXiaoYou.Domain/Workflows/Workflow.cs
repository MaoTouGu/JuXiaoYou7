namespace MaoTouGu.JuXiaoYou.Workflows
{
    public class Workflow : ObservableObject, ISortable<Workflow>
    {
        public int CompareTo(Workflow other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (other is null)
                return 1;
            return Index.CompareTo(other.Index);
        }

        public int Index { get; set; }
    }
}