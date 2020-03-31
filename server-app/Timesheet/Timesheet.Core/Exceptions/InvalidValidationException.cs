using System;

namespace Timesheet.Core.Exceptions
{
    public class InvalidValidationException : Exception
    {
        public string PropertyName { get; }

        public InvalidValidationException() : base() { }

        public InvalidValidationException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

        public InvalidValidationException(string propertyName, string message, Exception innerException) : base(message, innerException)
        {
            PropertyName = propertyName;
        }
    }
}
