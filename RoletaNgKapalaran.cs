class RoletaNgKapalaran
{
	public void Play2(Player user, Leaderboard leaderboard)
	{
		string[] number = {"1", "2", "3", "4", "5", "6", "7", "8"};
		ConsoleColor[] Consolecolor = {ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Magenta, ConsoleColor.Cyan};
		bool playagain = true;
		while (playagain)
		{
			//BETTING SYSTEM
			List<int> allbets = new List<int>(               );
			List<string> allchosenNumbers = new List<string>();
			int maxCounter = 8;
			
			Console.WriteLine("Enter a number from '1' to '8' only.");
			Console.Write("Enter number of bets:"                   );
			int NumofBets  = Convert.ToInt16(Console.ReadLine()     );
			Console.Clear(                                          );

			int[] bets = new int [NumofBets];
			string[] chosenNumbers = new string[NumofBets];
			 
			allbets.Clear();
			allchosenNumbers.Clear();
			if (NumofBets <= 0 || NumofBets > maxCounter) 
			{	
				Console.WriteLine("Try again!");
				Console.WriteLine(            );
				continue;
			}						
			for(int i = 0; i < NumofBets; i++)
			{
				while(true)
				{
				    Console.WriteLine("----------------------------------");
					Console.WriteLine($"Current Balance:P{user.balance}"  );
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
						continue;
					}
				} 
				bool Valid = false;
				while (!Valid)
				{
					Console.WriteLine("---------------------------------");
					for (int j = 0; j < number.Length; j++)
					{
						Console.ForegroundColor = Consolecolor[j];
						Console.Write($"| {number[j]} "                );
					}
					Console.ResetColor(                                );
					Console.WriteLine("|"                              );
					Console.WriteLine("---------------------------------");
					Console.Write($"Choose a 'Number' for bet{i + 1}: ");
					string Numberchoice = Console.ReadLine(            );
					foreach (string ValidNumbers in number)
					{
						if (Numberchoice == ValidNumbers)
						{	
							chosenNumbers[i] = Numberchoice;
							allchosenNumbers.Add(Numberchoice);
							Valid = true; 
							break;
						}					
					}
					if (!Valid)
					{
						Console.WriteLine("Invalid input! Please choose a valid number.");
						Console.Clear(                                                  );
					}		
				}
			Console.Clear();
			}   
			Console.WriteLine("===============BETS================");
			for (int i = 0; i < allbets.Count; i++)
			{		
				Console.WriteLine($"Bet {i + 1}:P{allbets[i]}"     );
				Console.WriteLine(                                 );				 
			}
			Console.WriteLine("==============NUMBERS==============");
			for (int i = 0; i < allchosenNumbers.Count; i++)
			{			
				Console.ForegroundColor = Consolecolor[Array.IndexOf(number, allchosenNumbers[i])];
				Console.WriteLine($"Color {i + 1}: {allchosenNumbers[i]}"                        );
				Console.WriteLine();
			}				
			//GAME RESULT
			Console.ResetColor(            );
			Console.WriteLine("SPINNING...");
			Console.ReadKey(               );
			Console.Clear(                 );
			Random random = new Random(    );
			int randomWinningIndex = random.Next(0, number.Length);
			string randomWinningNumber = number[randomWinningIndex];
			
			Console.WriteLine("----------------------------------------------------");
			Console.WriteLine("                     GAME RESULT                    ");
			Console.WriteLine("----------------------------------------------------");
			Console.ForegroundColor = Consolecolor[randomWinningIndex];
			Console.WriteLine($"Winning number: {randomWinningNumber}"              );
			Console.ResetColor(                                                     );
			Console.WriteLine("----------------------------------------------------");
			int TotalWinnings = CalculateTotalWinnings(allbets, allchosenNumbers, number, randomWinningNumber, Consolecolor, user, leaderboard);
			Console.WriteLine($"{user.Name}'s total winnings:P{TotalWinnings}"      );	
			Console.WriteLine($"Your current balance:P{user.balance += TotalWinnings}");
			Console.WriteLine("----------------------------------------------------");	
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
	private int CalculateTotalWinnings (List<int> allbets, List<string> allchosenNumbers, string[] number, string randomWinningNumber, ConsoleColor[] Consolecolor, Player player, Leaderboard leaderboard) 
	{
		int TotalWinnings = 0;
		for (int i = 0; i < allchosenNumbers.Count; i++)
		{
			int matches = 0;
			int Winnings = 0;
			if (allchosenNumbers[i] == randomWinningNumber)
			{
				matches = 1;
				Winnings += allbets[i] * 3;
			}
			else 
			{
				matches = 0;
				Winnings = 0;
			}
		    TotalWinnings += Winnings;
			Console.ForegroundColor = Consolecolor[Array.IndexOf(number, allchosenNumbers[i])];
			Console.WriteLine($"Bet {i + 1}: P{allbets[i]}, Color: {allchosenNumbers[i]}, Matches: {matches}, Winnings:P{Winnings}");
			Console.ResetColor();
		}
		leaderboard.UpdateScore(player, TotalWinnings);
		return TotalWinnings;
	}
}
