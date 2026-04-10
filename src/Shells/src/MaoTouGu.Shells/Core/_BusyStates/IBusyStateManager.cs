using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.Core
{
    public interface IBusyStateManager
    {
        IBusyStateManager Execute(ObservableAsyncOperation operation);
        
        IBusyStateManager Execute(Action operation);
        IBusyStateManager Execute(Action<List<object>> operation, List<object> parameters);
        IBusyStateManager Execute(string text,Action<List<object>> operation, List<object> parameters);
        IBusyStateManager Execute(string text, Action operation);
        
        IBusyStateManager Condition(Func<bool> operation);
        IBusyStateManager Condition(Func<bool> expression, Action callback);
        IBusyStateManager Condition(string text, Func<bool> operation);
        IBusyStateManager Condition(string text, Func<bool> expression, Action callback);

        /// <summary>
        /// 开始执行。
        /// </summary>
        void Execute();
    }
}