using System;
using System.Collections.Generic;
using System.Text;

namespace PokerRetry
{
    public class Deck
    {
        public List<Card> Cards { get; } = new List<Card>();

        // Holds all possible Suits and Ranks for a Card to have
        private string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        private string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10",
                "Jack", "Queen", "King", "Ace" };


        public Deck()
        {
            // Starting value to be used in the cards
            int value = 2;

            // Creates each possible combination of Card and adds to Cards list
            foreach (string rank in ranks)
            {
                foreach (string suit in suits)
                {
                    Cards.Add(new Card(rank, suit, value));
                }
                value++;
            }
        }
    }
}