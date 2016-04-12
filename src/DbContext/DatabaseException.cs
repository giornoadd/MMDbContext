using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Mimo.Dbcontext
{
    [Serializable]
    public class DatabaseException : Exception, ISerializable
    {
        public enum ErrorCode
        {
            SUCCESS,
            INITIALIZE_FAILED,
            CONNECT_FAILED,
            INVALID_CONNECTION_STRING,
            INVALID_MEMORY_ALLOCATE,
            EXECUTE_ERROR,
            UNKNOWN
        }

        private String customMessage;
        private ErrorCode dbErrorCode;

        public DatabaseException(ErrorCode errorCode)
            : this(errorCode, null)
        {
        }

        public DatabaseException(ErrorCode errorCode, String message)
            : this(errorCode, message, null)
        {
        }

        public DatabaseException(ErrorCode errorCode, String message, Exception innerException)
            : base(message, innerException)
        {
            this.customMessage = message;
            this.dbErrorCode = errorCode;
        }

        private static IDictionary<ErrorCode, String> errorMessage;

        static DatabaseException()
        {
            InitializeException();
        }

        private static void InitializeException()
        {
            errorMessage = new Dictionary<ErrorCode, String>();
            errorMessage.Add(ErrorCode.CONNECT_FAILED, "Failed to connect database");
            errorMessage.Add(ErrorCode.SUCCESS, "Success");
            errorMessage.Add(ErrorCode.INITIALIZE_FAILED, "Cannot initialize database server");
            errorMessage.Add(ErrorCode.INVALID_CONNECTION_STRING, "Invalid connection (username, password or permission) is invalid");
            errorMessage.Add(ErrorCode.INVALID_MEMORY_ALLOCATE, "Memory allocate filed");
            errorMessage.Add(ErrorCode.EXECUTE_ERROR, "Exceuting sql command error");
            errorMessage.Add(ErrorCode.UNKNOWN, "Unknown error is occured");

        }


        public ErrorCode Code
        {
            get
            {
                return this.dbErrorCode;
            }
        }


        public override string Message
        {
            get
            {
                return errorMessage[dbErrorCode] + " " + this.customMessage;
            }
        }

    }
}
