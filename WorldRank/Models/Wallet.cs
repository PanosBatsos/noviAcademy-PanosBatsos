using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;
using WorldRank.Exceptions;

namespace WorldRank.Models
{
    public class Wallet
    {
        public decimal Balance { private set; get; }
        public Currency Currency { set; get; }
        public bool isBlocked { set; get; }

        public Wallet(Currency currency) 
        {
            Balance = 0;
            isBlocked = false;
            this.Currency = currency;
        }

        public void AddBalance(decimal amount) 
        {
            if (amount < 0)
            {
                throw new InsufficientFundsException("Ammount cannot be negative");
            }

            Balance = amount;
        }
    }
}
