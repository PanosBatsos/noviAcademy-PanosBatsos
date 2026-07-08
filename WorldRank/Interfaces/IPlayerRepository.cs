using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Models;

namespace WorldRank.Interfaces
{
    public interface IPlayerRepository
    {
        public void AddPlayer(Player p);
        public Player FindPlayer(Guid playerId);
        public void DeletePlayer(Guid playerId);

        public IEnumerable<IGrouping<int, Player>> GrouPlayersByScore();

    }
}
