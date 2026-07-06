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
    }
}


void DisplayMenu()
{
    Console.WriteLine("\n=== Μενού Επιλογών ===");
    Console.WriteLine("1. (Add player)");
    Console.WriteLine("2. (List players)");
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

