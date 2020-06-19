using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Exceptions
{
    public class LoginAlreadyTakenException : Exception
    {
        public LoginAlreadyTakenException()
        {
        }

        public LoginAlreadyTakenException(string message) : base(message)
        {
        }

        public LoginAlreadyTakenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
