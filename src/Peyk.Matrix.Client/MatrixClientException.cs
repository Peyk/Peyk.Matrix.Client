using System;

namespace Peyk.Matrix.Client
{
    public class MatrixClientException : Exception
    {
        public MatrixClientError? Error { get; }

        public string ErrorCode { get; }

        public MatrixClientException(
            string errorCode,
            string message
        )
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public MatrixClientException(
            string errorCode,
            string message,
            Exception innerException
        )
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public MatrixClientException(
            MatrixClientError error
        )
            : this(error.Code, error.Message)
        {
            Error = error;
        }
    }
}