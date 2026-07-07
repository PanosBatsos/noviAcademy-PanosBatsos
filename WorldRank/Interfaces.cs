using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public interface IPlayer
    {
        public string Name { get; }
        public Guid Id { get; }
        public int Score { set; get; }
    }
}
