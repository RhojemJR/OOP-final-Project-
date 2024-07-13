class ColorGame
{
	public void Play1(Player user, Leaderboard leaderboard)
	{
	    string[] Colors = {"RED", "YELLOW", "GREEN", "WHITE", "BLUE", "MAGENTA"};
	    ConsoleColor[] Consolecolor = {ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Magenta};
    	bool playagain1 = true;
		while(playagain1)
		{
			//BETTING SYSTEM
		    List<int> allbets = new List<int>(              );
		    List<string> allchosenColors = new List<string>();
			int maxCounter = 6;
			
			Console.WriteLine("Enter a number from '1' to '6' only.");
			Console.Write("Enter number of bets:"                   );
			int NumofBets  = Convert.ToByte(Console.ReadLine()      );
			Console.Clear(                                          );
			
		    int[] bets = new int [NumofBets];
			string[] chosenColors = new string[NumofBets];
			
			if (NumofBets <= 0 || NumofBets > maxCounter)
			{
				Console.WriteLine("Try again! Enter a number from '1' to '6' only.");
				continue;
			}
			Console.Clear();
			bool playagain = true;
			while(playagain)
			{
				allbets.Clear();
				allchosenColors.Clear();
				for(int i = 0; i < NumofBets; i++)
				{
					while(true)
					{
						Console.WriteLine("----------------------------------");
						Console.WriteLine($"Current Balance:P{user.balance}");
						Console.WriteLine("----------------------------------");
						Console.Write($"Enter bet {i + 1} (P100 - P10,000):P" );
						int bet = Convert.ToInt16(Console.ReadLine()          );
						if (bet >= 100 && bet <= 10000 && bet <= user.balance)
						{
							bets[i] = bet;
							allbets.Add(bet);
							user.balance -= bet;
							break;
						}
						else
						{
							Console.WriteLine("Invalid input! P100 - P10,000.");
						}
					}
					bool Valid = false;
					while (!Valid)
					{
						Console.WriteLine("-------------------------------------------------");
						for (int j = 0; j < Colors.Length; j++)
						{
							Console.ForegroundColor = Consolecolor[j];
							Console.Write($"| {Colors[j]} "                               );
						}
						Console.ResetColor(                                               );
						Console.WriteLine("|"                                             );
						Console.WriteLine("-------------------------------------------------");
						Console.Write($"Choose a color for bet {i + 1}:"                  );
						string Colorchoice = Console.ReadLine().ToUpper(                  );
						foreach (string ValidColors in Colors)
						{
							if (Colorchoice == ValidColors)
							{
								chosenColors[i] = Colorchoice;
								allchosenColors.Add(Colorchoice);
								Valid = true;
								break;
							}
						}
						if (!Valid)
						{
							Console.WriteLine("Invalid input!");
						}
						Console.Clear();
					}
				}
				Console.WriteLine("------------------------------------");
				Console.WriteLine("|      Bets:       |    Colors:    |");
				Console.WriteLine("------------------------------------");
				for (int i = 0; i < allbets.Count; i++)
				{
					Console.WriteLine($"Bet {i + 1}:P{allbets[i]}"      );
					Console.WriteLine(                                  );
				}
				Console.WriteLine("------------------------------------");
				for (int i = 0; i < allchosenColors.Count; i++)
				{
					Console.ForegroundColor = Consolecolor[Array.IndexOf(Colors, allchosenColors[i])];
					Console.WriteLine($"Color {i + 1}:{allchosenColors[i]}"                         );
					Console.WriteLine(                                                              );
				}
				Console.ResetColor();
				Console.WriteLine("------------------------------------");
				Console.Write("1 - Continue / 2 - Restart bets:"        );
				string response = Console.ReadLine(                     );
				Console.WriteLine("------------------------------------");
				if (response != "1")
				{
					Console.WriteLine("resetting game...");
					Console.ReadKey(                     );
					Console.Clear(                       );
					playagain1 = true;
				}
				else
				{
					//GAME RESULT
					playagain = false;
					Console.WriteLine("ROLLING...");
					Console.ReadKey(              );
					Console.Clear(                );
					
					Random random = new Random(    );
					int randomnum = random.Next(1,5);
					(int RandomIndex1, int RandomIndex2, int RandomIndex3) = (random.Next(0, Colors.Length), random.Next(0, Colors.Length), random.Next(0, Colors.Length));
					DisplayWinningColors(randomnum, RandomIndex1, RandomIndex2, RandomIndex3, Colors, Consolecolor);
					
					Console.WriteLine("--------------------------------------------------");
					Console.WriteLine("                    GAME RESULT                   ");
					Console.WriteLine("--------------------------------------------------");
					int TotalWinnings = CalculateWinnings(allbets, allchosenColors, RandomIndex1, RandomIndex2, RandomIndex3, Colors, Consolecolor, user, leaderboard);
					Console.WriteLine($"{user.Name}'s total winnings: P{TotalWinnings}"   );
					Console.WriteLine($"Your current balance:P{user.balance += TotalWinnings}");
					Console.WriteLine("--------------------------------------------------");	
                    if (user.balance == 0)
                    {
                        Console.WriteLine("You have run out of balance!");
                        leaderboard.DisplayLeaderboard(                 );
                        return;
                    }
					Console.WriteLine(                             );
					Console.Write("Do you want to play again? Y/N:");
					string answer = Console.ReadLine().ToUpper(    );
					playagain1 = answer == "Y";
					if (playagain1)
					{
						Console.Clear();
					}
					else
					{ 
						leaderboard.DisplayLeaderboard();
					}
				}
			}
		}
	}

	private void DisplayWinningColors(int randomnum, int RandomIndex1, int RandomIndex2, int RandomIndex3, string[] Colors, ConsoleColor[] Consolecolor)
	{
		Console.WriteLine("=========================");
		Console.WriteLine("      WINNING COLORS     ");
		Console.WriteLine("=========================");
			
		switch (randomnum)
		{
		case 1:
		 Console.ForegroundColor = Consolecolor[RandomIndex3];
		 Console.WriteLine($":{Colors[RandomIndex3]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex2];
		 Console.WriteLine($":{Colors[RandomIndex2]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex1];
		 Console.WriteLine($":{Colors[RandomIndex1]}"       );
		break;
		case 2:
		 Console.ForegroundColor = Consolecolor[RandomIndex1];
		 Console.WriteLine($":{Colors[RandomIndex1]}"       );
		 Console.WriteLine($":{Colors[RandomIndex1]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex3];
		 Console.WriteLine($":{Colors[RandomIndex3]}"       );
		break;
		case 3:
		 Console.ForegroundColor = Consolecolor[RandomIndex1];
		 Console.WriteLine($":{Colors[RandomIndex1]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex2];
		 Console.WriteLine($":{Colors[RandomIndex2]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex3];
		 Console.WriteLine($":{Colors[RandomIndex3]}"       );
		break;
		case 4:
		 Console.ForegroundColor = Consolecolor[RandomIndex3];
		 Console.WriteLine($":{Colors[RandomIndex3]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex2];
		 Console.WriteLine($":{Colors[RandomIndex2]}"       );
		 Console.ForegroundColor = Consolecolor[RandomIndex3];
		 Console.WriteLine($":{Colors[RandomIndex3]}"       );
		break;
		}
			
		Console.ResetColor(                          );
		Console.WriteLine("=========================");
		Console.WriteLine(                           );
	}

	private int CalculateWinnings(List<int> allbets, List<string> allchosenColors, int RandomIndex1, int RandomIndex2, int RandomIndex3, string[] Colors, ConsoleColor[] Consolecolor, Player player, Leaderboard leaderboard)
	{
		int TotalWinnings = 0;

		for (int i = 0; i < allbets.Count; i++)
		{
			int matches = 0;
			if (allchosenColors[i] == Colors[RandomIndex1]) matches++;
			if (allchosenColors[i] == Colors[RandomIndex2]) matches++;
			if (allchosenColors[i] == Colors[RandomIndex3]) matches++;
					
			int winnings = 0;
		    switch (matches)
			{
			case 1:
			 winnings = allbets[i] + allbets[i];
			break;
			case 2:
			 winnings = allbets[i] * 2;
			break;
			case 3:
			 winnings = allbets[i] * 3;
			break;
			}
			TotalWinnings += winnings;
			Console.ForegroundColor = Consolecolor[Array.IndexOf(Colors, allchosenColors[i])];
			Console.WriteLine($"Bet {i + 1}: P{allbets[i]}, Color: {allchosenColors[i]}, Matches: {matches}, Winnings: P{winnings}");
			Console.ResetColor();
		}
		leaderboard.UpdateScore(player, TotalWinnings);
		return TotalWinnings;
	}
}