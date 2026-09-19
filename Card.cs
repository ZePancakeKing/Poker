using System;
using System.Collections.Generic;
using System.Text;

namespace PokerRetry
{
    public class Card
    {
        public string Rank { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }

        public Card(string rank, string suit, int value)
        {
            Rank = rank;
            Suit = suit;
            Value = value;
        }
    }
}
