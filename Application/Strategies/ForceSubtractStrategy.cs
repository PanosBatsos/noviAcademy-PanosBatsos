using Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Strategies
{
    public class ForceSubtractStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.ForceSubtract;

        public void Execute(Wallet wallet, decimal amount)
        {
            wallet.ForceWithdraw(amount);
        }
    }
}
