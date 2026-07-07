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
        }

        public string Name { get; private set; }
        public Guid Id { get; }
        public int Score { set; get; }
        

        
        public void AddScore(int score)
        {
            if (score < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(score), "Score cannot be negative");
            }

            _score += score;
        }

        public override string ToString()
        {
            return $"Name: {_name} | Score: {_score}";
        }
    }
}
