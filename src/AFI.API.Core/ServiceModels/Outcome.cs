using System;
using System.Net;

namespace AFI.API.Core.ServiceModels
{
    public class Outcome<T> 
    {
        private string _errorMessage = "An error occurred";
        private HttpStatusCode _statuscode;
        private T _result;

        public Outcome(string errorMessage, HttpStatusCode statusCode, T result)
        {
            this._errorMessage = errorMessage;
            _statuscode = statusCode;
            Result = result;
        }

        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
        }

        public bool Successful
        {
            get
            {
                return _statuscode == HttpStatusCode.OK;
            }
        }
        public HttpStatusCode StatusCode
        {
            get
            {
                return _statuscode ;
            }
        }

        public T Result
        {
            get
            {
                return this._result;
            }
            private set
            {
                this._result = value;
                if ((object)value == null || string.IsNullOrWhiteSpace(this._errorMessage) || !this._errorMessage.Equals("An error occurred", StringComparison.InvariantCultureIgnoreCase))
                    return;
                this._errorMessage = string.Empty;
            }
        }
    }
}
