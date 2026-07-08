using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;
using WorldRank.Exceptions;
using WorldRank.Interfaces;

namespace WorldRank.Models
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
            Wallets = new Dictionary<Currency, Wallet>();
        }

        public string Name { get; private set; }
        public Guid Id { get; }
        public int Score { set; get; }

        public Dictionary<Currency, Wallet> Wallets { get;  set; }
        
        public void AddScore(int score)
        {
            if (score < 0)
            {
                throw new NegativeScoreException ("Score cannot be negative");
            }

            Score += score;
        }

        public override string ToString()
        {
            return $"Name: {Name} | Score: {Score}";
        }


    }
}
