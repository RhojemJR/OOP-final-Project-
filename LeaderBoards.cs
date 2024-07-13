public class Player
{
	public string Name {get; set;}
	public int Score {get; set;}
	public int balance {get; set;}
	
	// layer's name, score, and balance 
	public Player(string name)
	{
		Name = name; 
		Score = 0;
		balance = 500;
	}
}
public class Leaderboard 
{
	//Leaderboard class 
	private List<Player> players = new List<Player>();
	//adds a player to the game
	public void AddPlayer(Player player)
	{
		players.Add(player);
	}
	public void UpdateScore(Player player, int score)
	{
		//updates player winnings 
		var p = players.FirstOrDefault(pl => pl.Name == player.Name); 
		if (p != null)
		{
			p.Score += score;
		}
	}
	//displays the leaderbaords all throughout the game
	public List<Player> GetPlayers()
	{
		return players;
	}
	public void DisplayLeaderboard()
	{
		Console.WriteLine("================================");
        Console.WriteLine("          LEADERBOARDS          ");
        Console.WriteLine("================================");
        foreach (var p in GetPlayers())
        {
        Console.WriteLine($"{p.Name}.....................P{p.Score}");
        }
        Console.WriteLine("================================");
	}
}

