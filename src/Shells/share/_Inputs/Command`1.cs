namespace MaoTouGu.Shells.Inputs
{
    public abstract class Command<T>(T _context) : _Command
    {
        public T Context => _context;
    }
}