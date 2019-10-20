using System;
using System.Runtime.Serialization;

namespace MarcusJ
{
    [Serializable]
    public class IsNotValidValueException : ArgumentOutOfRangeException
    {
        public IsNotValidValueException()
        {
        }

        public IsNotValidValueException(string message) : base(message)
        {
        }

        public IsNotValidValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsNotValidValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}