using System.Xml.Linq;
using WorldRank;
using WorldRank.Enums;
using WorldRank.Models;
using WorldRank.Repositories;
using NLog;
using WorldRank.Exceptions;

var logger = LogManager.GetCurrentClassLogger();

logger.Info("App started");



List<Player> players = new List<Player>();

IWalletRepository walletRepository = new InMemoryWalletRepository(players);
IPlayerRepository playerRepository = new InMemoryPlayerRepository(players);

while (true)
{
    DisplayMenu();
    string? choice = Console.ReadLine();
    Console.WriteLine();

    logger.Debug("User selected menu option: {menuChoice}", choice);

    switch (choice)
    {
        case "1":
            AddPlayer();
            break;
        case "2":
            DisplayList(); 
            break;
        case "3":
            FindByName();
            break;
        case "4":
            logger.Info("Application shutting down gracefully by user request.");
            Console.WriteLine("Exiting...");
            return;
        case "5":
            LinkWallet();
            break;
        case "6":
            ShowPlayersWallets();
            break;
        case "7":
            RemovePlayerFromList();
            break;
        case "8":
            GroupPlayersByScore();
            break;
        default:
            logger.Warn("Invalid menu choice entered: {MenuChoice}", choice);
            Console.WriteLine("Not valid choice try again...");
            break;
    }
}


void DisplayMenu()
{
    Console.WriteLine("\n=== Menu ===");
    Console.WriteLine("1. (Add player)");
    Console.WriteLine("2. (Display list)");
    Console.WriteLine("3. (Find by name)");
    Console.WriteLine("4. (Exit)");
    Console.WriteLine("5. (Link Player to Wallet)");
    Console.WriteLine("6. (Display Player's wallets)");
    Console.WriteLine("7. (Remove Player from List)");
    Console.WriteLine("8. (Group Players by Score)");
    Console.Write(" Select (1-8): ");
} 

void AddPlayer()
{
    Console.Write("Player's Name: ");
    string? name = Console.ReadLine();

    try
    {
        Player player = new Player(name ?? "");
        Console.Write("Give score: ");
        string? scoreInput = Console.ReadLine();
        if (int.TryParse(scoreInput, out int score))
        {
            player.AddScore(score);
            playerRepository.AddPlayer(player);
            logger.Info("Player {PlayerName} added successfully with ID: {PlayerId} and Score: {Score}", player.Name, player.Id, score);
            Console.WriteLine("Player added");
        }
        else
        {
            logger.Warn("Failed to add player {PlayerName}: Score input '{ScoreInput}' is not a valid integer.", name, scoreInput);
            Console.WriteLine("Score must be an integer");
        }
    }
    catch (ArgumentException ae)
    {
        logger.Error(ae, "Failed to create player: Name parameter was empty or null.");
        Console.WriteLine("Name cannot be empty");
    }
    catch (NegativeScoreException nse)
    {
        logger.Error(nse, "Failed to set score for player {name}: Attempted to add a negative score.", name);
        Console.WriteLine("Score cannot be negative");
    }

}

void DisplayList()
{
    if (players.Count == 0)
    {
        logger.Debug("DisplayList requested but player count is 0.");
        Console.WriteLine("Count of players list is 0");
    } else
    {
        logger.Info("Displaying list of {PlayerCount} players.", players.Count);
        foreach (Player player in players)
        {
            Console.WriteLine(player.ToString());
        }
    }
}

void FindByName()
{
    Player? player = FindPlayerByName();

    if (player != null)
    {
        logger.Info("Player {PlayerName} found successfully via search.", player.Name);
        Console.WriteLine("Player found: " + player.ToString());
    } else
    {
        logger.Debug("Search completed but no player was found.");
        Console.WriteLine("Player not found");
    }
}

void LinkWallet()
{
    Player? player = FindPlayerByName();
    if (player == null)
    {
        logger.Warn("LinkWallet aborted: Provided player name was not found.");
        Console.WriteLine("Player not found");
        return;
    }

    Console.WriteLine("Select Currency (1: USD, 2: EUR): ");
    string? currChoice = Console.ReadLine();
    Currency selectedCurrency;
    if (currChoice == "1")
    {
        selectedCurrency = Currency.USD;
    }
    else if (currChoice == "2")
    {
        selectedCurrency = Currency.EUR;
    } else
    {
        logger.Warn("LinkWallet aborted: User selected invalid currency choice {CurrencyChoice} for player {PlayerId}.", currChoice, player.Id);
        Console.WriteLine("Not valid choice... Try again");
        return;
    }

    Wallet wallet = new Wallet(selectedCurrency);
    walletRepository.AddWallet(wallet, player.Id);
    logger.Info("Successfully linked a {Currency} wallet to player ID: {PlayerId}.", selectedCurrency, player.Id);
}

void ShowPlayersWallets()
{
    Player? player = FindPlayerByName(); 

    if (player == null)
    {
        logger.Warn("ShowPlayersWallets aborted: Provided player name was not found.");
        Console.WriteLine("Player not found");
        return;
    }

    List<Wallet> userWallets = walletRepository.GetByPlayer(player.Id);
    if (userWallets.Count == 0)
    {
        logger.Info("Player {PlayerName} (ID: {PlayerId}) currently holds no wallets.", player.Name, player.Id);
        Console.WriteLine("This player has no wallets.");
        return;
    }

    logger.Info("Displaying {WalletCount} wallet(s) for player {PlayerName} (ID: {PlayerId}).", userWallets.Count, player.Name, player.Id);
    Console.WriteLine($"--- Wallets for {player.Name} ---");
    foreach (Wallet wallet in userWallets)
    {
        Console.WriteLine($"- Currency: {wallet.Currency} | Balance: {wallet.Balance} | Blocked: {wallet.isBlocked}");
    }
}

void RemovePlayerFromList()
{
    
    Player? player = FindPlayerByName();

    if (player == null)
    {
        logger.Warn("RemovePlayerFromList aborted: Provided player name was not found.");
        Console.WriteLine("Player does not exist");
        return;
    }

    playerRepository.DeletePlayer(player.Id);
    logger.Info("Player {PlayerName} with ID: {PlayerId} was successfully removed from the system.", player.Name, player.Id);
    Console.WriteLine("Player removed from List succesfully");
}

void GroupPlayersByScore()
{
    IEnumerable<IGrouping<int, Player>> playersGroup = playerRepository.GrouPlayersByScore();

    if (!playersGroup.Any())
    {
        logger.Debug("GroupPlayersByScore executed, but no players are available to group.");
        Console.WriteLine("No players to group");
        return;
    }

    logger.Info("Grouped {PlayerCount} players by score successfully.", players.Count);
    foreach (IGrouping<int, Player> group in playersGroup)
    {
        Console.WriteLine("Score: " + group.Key);

        foreach (Player player in group)
        {
            Console.WriteLine(player.ToString());
        }
    }
}

Player? FindPlayerByName()
{
    Console.Write("Enter Name: ");
    string? name = Console.ReadLine();
    logger.Debug("Searching for player with exact name: {SearchName}", name);

    Player? player = players.FirstOrDefault(p => p.Name.Equals(name ?? "", StringComparison.OrdinalIgnoreCase));
    return player;
}