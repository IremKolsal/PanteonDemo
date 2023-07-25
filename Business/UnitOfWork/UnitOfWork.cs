using DataAccess.Repository.Account;
using DataAccess.Repository.Building;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace Business.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        public AccountRepository Accounts;
        public BuildingRepository Buildings;
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString);
            _dbConnection.Open();
            _transaction = _dbConnection.BeginTransaction();
            Accounts = new AccountRepository(_dbConnection);
            Buildings = new BuildingRepository();
        }

        public void SaveChanges()
        {
            _transaction.Commit();
        }
        public void RollBack()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _dbConnection?.Close();
                    _dbConnection?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
