// See https://aka.ms/new-console-template for more information
using System;

namespace Games
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//user interface
			Console.WriteLine("=====================================");
			Console.WriteLine("              PERYAHAN               ");
			Console.WriteLine("=====================================");
			Console.Write("Please enter your username:");
			string userName = Console.ReadLine();
			Console.WriteLine("Welcome:"+ userName);
			Player user = new Player(userName); //player class
			Leaderboard leaderboard = new Leaderboard(); //leaderboard class
			leaderboard.AddPlayer(user); 
			Console.ReadKey();
			Console.Clear();
			Console.WriteLine("=====================================");
			Console.WriteLine("              SUGAL MENU"             );
			Console.WriteLine("=====================================");
			Console.WriteLine("1 - COLOR GAME."                      );
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("2 - ROLETA NG KAPALARAN."             );
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("3 - DROP BALL."                       );
			Console.WriteLine("=====================================");
			Console.Write("Enter a number:"                          );
			int singleplayer = Convert.ToByte(Console.ReadLine());	
			Console.Clear();
	        switch (singleplayer)
		    {		 
			    case 1:
			     Console.WriteLine("=====================================");
	             Console.WriteLine("             COLOR GAME"              );
		         Console.WriteLine("=====================================");
				 ColorGame game1 = new ColorGame(); 
				 game1.Play1(user, leaderboard);
				 Console.Clear();
				break;
				case 2: 
			     Console.WriteLine("=====================================");
	             Console.WriteLine("         ROLETA NG KAPALARAN"         );
				 Console.WriteLine("   YOUR BET WILL RETURN 3 TIMES!!!"   );
		         Console.WriteLine("=====================================");
				 RoletaNgKapalaran game2 = new RoletaNgKapalaran();
				 game2.Play2(user, leaderboard);
				 Console.Clear();
			    break; 
				case 3: 
				 Console.Clear();
			     Console.WriteLine("=================================================");
	             Console.WriteLine("                     DROP BALL"                   );
		         Console.WriteLine("=================================================");
				 DropBall game3 = new DropBall();
				 game3.play3(user, leaderboard);
				 Console.Clear();
			    break; 
				default:
			     Console.WriteLine("INVALID!");
				break;
		    } 
			leaderboard.DisplayLeaderboard();
		}
	}
}