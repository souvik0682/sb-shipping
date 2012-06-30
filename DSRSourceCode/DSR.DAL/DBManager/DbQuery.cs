
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DSR.Common;

namespace DSR.DAL.DbManager
{
	#region Enum: Parameter Directions
	/// <summary>
	/// Enum for Parameter Directions
	/// </summary>
	public enum QueryParameterDirection: int
	{
		/// <summary>
		/// The parameter is an input parameter.
		/// </summary>
		Input  = 1,
		/// <summary>
		/// The parameter is capable of both input and output.
		/// </summary>
		Output = 2,
		/// <summary>
		/// The parameter represents a return value from an 
		/// operation such as a stored procedure, built-in
		/// function, or user-defined function.
		/// </summary>
		Return = 3
	}
	#endregion

	/// <summary>
	/// Class to execute any kind of database query.
	/// </summary>
    public sealed class DbQuery : DbManagerBase, IDisposable
	{
		#region FIELDS
		private string m_strCommandText = string.Empty;
		private bool m_blnSP = true;
        private List<SqlParameter> m_oParameters = new List<SqlParameter>();
		private bool m_blnLocalConn = true;
		#endregion

		#region Constructors
        
		/// <summary>
		/// Initializes a Stored Procedure query with
		/// the given Stored Procedure name.
		/// </summary>
		/// <param name="StoredProcName">Name of the Stored Procedure.</param>
		public DbQuery(string StoredProcName): this(StoredProcName, false)
		{
		}
		/// <summary>
		/// Initializes a query with the given Stored Procedure
		/// name or the query text and the query type (Text or Stored
		/// Procedure).
		/// </summary>
		/// <param name="SqlString">Query Text/ Stored Procedure name.</param>
		/// <param name="IsTextQuery">True->Text Query, False->Stored Procedure</param>
		public DbQuery(string SqlString, bool IsTextQuery)
		{
			m_blnSP = !IsTextQuery;
			m_strCommandText = SqlString;
		}
		#endregion


		// #################### M E T H O D S ####################
		#region Run Query
		#region DataTable
		/// <summary>
		/// Executes the current query and returns the result
		/// in a DataTable object.
		/// </summary>
		/// <returns>The query result set</returns>
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(oCmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.m_blnLocalConn)
                {
                    this.m_oConn.Close();
                }
                oCmd.Dispose();
            }

