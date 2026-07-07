using WorldRank;

List<Player> players = new List<Player>();


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
    Console.Write(" Select (1-4): ");
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
        players.Add(player);
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

