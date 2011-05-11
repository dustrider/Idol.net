using System;
using System.ComponentModel;

namespace Rbi.Search
{
    public class ExecuteCompletedEventArgs<TResultSet> : AsyncCompletedEventArgs
    {
        public ExecuteCompletedEventArgs(TResultSet result, Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {
            Result = result;
        }

        public TResultSet Result { get; private set; }
    }
}
