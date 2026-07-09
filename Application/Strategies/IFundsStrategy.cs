using Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Strategies
{
    public interface IFundsStrategy
    {
        FundsOperation Operation { get; }
        public void Execute(Wallet wallet, decimal amount);
    }
}
