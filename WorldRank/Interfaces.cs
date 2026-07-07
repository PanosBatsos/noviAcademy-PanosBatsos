using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WorldRank
{
    public interface IPlayer
    {
        public string Name { get; }
        public Guid Id { get; }
        public int Score { set; get; }

    }

    public interface IWalletRepository
    {
        public void AddWallet(Wallet wallet, Guid playerId);
        public List<Wallet> GetByPlayer(Guid playerId);

    }

    public interface IPlayerRepository
    {
        public void AddPlayer(Player p);
        public Player FindPlayer(Guid playerId);
        public void DeletePlayer(Guid playerId);

        public IEnumerable<IGrouping<int, Player>> GrouPlayersByScore();

    }

    public class InMemoryWalletRepository : IWalletRepository
    {
        public List<Player> Players { get; }

        public InMemoryWalletRepository(List<Player> players)
        {
            this.Players = players;
        }

        public void AddWallet(Wallet wallet, Guid playerId)
        {
            Player? foundPlayer = Players.FirstOrDefault(p => p.Id == playerId);

            if (foundPlayer != null)
            {
                foundPlayer.Wallets.Add(wallet.Currency, wallet);
            } else
            {
                throw new ArgumentNullException("This player does not exist");
            }
        }
        
        public List<Wallet> GetByPlayer(Guid playerId)
        {
            Player? foundPlayer = Players.FirstOrDefault(p => p.Id == playerId);

            if (foundPlayer != null)
            {
                List<Wallet> wallets = foundPlayer.Wallets.Values.ToList();
                return wallets;
            }
            else
            {
                throw new ArgumentNullException("This player does not exist");
            }
        }

  
    }

    public class InMemoryPlayerRepository : IPlayerRepository
    {
        public List<Player> Players { get; }

        public InMemoryPlayerRepository (List<Player> players)
        {
            Players = players;
        }
        public void AddPlayer(Player p)
        {
            Players.Add(p);
        }

        public void DeletePlayer(Guid playerId)
        {
            Player? foundPlayer = Players.FirstOrDefault(p => p.Id == playerId);

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
            return Players.GroupBy(p =>  p.Score);
        }
    }
}