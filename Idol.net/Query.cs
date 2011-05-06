using System;
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
        private Term whereClause = null;
        private ReaderWriterLockSlim slimLock = new ReaderWriterLockSlim();

        internal Query(Func<XElement, TResultSet> resultSetFactory, IdolConnection<TResultSet> idolServer)
        {
            this.idolConnection = idolServer;
            this.resultSetFactory = resultSetFactory;
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

        public TResultSet Execute()
        {
            return resultSetFactory(idolConnection.GetXElement(idolConnection.Configuration.IdolActionUri, Command, true));
        }

        public string Command
        {
            get
            {
                slimLock.EnterReadLock();
                try
                {
                    if (whereClause != null)
                    {
                        return String.Format("a=query&text={0}&print=all&combine=simple&FieldText={1}", queryText,
                                             whereClause);
                    }
                    return String.Format("a=query&text={0}&combine=simple&print=all", queryText);
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
