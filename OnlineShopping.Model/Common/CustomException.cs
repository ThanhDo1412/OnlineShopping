using System;
using System.Net;

namespace OnlineShopping.Model.Common
{
    public class CustomException : Exception
    {
        public HttpStatusCode ErrorCode { get; }
        public string ErrorMessage { get; }
        public CustomException(HttpStatusCode error)
        {
            ErrorCode = error;
        }

        public CustomException(HttpStatusCode error, string message) : base (message)
        {
            ErrorCode = error;
        }
    }
}
