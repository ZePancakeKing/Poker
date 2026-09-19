using System;
using System.Collections.Generic;
using System.Text;

namespace PokerRetry
{
    internal class TestHands
    {
        public static List<Card> testHands(int num)
        {
            List<List<Card>> testHands = new List<List<Card>>();

            List<Card> testHand1 = new List<Card>();
            testHand1.Add(new Card("King", "Spades", 13));
            testHand1.Add(new Card("Queen", "Spades", 12));
            testHand1.Add(new Card("Ace", "Spades", 14));
            testHand1.Add(new Card("10", "Spades", 10));
            testHand1.Add(new Card("Jack", "Spades", 11));
            testHands.Add(testHand1);

            List<Card> testHand2 = new List<Card>();
            testHand2.Add(new Card("5", "Spades", 5));
            testHand2.Add(new Card("4", "Spades", 4));
            testHand2.Add(new Card("Ace", "Spades", 14));
            testHand2.Add(new Card("2", "Spades", 2));
            testHand2.Add(new Card("3", "Spades", 3));
            testHands.Add(testHand2);

            List<Card> testHand3 = new List<Card>();
            testHand3.Add(new Card("King", "Hearts", 13));
            testHand3.Add(new Card("Queen", "Hearts", 12));
            testHand3.Add(new Card("Ace", "Hearts", 14));
            testHand3.Add(new Card("Ace", "Clubs", 14));
            testHand3.Add(new Card("3", "Hearts", 3));
            testHand3.Add(new Card("King", "Spades", 13));
            testHand3.Add(new Card("King", "Diamonds", 13));
            testHand3.Add(new Card("King", "Clubs", 13));
            testHands.Add(testHand3);

            return testHands[num];
        }
    }
}
