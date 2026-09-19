using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PokerRetry
{
    public class Dialogue
    {
        // Stores all the scores from each player.
        public static List<int[]> AllPlayerScores { get; set; } = new List<int[]>();

        public static string[] WinningHands = {"High Card", "One Pair", "Two Pair",
            "Three of a Kind", "Straight", "Flush", "Full House",
            "Four of a Kind", "Straight Flush", "Royal Flush"};


        // The initial screen the user sees upon launching the program
        public static int WelcomeScreen()
        {
            Console.WriteLine(
                "Welcome to Poker\n" +
                "\n" +
                "1.) Start Game\n" +
                "2.) Exit"
            );

            int choice = Utilities.EntryInRange(1, 2);
            Console.Clear();
            return choice;
        }

        // Basic Setup for the Game
        public static void GameSetup(Deck deck)
        {
            // Creating the User and Dealer
            Utilities.PlayerCreator(deck.Cards, isDealer: true);
            Utilities.PlayerCreator(deck.Cards, isUser: true);

            // User enters the number of opponents and that amount is created
            Console.Write("Enter Number of Opponents (1 - 4): ");
            int count = Utilities.EntryInRange(1, 4);
            Utilities.PlayerCreator(deck.Cards, count);

            Console.Clear();
        }

        // The Gameplay loop
        public static void Gameplay(List<Card> deck, Player dealer, Player user)
        {
            bool gameOver = false;
            int round = 1;

            while (!gameOver)
            {
                Console.WriteLine($"Current {dealer.Name}'s Hand:");
                dealer.ShowHand();
                Console.WriteLine();

                Console.WriteLine($"{user.Name}'s Hand:");
                user.ShowHand();
                Console.WriteLine();

                if (round < 4)
                {
                    Console.WriteLine(
                    "1.) Continue\n" +
                    "2.) End Game"
                    );

                    int choice = Utilities.EntryInRange(1, 2);
                    if (choice == 2)
                        gameOver = true;

                    dealer.AddCard(deck);
                }
                else
                {
                    gameOver = true;
                }

                Console.Clear();
                round++;
            }

            Console.Clear();
            FinalScreen(dealer, user);
        }

        // Final screen the user sees before end of game
        public static void FinalScreen(Player dealer, Player user)
        {
            Console.WriteLine("End Game:\n");

            Console.WriteLine($"Current {dealer.Name}'s Hand:");
            dealer.ShowHand();
            Console.WriteLine();

            // Display the hands of all players
            for (int i = 1; i < Player.AllPlayers.Count; i++)
            {
                Console.WriteLine($"{Player.AllPlayers[i].Name}'s Hand:");
                Player.AllPlayers[i].ShowHand();

                Console.WriteLine();
            }

            // Find the scores of all players and display their hands
            for (int i = 1; i < Player.AllPlayers.Count; i++)
            {
                AllPlayerScores.Add(Score.FindHand(dealer.PlayerCards,
                    Player.AllPlayers[i].PlayerCards));

                // Example: Player's Hand: Full House
                Console.WriteLine($"{Player.AllPlayers[i].Name}'s Hand: " +
                    $"{WinningHands[AllPlayerScores[i - 1][0]]}");
            }

            // Tie-Breaker if needed
            int largestRank = AllPlayerScores.Max(a => a[0]);
            int maxValueIndex = AllPlayerScores.FindIndex(score => score[0] == largestRank);
            if (AllPlayerScores.Count(n => n[0] == largestRank) > 1)
            {
                List<int> tiedPlayers = new List<int>();

                // Find the indices of all players with the largest rank
                for (int i = 0; i < AllPlayerScores.Count; i++)
                {
                    if (AllPlayerScores[i][0] == largestRank)
                    {
                        tiedPlayers.Add(i);
                    }
                }

                // Find the player with the highest tie-breaker value among the tied players
                int maxValue = 0;
                foreach (int index in tiedPlayers)
                {
                    if (AllPlayerScores[index][1] > maxValue)
                    {
                        maxValueIndex = index;
                        maxValue = AllPlayerScores[index][1];
                    }
                    else if (AllPlayerScores[index][1] == maxValue)
                    {
                        int temp = Score.TieBreaker(Player.AllPlayers[index + 1].PlayerCards,
                            Player.AllPlayers[maxValueIndex + 1].PlayerCards);

                        if (temp == 1)
                            maxValueIndex = index;
                    }
                }
            }

            Console.WriteLine($"{Player.AllPlayers[maxValueIndex + 1].Name} wins!");
        }
    }
}
