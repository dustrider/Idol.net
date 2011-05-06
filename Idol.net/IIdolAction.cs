namespace Rbi.Search
{
    public interface IIdolAction<out TResultSet>
    {
        string Command { get; }
        TResultSet Execute();
        void ExecuteAsync();
        void ExecuteAsync(object userState);
    }
}
