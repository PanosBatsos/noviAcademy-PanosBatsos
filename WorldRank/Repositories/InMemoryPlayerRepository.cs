using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using WorldRank.Interfaces;
using WorldRank.Models;

namespace WorldRank.Repositories
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        public List<Player> Players { get; }

        public InMemoryPlayerRepository(List<Player> players)
        {
            Players = players;
        }
        public void AddPlayer(Player p)
        {
            Players.Add(p);
        }

        public void DeletePlayer(Guid playerId)
        {
            Player? foundPlayer = FindPlayer(playerId);

            if (foundPlayer != null)
            {
                Players.Remove(foundPlayer);
            }
            else
            {
                throw new ArgumentNullException("This player does not exist");
            }
        }

        public Player FindPlayer(Guid playerId)
        {
            Player? foundPlayer = Players.FirstOrDefault(p => p.Id == playerId);

            if (foundPlayer != null)
            {
                return foundPlayer;
            }
            else
            {
                throw new ArgumentNullException("This player does not exist");
            }
        }

        public IEnumerable<IGrouping<int, Player>> GrouPlayersByScore()
        {
            return Players.GroupBy(p => p.Score);
        }
    }
}
