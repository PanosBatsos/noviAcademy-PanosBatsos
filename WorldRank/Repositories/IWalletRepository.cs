using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Models;

namespace WorldRank.Repositories
{
    public interface IWalletRepository
    {
        public void AddWallet(Wallet wallet, Guid playerId);
        public List<Wallet> GetByPlayer(Guid playerId);

    }
}
