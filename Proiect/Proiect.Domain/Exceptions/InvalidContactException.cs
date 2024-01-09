using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain.Exceptions
{
    internal class InvalidContactException : Exception
    {
        public InvalidContactException() { }
        public InvalidContactException(string message) : base(message) { }
        public InvalidContactException(string message,  Exception innerException) : base(message, innerException) { }
        protected InvalidContactException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
