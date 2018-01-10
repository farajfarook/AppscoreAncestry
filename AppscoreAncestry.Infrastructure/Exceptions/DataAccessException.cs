using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Domain.Exceptions;

namespace AppscoreAncestry.Infrastructure.Exceptions
{
    public class DataAccessException : DomainException
    {
        public DataAccessException()
        {
        }

        public DataAccessException(string message) : base(message)
        {
        }

        public DataAccessException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
