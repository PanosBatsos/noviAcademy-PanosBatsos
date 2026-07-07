using WorldRank;

List<Player> players = new List<Player>();

IWalletRepository walletRepository = new InMemoryWalletRepository(players);
IPlayerRepository playerRepository = new InMemoryPlayerRepository(players);

while (true)
{
    DisplayMenu();
    string? choice = Console.ReadLine();
    Console.WriteLine();
    

    switch (choice)
    {
        case "1":
            AddPlayer();
            break;
        case "2":
            DisplayList(); 
            break;
        case "3":
            Console.Write("Give player's name to search: ");
            string? searchName = Console.ReadLine();
            FindByName(searchName ?? "");
            break;
        case "4":
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
    Player player = new Player(name ?? "");

    Console.Write("Give score: ");
    if (int.TryParse(Console.ReadLine(), out int score))
    {
        player.AddScore(score);
        playerRepository.AddPlayer(player);
        Console.WriteLine("Player added");
    }
    else
    {
        Console.WriteLine("Score must be an integer");
    }
   
}

void DisplayList()
{
    if (players.Count == 0)
    {
        Console.WriteLine("Count of players list is 0");
    } else
    {
        foreach (Player player in players)
        {
            Console.WriteLine(player.ToString());
        }
    }
}

void FindByName(string name)
{
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name must not be empty");
        return;
    }

    Player? player = players.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    if (player != null)
    {
        Console.WriteLine("Player found: " + player.ToString());
    } else
    {
        Console.WriteLine("Player with name " + name + " not found");
    }
}

void LinkWallet()
{
    Console.Write("Enter Name: ");
    string? name = Console.ReadLine();
    Player? player = players.FirstOrDefault(p => p.Name.Equals(name ?? ""));

    if (player == null)
    {
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
        Console.WriteLine("Not valid choice... Try again");
        return;
    }

    Wallet wallet = new Wallet(selectedCurrency);
    walletRepository.AddWallet(wallet, player.Id);

    Console.WriteLine("Wallet succesfully linked to player: " + player.Name);
}

void ShowPlayersWallets()
{
    Console.Write("Enter Player's name to show wallets: ");
    string? name = Console.ReadLine();
    Player? player = players.FirstOrDefault(p => p.Name.Equals(name ?? ""));

    if (player == null)
    {
        Console.WriteLine("Player not found");
        return;
    }

    List<Wallet> userWallets = walletRepository.GetByPlayer(player.Id);
    if (userWallets.Count == 0)
    {
        Console.WriteLine("This player has no wallets.");
        return;
    }

    Console.WriteLine($"--- Wallets for {player.Name} ---");
    foreach (Wallet wallet in userWallets)
    {
        Console.WriteLine($"- Currency: {wallet.Currency} | Balance: {wallet.Balance} | Blocked: {wallet.isBlocked}");
    }
}

void RemovePlayerFromList()
{
    Console.Write("Enter Player's name: ");
    string? name = Console.ReadLine();
    Player? player = players.FirstOrDefault(p => p.Name.Equals(name ?? ""));

    if (player == null)
    {
        Console.WriteLine("Player does not exist");
        return;
    }

    playerRepository.DeletePlayer(player.Id);
    Console.WriteLine("Player removed from List succesfully");
}

void GroupPlayersByScore()
{
    IEnumerable<IGrouping<int, Player>> playersGroup = playerRepository.GrouPlayersByScore();

    if (!playersGroup.Any())
    {
        Console.WriteLine("No players to group");
        return;
    }

    foreach (IGrouping<int, Player> group in playersGroup)
    {
        Console.WriteLine("Score: " + group.Key);

        foreach (Player player in group)
        {
            Console.WriteLine(player.ToString());
        }
    }
}