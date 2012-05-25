using System;
using System.Data.SqlClient;
using DAL.Resources;
using System.Configuration;

namespace DAL
{
    /// <summary>
    /// Base Class of all Operations Classes
    /// </summary>
    public abstract class DataManagment
    {
        #region member variables

        private string connectionString = 
            ConfigurationManager.ConnectionStrings["DALConnectionString"].ConnectionString;
        private SqlConnection connection;
        private SqlTransaction trans;

        #endregion

        #region Properties

        /// <summary>
        ///  Gets a Connection
        /// </summary>
        protected SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    return connection;
                }
                else
                {
                    return connection;
                }
            }
        }

        /// <summary>
        /// Gets a Transaction
        /// </summary>
        protected SqlTransaction Trans
        {
            get
            {
                if (trans == null)
                {
                    trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    return trans;
                }
                else
                {
                    return trans;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the Connection String
        /// </summary>
        protected string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens the connection, if the connection is not created yet then creats a new connection and opens it
        /// </summary>
        protected void OpenConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            else
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
        }

        /// <summary>
        /// Closes the Connection
        /// </summary>
        protected void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            else
            {
                throw new Exception(ErrorMessages.CloseNullConnection);
            }
        }

        #endregion
    }
}
