using System;
using System.Collections.Generic;
using System.Text;

namespace PokerRetry
{
    public class Utilities
    {
        // Used to make sure user entry is an int
        public static int IsInt()
        {
            int num = -1;
            while (num == -1)
            {
                try
                {
                    num = int.Parse(Console.ReadLine());
                }
                catch
                {
                    continue;
                }
            }
            return num;
        }

        // Used to make sure user entry fits within a set range
        public static int EntryInRange(int min, int max)
        {
            int num = min - 1;

            while (num < min || num > max)
            {
                num = IsInt();
            }

            return num;
        }

        public static void PlayerCreator(List<Card> deck, int amount = 1,
                bool isUser = false, bool isDealer = false)
        {
            int cardAmount;
            Player player;

            while (amount > 0)
            {
                if (isUser) // Creates a User object
                {
                    // Asks for a name and assigns it as "User" if no name is given
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine() ?? "User";

                    player = new User(name);
                    cardAmount = 3;
                }
                else if (isDealer) // Creates a dealer object
                {
                    player = new Dealer();
                    cardAmount = 2;
                }
                else // Creates a Bot object
                {
                    player = new Bot();
                    cardAmount = 3;
                }

                // Adds 3 Cards to Player objects or 2 Cards to the Dealer object
                for (int i = 0; i < cardAmount; i++)
                {
                    player.AddCard(deck); 
                }

                amount--;
            }
        }
    }
}
