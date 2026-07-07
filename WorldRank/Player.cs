using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class Player : IPlayer
    {
        

        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Id = Guid.NewGuid();
            Name = name;
            Score = 0;
            wallets = new Dictionary<Currency, Wallet> ();
            wallets.Add(Currency.EUR, new Wallet(Currency.EUR));
            wallets.Add(Currency.USD, new Wallet(Currency.USD));
        }

        public string Name { get; private set; }
        public Guid Id { get; }
        public int Score { set; get; }

        public Dictionary<Currency, Wallet> wallets;
        
        public void AddScore(int score)
        {
            if (score < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(score), "Score cannot be negative");
            }

            Score += score;
        }

        public override string ToString()
        {
            return $"Name: {Name} | Score: {Score}";
        }


    }
}
