using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Xml.Linq;

[assembly: CLSCompliant(true)]
namespace Rbi.Search
{
    /// <summary>
    /// 
    /// </summary>
    public class Query<TResultSet> : IIdolAction<TResultSet>, IDisposable
    {
        private readonly IdolConnection<TResultSet> idolConnection;
        private readonly Func<XElement, TResultSet> resultSetFactory;
        private string queryText = "*";
        private Term whereClause;
        private ReaderWriterLockSlim slimLock = new ReaderWriterLockSlim();
        public int Offset { get; set; }
        public int PageSize { get; set; }
        private Sort sorting;

        internal Query(Func<XElement, TResultSet> resultSetFactory, IdolConnection<TResultSet> idolServer)
        {
            idolConnection = idolServer;
            this.resultSetFactory = resultSetFactory;
            Offset = 0;
            PageSize = 6;
            sorting = Sort.Unsorted;
        }

        public void Where(string freeTextQuery)
        {
            slimLock.EnterWriteLock();
            try
            {
                queryText = String.IsNullOrEmpty(freeTextQuery) ? "*" : freeTextQuery;
            }
            finally
            {
                slimLock.ExitWriteLock();
            }
        }

        public void Where(Term term)
        {
            slimLock.EnterWriteLock();
            try
            {
                whereClause = term;
            }
            finally
            {
                slimLock.ExitWriteLock();
            }
        }

        public void OrderBy(Sort sort)
        {
            slimLock.EnterWriteLock();
            try
            {
                sorting = sort;
            }
            finally
            {
                slimLock.ExitWriteLock();
            }
        }

        public TResultSet Execute()
        {
            return resultSetFactory(idolConnection.GetXElement(idolConnection.Configuration.IdolActionUri, Command, true));
        }

        public void ExecuteAsync()
        {
            var resultFunc = new Func<TResultSet>(Execute);
            resultFunc.BeginInvoke(InvokeExecuteCompleted, null);
        }

        public void ExecuteAsync(object userState)
        {
            var resultFunc = new Func<TResultSet>(Execute);
            resultFunc.BeginInvoke(InvokeExecuteCompleted, userState);
        }

        public void CancelAysnc(object userState)
        {
            throw new NotImplementedException();
        }

        public delegate void ExecuteCompletedEventHandler(object sender, ExecuteCompletedEventArgs<TResultSet> e);
        public event ExecuteCompletedEventHandler ExecuteCompleted;

        private void InvokeExecuteCompleted(IAsyncResult result)
        {
            var asyncResult = (AsyncResult)result;
            var del = (Func<TResultSet>)asyncResult.AsyncDelegate;
            var results = del.EndInvoke(result);
            var handler = ExecuteCompleted;
            if (handler != null)
            {
                handler(this, new ExecuteCompletedEventArgs<TResultSet>(results, null, false, asyncResult.AsyncState));
            }
        }


        public string Command
        {
            get
            {
                //This is (obviously) just test code still
                slimLock.EnterReadLock();
                try
                {
                    if (whereClause != null)
                    {
                        return String.Format("a=query&text={0}&print=all&combine=simple&FieldText={1}&sort={2}", queryText,
                                             whereClause, sorting);
                    }
                    return String.Format("a=query&text={0}&combine=simple&print=all&sort={1}", queryText, sorting);
                }
                finally
                {
                    slimLock.ExitReadLock();
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (slimLock != null)
                {
                    slimLock.Dispose();
                    slimLock = null;
                }
            }
        }

        #endregion
    }
}
