﻿using Mimo.Dbcontext.DataItemConversion;
using Mimo.Dbcontext.DataTransferObjects.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Mimo.Dbcontext.DatabaseContext
{

    public partial class MssqlContext : BaseDBContext
    {

        #region IDisposable Members

        public override void Dispose()
        {
            this.databaseSqlConnection.Dispose();
            this.databaseSqlConnection = null;
            this.connectionString = null;
        }

        #endregion

        public String connectionString;

        public MssqlContext(String connectionString)
        {
            this.connectionString = connectionString;
            InitializationSession();
        }

        private SqlConnection databaseSqlConnection;

        public SqlConnection DatabaseSqlConnection
        {
            get { return databaseSqlConnection; }
        }

        private void InitializationSession()
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new DatabaseException(DatabaseException.ErrorCode.INVALID_CONNECTION_STRING);
            SqlConnection tempSqlClient = new SqlConnection(connectionString);
            InitializationSession(tempSqlClient);
        }

        private void InitializationSession(SqlConnection sqlClient)
        {
            if (sqlClient == null)
                throw new DatabaseException(DatabaseException.ErrorCode.INITIALIZE_FAILED);
            this.databaseSqlConnection = sqlClient;
        }

        #region "Database Helper Methods"
        // GetDbSqlCommand
        public override IDbCommand GetSqlCommand(string sqlQuery)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = DatabaseSqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = sqlQuery;
            return command;
        }

        // GetDbSprocCommand
        public override IDbCommand GetSprocCommand(string sprocName)
        {
            SqlCommand command = new SqlCommand(sprocName);
            command.Connection = DatabaseSqlConnection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }


        // CreateNullParameter
        public override DbParameter CreateNullParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Value = DBNull.Value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateNullParameter - with size for nvarchars
        public override DbParameter CreateNullParameter(string name, SqlDbType paramType, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Size = size;
            parameter.Value = DBNull.Value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }


        // CreateOutputParameter
        public override DbParameter CreateOutputParameter(string name, SqlDbType paramType)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }


        // CreateOuputParameter - with size for nvarchars
        public override DbParameter CreateOutputParameter(string name, SqlDbType paramType, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = paramType;
            parameter.Size = size;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }


        public override DbParameter CreateParameter(String name, Decimal? value)
        {
            if (!value.HasValue)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Decimal);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Decimal;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }


        // CreateParameter - uniqueidentifier
        public override DbParameter CreateParameter(string name, Guid value)
        {
            if (value.Equals(NullValues.NullGuid))
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.UniqueIdentifier);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.UniqueIdentifier;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }


        // CreateParameter - int
        public override DbParameter CreateParameter(string name, Int32 value)
        {
            if (value == NullValues.NullInt)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Int);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Int;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }


        // CreateParameter - datetime
        public override DbParameter CreateParameter(string name, DateTime value)
        {
            if (value == NullValues.NullDateTime)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.DateTime);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.DateTime;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        // CreateParameter - datetime?        
        public DbParameter CreateParameter(string name, DateTime? value)
        {
            if (value == NullValues.NullDateTime || value == null)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.DateTime);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.DateTime;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        // CreateParameter - boolean
        public override DbParameter CreateParameter(string name, bool value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.Bit;
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        // CreateParameter - boolean?
        public override DbParameter CreateParameter(string name, bool? value)
        {

            if (value == null)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Bit);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Bit;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        // CreateParameter - nvarchar
        public override DbParameter CreateParameter(string name, string value, int size)
        {
            if (String.IsNullOrEmpty(value))
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.NVarChar);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Size = size;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public override DbParameter CreateParameter(string name, long value)
        {
            if (value == NullValues.NullLong)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.BigInt);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.BigInt;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public override DbParameter CreateParameter(string name, decimal value)
        {
            if (value == NullValues.NullDecimal)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Decimal);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Decimal;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        public override DbParameter CreateParameter(string name, double value)
        {
            if (value == NullValues.NullDouble)
            {
                // If value is null then create a null parameter
                return CreateNullParameter(name, SqlDbType.Float);
            }
            else
            {
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Float;
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        private void OpenConnection(IDbConnection sqlConnection)
        {
            if (sqlConnection == null)
                throw new DatabaseException(DatabaseException.ErrorCode.INITIALIZE_FAILED);
            try
            {
                sqlConnection.Open();
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.CONNECT_FAILED, "", sqlExp);
            }
        }

        private void CloseConnection(IDbConnection sqlConnection)
        {
            try
            {
                sqlConnection.Close();
            }
            catch
            {
            }
        }
        #endregion

        #region "Data Projection Methods"

        // ExecuteNonQuery
        public override Int32 ExecuteNonQuery(IDbCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                return command.ExecuteNonQuery();
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }

        // ExecuteScalar
        public override Object ExecuteScalar(IDbCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                return command.ExecuteScalar();
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }

        // GetSingleValue
        public override T GetSingleValue<T>(IDbCommand command)
        {
            T returnValue = default(T);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                SqlDataReader reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = (T)reader[0]; }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnValue;
        }


        // GetSingleString
        public override Int32 GetSingleInt32(IDbCommand command)
        {
            Int32 returnValue = default(int);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                SqlDataReader reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = reader.GetInt32(0); }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnValue;
        }


        // GetSingleString
        public override string GetSingleString(IDbCommand command)
        {
            string returnValue = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                SqlDataReader reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = reader.GetString(0); }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnValue;
        }


        // GetStringList
        public override List<string> GetStringList(IDbCommand command)
        {
            List<string> returnList = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                SqlDataReader reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    returnList = new List<string>();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) { returnList.Add(reader.GetString(0)); }
                    }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnList;
        }


        // GetInt32List
        public override List<int> GetInt32List(IDbCommand command)
        {
            List<int> returnList = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                SqlDataReader reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    returnList = new List<int>();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) { returnList.Add(reader.GetInt32(0)); }
                    }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnList;
        }

        // GetSingle
        public override T GetSingle<T>(IDbCommand command)
        {
            T modelObj = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                SqlDataReader reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    reader.Read();
                    IDataTransferObject<T> mapper = DataMapperFactory.GetMapper<T>();
                    modelObj = mapper.EntityMapping(reader);
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            // return the MODEL, it's either populated with data or null.
            return modelObj;
        }

        // GetList
        public override List<T> GetList<T>(IDbCommand command)
        {
            List<T> modelObjList = new List<T>();
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    OpenConnection(command.Connection);

                var reader = command.ExecuteReader() as SqlDataReader;
                if (reader.HasRows)
                {
                    IDataTransferObject<T> mapper = DataMapperFactory.GetMapper<T>();
                    modelObjList = mapper.GetList(reader).ToList<T>();
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            // We return either the populated list if there was data,
            // or if there was no data we return an empty list.
            return modelObjList;
        }

        #endregion


        #region Async Command


        // ExecuteNonQuery
        public override async Task<Int32> ExecuteNonQueryAsync(IDbCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                return await cmd.ExecuteNonQueryAsync();
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }

        // ExecuteScalar
        public override async Task<Object> ExecuteScalarAsync(IDbCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                return await cmd.ExecuteScalarAsync();
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }

        // GetSingleValue
        public override async Task<T> GetSingleValueAsync<T>(IDbCommand command)
        {
            T returnValue = default(T);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = (T)reader[0]; }
                    reader.Close();
                }
                return returnValue;
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }


        // GetSingleString
        public override async Task<Int32> GetSingleInt32Async(IDbCommand command)
        {
            Int32 returnValue = default(int);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = reader.GetInt32(0); }
                    reader.Close();
                }
                return returnValue;
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }


        // GetSingleString
        public override async Task<String> GetSingleStringAsync(IDbCommand command)
        {
            string returnValue = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0)) { returnValue = reader.GetString(0); }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnValue;
        }


        // GetStringList
        public override async Task<List<String>> GetStringListAsync(IDbCommand command)
        {
            List<String> returnList = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    returnList = new List<String>();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) { returnList.Add(reader.GetString(0)); }
                    }
                    reader.Close();
                }
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
            return returnList;
        }


        // GetInt32List
        public override async Task<List<int>> GetInt32ListAsync(IDbCommand command)
        {
            List<int> returnList = null;
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    returnList = new List<int>();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) { returnList.Add(reader.GetInt32(0)); }
                    }
                    reader.Close();
                }
                return returnList;
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }

        // GetSingle
        public override async Task<T> GetSingleAsync<T>(IDbCommand command)
        {
            T modelObj = default(T);
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    OpenConnection(command.Connection);
                }
                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    reader.Read();
                    IDataTransferObject<T> mapper = DataMapperFactory.GetMapper<T>();
                    modelObj = mapper.EntityMapping(reader);
                    reader.Close();
                }
                // return the MODEL, it's either populated with data or null.
                return modelObj;
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }

        // GetList
        public override async Task<List<T>> GetListAsync<T>(IDbCommand command)
        {
            List<T> modelObjList = new List<T>();
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    OpenConnection(command.Connection);

                var cmd = command as SqlCommand;
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    IDataTransferObject<T> mapper = DataMapperFactory.GetMapper<T>();
                    modelObjList = mapper.GetList(reader).ToList<T>();
                    reader.Close();
                }
                // We return either the populated list if there was data,
                // or if there was no data we return an empty list.
                return modelObjList;
            }
            catch (DatabaseException dbExp)
            {
                throw dbExp;
            }
            catch (SqlException sqlExp)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.EXECUTE_ERROR, "Error executing query", sqlExp);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseException.ErrorCode.UNKNOWN, "Error executing query", e);
            }
            finally
            {
                CloseConnection(command.Connection);
            }
        }
        #endregion

    }
}