using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Models;

namespace WorldRank.Repositories
{
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

            if (foundPlayer == null)
            {
                Console.WriteLine("This player does not exist");
                return;
            }

            if (foundPlayer.Wallets.ContainsKey(wallet.Currency))
            {
                Console.WriteLine("Player is already linked to this wallet");
                return;
            }
            else
            {
                foundPlayer.Wallets.Add(wallet.Currency, wallet);
                Console.WriteLine("Wallet linked to player succesfully");
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
}
