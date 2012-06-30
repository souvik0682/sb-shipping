using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DSR.Common;

namespace DSR.DAL.DbManager
{
	/// <summary>
	/// Encapsulates the functionalities of the database connection class.
	/// This class is used by the business logic component only when it needs
	/// to perform database related operations of different objects within a
	/// single transaction boundary.
	/// </summary>
    public sealed class DataConnection : DbManagerBase, IDisposable
	{

        
		#region FIELDS
		private static string m_sConnStr = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
		internal SqlConnection m_oConn = null;
        
		internal SqlTransaction m_oTran = null;
		#endregion


		#region Constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public DataConnection()
		{
		}
		#endregion


		// #################### M E T H O D S ####################
		#region Opens a connection
		/// <summary>
		/// Opens the the database connection.
		/// </summary>
		/// <returns>Whether the connection has been successfully opened or not.</returns>
		public bool Open()
		{
			return Open(m_sConnStr);
		}
		/// <summary>
		/// Opens the the database connection with a given connection string.
		/// </summary>
		/// <param name="ConnectionString">The connection string.</param>
		/// <returns>Whether the connection has been successfully opened or not.</returns>
		public bool Open(string ConnectionString)
		{
			m_blnIsOpen = false;

			m_oConn = new SqlConnection(ConnectionString);
			m_oConn.Open();
			m_blnIsOpen = true;

			return m_blnIsOpen;
		}
		#endregion

		#region Closes a connection
		/// <summary>
		/// Closes the connection (if open).
		/// </summary>
		public void Close()
		{
			if (m_oConn.State == ConnectionState.Open)
			{
				m_oConn.Close();
				m_oConn = null;
				m_blnIsOpen = false;
			}
		}
		#endregion

		#region Transaction handling related methods
		/// <summary>
		/// Starts a Transaction session on the current connection.
		/// </summary>
		public void BeginTran()
		{
			if (m_oConn.State == ConnectionState.Open)
			{
				m_oTran = m_oConn.BeginTransaction();
				m_blnTranActive = true;
			}
		}
		/// <summary>
		/// Commits the currently active Transaction (if any).
		/// </summary>
		public void CommitTran()
		{
			if ( (m_oConn.State == ConnectionState.Open) && m_blnTranActive )
			{
				m_oTran.Commit();
				m_blnTranActive = false;
			}
		}
		/// <summary>
		/// Rools back the currently active Transaction (if any).
		/// </summary>
		public void RollbackTran()
		{
			if ( (m_oConn.State == ConnectionState.Open) && m_blnTranActive )
			{
				m_oTran.Rollback();
				m_blnTranActive = false;
			}
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Releases the resources.
		/// </summary>
		public void Dispose()
		{
			if (m_oConn != null)
			{
				m_oConn.Dispose();
			}
		}
		#endregion


		// ################## P R O P E R T I E S ##################
		#region IsOpen
		private bool m_blnIsOpen = false;
		/// <summary>
		/// Indicates whether the connection is currently open or not.
		/// </summary>
		public bool IsOpen
		{
			get
			{
				return m_blnIsOpen;
			}
		}
		#endregion

		#region IsTransactionActive
		private bool m_blnTranActive = false;
		/// <summary>
		/// Indicates whether there is any active Transaction or not.
		/// </summary>
		public bool IsTransactionActive
		{
			get
			{
				return m_blnTranActive;
			}
		}
		#endregion
	}
}