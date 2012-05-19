using System;
using System.Data;
using NHibernate;
using NHibernate.Transaction;

namespace DataAccessLayerTest
{
    public class MockTransaction : ITransaction
    {
        #region Implementation of IDisposable

        public void Dispose()
        {
            
        }

        #endregion

        #region Implementation of ITransaction

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void Begin(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            
        }

        public void Rollback()
        {
            WasRolledBack = true;
        }

        public bool WasRolledBack { get; set; }

        public void Enlist(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public void RegisterSynchronization(ISynchronization synchronization)
        {
            throw new NotImplementedException();
        }

        public bool IsActive
        {
            get { throw new NotImplementedException(); }
        }


        public bool WasCommitted
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}