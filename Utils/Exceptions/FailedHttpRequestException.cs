using System;
using System.Net;

namespace RickAndMortyAPI.Utils.Exceptions
{
    public class FailedHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public FailedHttpRequestException(string message, HttpStatusCode responseStatusCode) : base(message)
        {
            StatusCode = responseStatusCode;
        }
    }
}