            return dt;
        }
		#endregion

        #region DataTableReader
        /// <summary>
        /// Executes the current query and returns the result
        /// in a DataTableReader object.
        /// </summary>
        /// <returns>The query result set</returns>
        public DataTableReader GetTableReader()
        {
            DataTable dt = new DataTable();
            DataTableReader dtr = null;

            SqlCommand oCmd = new SqlCommand();
            this.InitQuery(oCmd);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(oCmd);
                da.Fill(dt);
                dtr = dt.CreateDataReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.m_blnLocalConn)
                {
                    this.m_oConn.Close();
                }
                oCmd.Dispose();
            }

            return dtr;
        }
        #endregion

        #region DataSet
		/// <summary>
		/// Executes the current query and returns the result
		/// in a DataSet object.
		/// </summary>
		/// <returns>The query result set</returns>
		public DataSet GetTables()
		{
			SqlCommand oCmd = new SqlCommand();
			this.InitQuery(oCmd);

			SqlDataAdapter da = new SqlDataAdapter(oCmd);
			DataSet ds = new DataSet();

			try
			{
				da.Fill(ds);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (this.m_blnLocalConn)
				{
					this.m_oConn.Close();
				}
				oCmd.Dispose();
			}

			return ds;
		}
		#endregion

		#region NonQuery
		/// <summary>
		/// Executes a DML type query (with no result set).
		/// </summary>
		/// <returns>Number of affected rows.</returns>
		public int RunActionQuery()
		{
			int intRowsAffected = -1;

			SqlCommand oCmd = new SqlCommand();
			this.InitQuery(oCmd);

			try
			{
				intRowsAffected = oCmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (this.m_blnLocalConn)
				{
					this.m_oConn.Close();
				}
				oCmd.Dispose();
			}

			return intRowsAffected;
		}
		#endregion

		#region Scalar
		/// <summary>
		/// Executes the query, and returns the first column of the
		/// first row in the result set returned by the query. 
		/// Extra columns or rows are ignored.
		/// </summary>
		/// <returns>The first column of the first row in the result
		/// set, or a null reference if the result set is empty.</returns>
		public object GetScalar()
		{
			object oRetVal = null;

			SqlCommand oCmd = new SqlCommand();
			this.InitQuery(oCmd);

			try
			{
				oRetVal = oCmd.ExecuteScalar();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (this.m_blnLocalConn)
				{
					this.m_oConn.Close();
				}
				oCmd.Dispose();
			}

			return oRetVal;
		}
		#endregion

		#region Initializes a Query
		/// <summary>
		/// Performs the initial tasks before executing a query.
		/// </summary>
		/// <param name="oCmd">Command object holding the query.</param>
		private void InitQuery(SqlCommand oCmd)
		{
			// set Connection
			m_blnLocalConn = (this.m_oConn == null);
			if (m_blnLocalConn)
			{
				m_oConn = new DataConnection();
				m_blnLocalConn = true;
				m_oConn.Open();
			}
			oCmd.Connection = m_oConn.m_oConn;

			// set Command
			oCmd.CommandText = this.m_strCommandText;
			oCmd.CommandType = (this.m_blnSP? CommandType.StoredProcedure: CommandType.Text);

			// set timeout
			oCmd.CommandTimeout = (6 * 60 * 60);	// 6 hours

			// set Parameters
            foreach (SqlParameter oParam in this.m_oParameters)
			{
				oCmd.Parameters.Add(oParam);
			}
		}
		#endregion
		#endregion

		#region Parameter handling
		#region Type: Integer
		/// <summary>
		/// Adds an Integer type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddIntegerParam(string Name, Nullable<int> Value)
		{
			AddIntegerParam(Name, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds an Integer type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddIntegerParam(string Name, Nullable<int> Value, QueryParameterDirection Direction)
		{
			SqlParameter oPara = new SqlParameter(Name, SqlDbType.Int);
			oPara.Direction = GetParaType( Direction );
            
            if (Value.HasValue)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }
			
            this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: Char
		/// <summary>
		/// Adds a Char type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddCharParam(string Name, int Size, Nullable<char> Value)
		{
			AddCharParam(Name, Size, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a Char type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddCharParam(string Name, int Size, Nullable<char> Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Char, Size);
            oPara.Direction = GetParaType(Direction);

            if (Value.HasValue)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }
			
			this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: NChar
		/// <summary>
		/// Adds a NChar type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddNCharParam(string Name, int Size, Nullable<char> Value)
		{
			AddNCharParam(Name, Size, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a NChar type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddNCharParam(string Name, int Size, Nullable<char> Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.NChar, Size);
            oPara.Direction = GetParaType(Direction);

            if (Value.HasValue)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }

			this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: Varchar
		/// <summary>
		/// Adds a Varchar type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddVarcharParam(string Name, int Size, string Value)
		{
			AddVarcharParam(Name, Size, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a Varchar type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddVarcharParam(string Name, int Size, string Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.VarChar, Size);
            oPara.Direction = GetParaType(Direction);

            if (null != Value)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }

			this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: NVarchar
		/// <summary>
		/// Adds a NVarchar type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddNVarcharParam(string Name, int Size, string Value)
		{
			AddNVarcharParam(Name, Size, Value, QueryParameterDirection.Input);
		}
        /// <summary>
        /// Adds a NVarchar type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        public void AddNVarcharParam(string Name, string Value)
        {
            AddNVarcharParam(Name, Value, QueryParameterDirection.Input);
        }

        /// <summary>
        /// Adds a NVarchar type input query parameter.
        /// </summary>
        /// <param name="Name">Name of the parameter.</param>
        /// <param name="Value">Value of the parameter.</param>
        /// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddNVarcharParam(string Name, string Value, QueryParameterDirection Direction)
        {
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.NVarChar);
            oPara.Direction = GetParaType(Direction);

            if (null != Value)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }

            this.m_oParameters.Add(oPara);
        }


		/// <summary>
		/// Adds a NVarchar type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Size">Size of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
		public void AddNVarcharParam(string Name, int Size, string Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.NVarChar, Size);
            oPara.Direction = GetParaType(Direction);

            if (null != Value)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }
			
			this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: Boolean
		/// <summary>
		/// Adds a Boolean type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddBooleanParam(string Name, Nullable<bool> Value)
		{
			AddBooleanParam(Name, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a Boolean type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddBooleanParam(string Name, Nullable<bool> Value, QueryParameterDirection Direction)
		{
			SqlParameter oPara = new SqlParameter(Name, SqlDbType.Bit);
			oPara.Direction = GetParaType( Direction );

            if (Value.HasValue)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }
			
            this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: DateTime
		/// <summary>
		/// Adds a DateTime type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddDateTimeParam(string Name, Nullable<DateTime> Value)
		{
			AddDateTimeParam(Name, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a DateTime type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddDateTimeParam(string Name, Nullable<DateTime> Value, QueryParameterDirection Direction)
		{
			SqlParameter oPara = new SqlParameter(Name, SqlDbType.DateTime);
			oPara.Direction = GetParaType( Direction );

            if (Value.HasValue)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }
			
            this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: Text
		/// <summary>
		/// Adds a Text type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddTextParam(string Name, string Value)
		{
			AddTextParam(Name, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a Text type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
		public void AddTextParam(string Name, string Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Text);
            oPara.Direction = GetParaType(Direction);

            if (null != Value)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }

            this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: NText
		/// <summary>
		/// Adds an NText type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddNTextParam(string Name, string Value)
		{
			AddNTextParam(Name, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds an NText type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
		public void AddNTextParam(string Name, string Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.NText);
            oPara.Direction = GetParaType(Direction);

            if (null != Value)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }

            this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: Decimal
		/// <summary>
		/// Adds a Decimal type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
        /// <param name="Precision">Precision of the decimal number</param>
        /// <param name="Scale">Scale of the decimal number</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddDecimalParam(string Name, byte Precision, byte Scale, Nullable<decimal> Value)
		{
			AddDecimalParam(Name, Precision, Scale, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds a Decimal type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
        /// <param name="Precision">Precision of the decimal number</param>
        /// <param name="Scale">Scale of the decimal number</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
        public void AddDecimalParam(string Name, byte Precision, byte Scale, Nullable<decimal> Value, QueryParameterDirection Direction)
		{
			SqlParameter oPara = new SqlParameter(Name, SqlDbType.Decimal);
            oPara.Direction = GetParaType(Direction);

            oPara.Precision = Precision;
            oPara.Scale = Scale;

            if (Value.HasValue)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }

            this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Type: Image
		/// <summary>
		/// Adds an Image type input query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		public void AddImageParam(string Name, byte[] Value)
		{
			AddImageParam(Name, Value, QueryParameterDirection.Input);
		}
		/// <summary>
		/// Adds an Image type query parameter with
		/// the given direction type. 
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		/// <param name="Value">Value of the parameter.</param>
		/// <param name="Direction">Parameter Direction: Input/ Output/ Return</param>
		public void AddImageParam(string Name, byte[] Value, QueryParameterDirection Direction)
		{
            SqlParameter oPara = new SqlParameter(Name, SqlDbType.Image);
            oPara.Direction = GetParaType(Direction);

            if (null != Value)
            {
                oPara.Value = Value;
            }
            else
            {
                oPara.Value = DBNull.Value;
            }
			
			this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Adds a NULL value Parameter
		/// <summary>
		/// Adds a NULL value query parameter.
		/// </summary>
		/// <param name="Name">Name of the parameter.</param>
		public void AddNullValuePara(string Name)
		{
			SqlParameter oPara = new SqlParameter(Name, DBNull.Value);
			oPara.Direction = ParameterDirection.Input;
			this.m_oParameters.Add( oPara );
		}

		/// <summary>
		/// Adds a NULL value query parameter with Parameter Direction
		/// </summary>
		/// <param name="Name">Name of parameter</param>
		/// <param name="Direction">Parameter Direction</param>
		public void AddNullValuePara(string Name, QueryParameterDirection Direction)
		{
			SqlParameter oPara = new SqlParameter(Name, DBNull.Value);
			oPara.Direction = GetParaType( Direction );
			this.m_oParameters.Add( oPara );
		}
		#endregion

		#region Adds the Return Parameter
		/// <summary>
		/// Adds the return parameter.
		/// </summary>
		public void AddReturnPara()
		{
			this.AddIntegerParam("ReturnIntPara", 0, QueryParameterDirection.Return);
		}
		#endregion

		#region Returns the value of the passed parameter
		/// <summary>
		/// Returns the value of a parameter.
		/// </summary>
		/// <param name="ParaName">Name of the parameter.</param>
		/// <returns>Value of the parameter.</returns>
		public object GetParaValue(string ParamName)
		{
			object oValue = null;

			ParamName = ParamName.Trim().ToLower();
            foreach (SqlParameter oParam in this.m_oParameters)
			{
				if ( oParam.ParameterName.ToLower() == ParamName )
				{
					oValue = oParam.Value;
					break;
				}
			}

			return oValue;
		}
		#endregion

		#region Returns the value of the Return Parameter
		/// <summary>
		/// Returns the value of the Return Parameter.
		/// </summary>
		/// <returns>The value of the Return Parameter.</returns>
		public object GetReturnParaValue()
		{
			return this.GetParaValue("ReturnIntPara");
		}
		#endregion

		#region Clears the parameters
		/// <summary>
		/// Clears the parameters collection.
		/// </summary>
		public void ClearParameters()
		{
			this.m_oParameters.Clear();
		}
		#endregion

		#region Converts enum to parameter direction
		/// <summary>
		/// Converts parameter direction enum to the underlying sql type
		/// </summary>
		/// <param name="Direction">Enum value to convert</param>
		/// <returns>Underlying SqlClient value corresponding to the passed Enum</returns>
		private ParameterDirection GetParaType(QueryParameterDirection Direction)
		{
			switch (Direction)
			{
				case QueryParameterDirection.Output:
					return ParameterDirection.InputOutput;
				case QueryParameterDirection.Return:
					return ParameterDirection.ReturnValue;
				default:
					return ParameterDirection.Input;
			}
		}
		#endregion
		#endregion

		#region Dispose
		/// <summary>
		/// Releases the resources.
		/// </summary>
		public void Dispose()
		{
			this.m_oConn.Dispose();
			this.m_oParameters.Clear();
		}
		#endregion


		// ################## P R O P E R T I E S ##################
		#region Connection
		private DataConnection m_oConn = null;
		/// <summary>
		/// Write Only: Connection object to run the query. To be used
		/// in transactional operations involving multiple objects.
		/// Also used in performing multiple database operations using
		/// the same connection.
		/// </summary>
		public DataConnection Connection
		{
			set
			{
				m_oConn = value;
			}
		}
		#endregion
	}
}