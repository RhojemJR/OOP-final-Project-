class DropBall
{
	private string[] Colors = { "RED", "YELLOW", "GREEN", "DARK GREEN", "WHITE", "BLUE", "DARK BLUE", "MAGENTA" };
    private ConsoleColor[] ConsoleColors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Magenta };
    private List<int> allBets = new List<int>();
    private List<string> allChosenColors = new List<string>();
    private int maxCounter = 8;
	
	public void play3(Player user, Leaderboard leaderboard)
	{
		bool playagain = true;
		while(playagain)
		{
			Console.WriteLine("Try again! Enter a number from '1' to '8' only.");
			Console.Write("Enter number of bets:"                              );
			int NumofBets  = Convert.ToByte(Console.ReadLine()                 );
			Console.Clear(                                                     );

		    int[] bets = new int [NumofBets];
			string[] chosenColors = new string[NumofBets];
			
			if (NumofBets <= 0 || NumofBets > maxCounter)
			{
				Console.WriteLine("Try again! Enter a number from '1' to '8' only.");
				continue;
			}
			Console.Clear(        );
			allBets.Clear(        );
			allChosenColors.Clear();
			
			//PlayerBets&Colors
			PlayerBets(NumofBets, bets, user    );
			PlayerColors(NumofBets, chosenColors);
			
			//PlayerResult
			DisplayResult();
						
			//game results
			Console.WriteLine("ROLLING...");
			Console.ReadKey(              );
			Console.Clear(                );
			Random random = new Random(   );
			int randomWinningIndex = random.Next(0, Colors.Length);
			string randomWinningColor = Colors[randomWinningIndex];
			Console.WriteLine("---------------------------------------------------");
			Console.WriteLine("                     GAME RESULT               "    );
			Console.WriteLine("---------------------------------------------------");
			Console.ForegroundColor = ConsoleColors[randomWinningIndex];
			Console.WriteLine($"Winning Color:{randomWinningColor}"                );
			Console.ResetColor(                                                    );
			Console.WriteLine("---------------------------------------------------");
			int TotalWinnings = CalculateTotalWinnings(randomWinningColor, user, leaderboard);
			Console.WriteLine($"{user.Name}'s total winnings:P{TotalWinnings}"     );	
			Console.WriteLine($"Your current balance:P{user.balance += TotalWinnings}");
			Console.WriteLine("---------------------------------------------------");	
			if (user.balance == 0)
			{
				Console.WriteLine("You have run out of balance!");
				leaderboard.DisplayLeaderboard(                 );
				return;
			}
			Console.Write("Do you want to play again? Y/N:");
			string answer = Console.ReadLine().ToUpper(    ); 
			playagain = answer == "Y";
			if (playagain)
			{
				Console.Clear();
			}
			else 
			{
				leaderboard.DisplayLeaderboard();
			}
		}
	}
	
	private void PlayerBets(int NumofBets, int[] bets, Player player)
	{
		Console.WriteLine("-----------------");
		Console.WriteLine("      TAYA     "  );
		Console.WriteLine("-----------------");
		Console.WriteLine("   P20 = P100"    );
		Console.WriteLine("-----------------");
		Console.WriteLine("   P30 = P150"    );
		Console.WriteLine("-----------------");
		Console.WriteLine("   P40 = P200"    );
		Console.WriteLine("-----------------");
		Console.WriteLine("   P50 = P250"    );
		Console.WriteLine("-----------------");
		Console.WriteLine("   P100 = P500"   );
		Console.WriteLine("-----------------");
		Console.WriteLine("   P200 = P1000"  );
		Console.WriteLine("-----------------");
		for(int i = 0; i < NumofBets; i++)
		{
			while(true)
			{
		        Console.WriteLine("----------------------------------");
				Console.WriteLine($"Current Balance:P{player.balance}");
				Console.WriteLine("----------------------------------");
				Console.Write($"Enter bet {i + 1} (P20 - P200):P"     );
				int bet = Convert.ToByte(Console.ReadLine()           );
				if (bet >= 20 && bet <= 200 && bet <= player.balance) 
				{
					bets[i] = bet;
					allBets.Add(bet);
					player.balance -= bet;
					break;
				}
				else
				{
					Console.WriteLine("Invalid input! P20 - P200.");
				}
			}
		}
		Console.Clear();
	}
	private void PlayerColors(int NumofBets, string[] chosenColors)
	{
		for (int i = 0; i < NumofBets; i++)
        {
            bool Valid = false;
            while (!Valid)
            {		
				Console.WriteLine("--------------------------------------------------------------------------");
				for (int j = 0; j < Colors.Length; j++)
				{
					Console.ForegroundColor = ConsoleColors[j];
					Console.Write($"| {Colors[j]} "                                                           );
				}
				Console.ResetColor(                                                                           );
				Console.WriteLine("|"                                                                         );
				Console.WriteLine("--------------------------------------------------------------------------");
				Console.Write($"Choose a color for bet {i + 1}:"                                              );
				string Colorchoice = Console.ReadLine().ToUpper(                                              );
				Console.ResetColor(                                                                           );
				foreach (string ValidColors in Colors)
				{
					if (Colorchoice == ValidColors)
					{
						chosenColors[i] = Colorchoice;
						allChosenColors.Add(Colorchoice);
						Valid = true;
						break;
					}
				}
				if (!Valid)
				{
					Console.WriteLine("Invalid input!");
					Console.Clear();
				}
			}
		}
		Console.Clear();
	}
	
	private void DisplayResult()
	{
		Console.WriteLine("------------------------------------");
		Console.WriteLine("|      Bets:       |    Colors:    |");
		Console.WriteLine("------------------------------------");
		for (int i = 0; i < allBets.Count; i++)
		{
			Console.WriteLine($"Bet {i + 1}:P{allBets[i]}");
			Console.WriteLine();
		}
		Console.WriteLine("------------------------------------");
		for (int i = 0; i < allChosenColors.Count; i++)
		{
			Console.ForegroundColor = ConsoleColors[Array.IndexOf(Colors, allChosenColors[i])];
			Console.WriteLine($"Color {i + 1}:{allChosenColors[i]}");
			Console.WriteLine();
			Console.ResetColor();
		}
	}
    private int CalculateTotalWinnings (string randomWinningColor, Player player, Leaderboard leaderboard) 
	{
		int TotalWinnings = 0;
		for (int i = 0; i < allChosenColors.Count; i++)
		{
			int matches = 0;
			int Winnings = 0;
			if (allChosenColors[i] == randomWinningColor) 
			{
				matches = 1;
				if (allBets[i] == 20)
				{
					Winnings = 100;
				}
				else if  (allBets[i] == 30)
				{
					Winnings = 150;
				}
				else if  (allBets[i] == 40)
				{
					Winnings = 200;
				}
				else if  (allBets[i] == 50)
				{
					Winnings = 250;
				}	
                else if  (allBets[i] == 100)
				{
					Winnings = 500;
					
				}
                else if  (allBets[i] == 200)
				{
					Winnings = 1000;
				}						
			}
			else 
			{
				matches = 0;
				Winnings = 0;
			}
		    TotalWinnings += Winnings;
			Console.ForegroundColor = ConsoleColors[Array.IndexOf(Colors, allChosenColors[i])];
			Console.WriteLine($"Bet {i + 1}: P{allBets[i]}, Color: {allChosenColors[i]}, Matches: {matches}, Winnings:P{Winnings}");
			Console.ResetColor();
		}
		leaderboard.UpdateScore(player, TotalWinnings);
		return TotalWinnings;
	}
}