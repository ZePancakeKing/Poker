using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PokerRetry
{
    public class Player
    {
        public string Name { get; set; } = "Player";
        public List<Card> PlayerCards { get; } = new List<Card>();
        public static List<Player> AllPlayers = new List<Player>();
        public static int TotalBots { get; set; }
        private Random rand = new Random();


        // Adds a card from the deck to the player's hand
        public void AddCard(List<Card> deck)
        {
            // Returns early if deck is null or empty
            if (deck == null || deck.Count == 0)
            { return; }

            // Picks a random card out of the deck
            int index = rand.Next(deck.Count);

            // Adds the random card to Player's hand then removes card from deck
            PlayerCards.Add(deck[index]);
            deck.RemoveAt(index);
        }

        // Prints out the hand of the Player
        public void ShowHand()
        {
            // Returns early if Player doesn't have any cards
            if (PlayerCards.Count == 0)
                return;

            // Goes through all cards and prints them out
            foreach (Card card in PlayerCards)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }
    }
}
