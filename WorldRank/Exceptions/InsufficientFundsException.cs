using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Exceptions
{
    public class InsufficientFundsException : WalletException
    {
        public InsufficientFundsException() { }
        public InsufficientFundsException(string message) : base(message) {}
        public InsufficientFundsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
