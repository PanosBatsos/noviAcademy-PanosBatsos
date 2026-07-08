using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Exceptions
{
    public class NegativeScoreException : PlayerException
    {
        public NegativeScoreException() { }
        public NegativeScoreException(string message) : base(message) { }
        public NegativeScoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}
