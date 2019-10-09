using System;
namespace APIGateway.Models
{
    public class ApiException : Exception
    {
        public ApiException(ApiErrorResult error) : base(error.Message)
        {

        }

        public ApiException(string message) : base(message)
        {

        }
    }
}