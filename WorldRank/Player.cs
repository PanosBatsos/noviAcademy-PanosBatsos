using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class Player
    {
        private Guid _id;
        private string _name;
        private int _score;

        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            _id = Guid.NewGuid();
            _name = name;
            _score = 0;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

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
