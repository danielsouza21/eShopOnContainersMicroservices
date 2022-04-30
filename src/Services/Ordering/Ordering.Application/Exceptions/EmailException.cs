using System;

namespace Ordering.Application.Exceptions
{
    public class EmailException : ApplicationException
    {
        public EmailException(string message) : base(message)
        {
        }
    }
}
