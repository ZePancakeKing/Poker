using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PokerRetry
{
    public class Score
    {
        public static int RankValue { get; set; }

        public static int[] SameRank { get; set; } = new int[13];

        public enum HandRank
        {
            HighCard = 0,
            Pair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            Straight = 4,
            Flush = 5,
            FullHouse = 6,
            FourOfAKind = 7,
            StraightFlush = 8,
            RoyalFlush = 9
        }

        // Tie-Breaker: Returns 1 if player1 has better hand, 2 if it's player2
            // Returns 0 if they're exactly the same
        public static int TieBreaker(List<Card> player1, List<Card> player2)
        {
            // Sorts the player's hands in terms of their Value
            player1.Sort((x, y) => y.Value.CompareTo(x.Value));
            player2.Sort((x, y) => y.Value.CompareTo(x.Value));

            for (int i = 0; i < player1.Count; i++)
            {
                if (player1[i].Value == player2[i].Value)
                    continue;
                else if (player1[i].Value > player2[i].Value)
                    return 1;
                else
                    return 2;
            }

            return 0;
        }

        // Calculates the final score for the hand submitted
        public static int[] FindScore(List<Card> hand)
        {
            // Goes through and evaluates the value of each card
                    // for Royal Flush (9), Straight Flush (8), Flush (5), and Straight (4)
            if (RankValue == 9 || RankValue == 8 || RankValue == 5 || RankValue == 4)
            {
                return [RankValue, hand[0].Value];
            }

            // Four of a Kind
            if (RankValue == 7)
            {
                int index = Array.FindLastIndex(SameRank, value => value == 4);
                return [RankValue, (index + 2) * 4];
            }

            // Full House
            if (RankValue == 6)
            {
                int index3 = Array.FindLastIndex(SameRank, value => value == 3);
                int index2 = Array.FindLastIndex(SameRank, value => value == 2);
                return [RankValue, ((index3 + 2) * 3) + ((index2 + 2) * 2)];
            }
            
            // Three of a Kind
            if (RankValue == 3)
            {
                int index = Array.FindLastIndex(SameRank, value => value == 3);
                return [RankValue, (index + 2) * 3];
            }

            // Two Pair
            if (RankValue == 2)
            {
                int firstPair = Array.FindLastIndex(SameRank, value => value == 2);
                int secondPair = Array.FindLastIndex(SameRank, firstPair - 1, value => value == 2);

                return [RankValue, ((firstPair + 2) * 2) + ((secondPair + 2) * 2)];
            }

            // Pair
            if (RankValue == 1)
            {
                int index = Array.FindLastIndex(SameRank, value => value == 2);
                return [RankValue, (index + 2) * 2];
            }

            // High Card
            return [RankValue, Array.FindLastIndex(SameRank, value => value == 1) + 2];
        }

        // Central method used to find the hand the player has
        public static int[] FindHand(List<Card> dealerHand, List<Card> playerHand)
        {
            // A new list to hold all cards from both the dealer and player hands
            List<Card> combinedCards = new List<Card>();

            // Adds cards from playerHand to dealerHand to create easy access
            foreach (Card card in playerHand)
            {
                combinedCards.Add(card);
            }
            foreach (Card card in dealerHand)
            {
                combinedCards.Add(card);
            }

            List<Card> tempHand = new List<Card>();

            // Checking if Hand contains a Flush
            tempHand = FlushCheck(combinedCards);


            // Checking for Royal Flush and Straight Flush
                // If neither are found, value for regular Flush will be sent
            if (tempHand.Count == 5)
            {
                RankValue = (int)FlushStraightCheck(tempHand);
                return FindScore(tempHand);
            }


            // Checking if Hand contains a Straight and sends its value if it is
            tempHand = StraightCheck(combinedCards);
            if (tempHand.Count == 5)
            {
                RankValue = 4;
                return FindScore(tempHand);
            }


            // Checking for Four of a Kind, Full House, Three of a Kind, Two Pair and Pair
                // If none are found, sends value for High Card
            RankValue = (int)DuplicateCheck(combinedCards);
            return FindScore(combinedCards);
        }

        // Checks for duplicates in the given hand
        public static HandRank DuplicateCheck(List<Card> hand)
        {
            SameRank = new int[13];

            // Goes through each card and increases number to count duplicate Ranks
            foreach (Card card in hand)
            {
                SameRank[card.Value - 2]++;
            }

            // Checks for Four of a Kind
            if (SameRank.Contains(4))
                return HandRank.FourOfAKind;

            // Checks for Full House
            if (SameRank.Contains(3) && SameRank.Contains(2))
                return HandRank.FullHouse;

            // Checks for Three of a Kind
            if (SameRank.Contains(3))
                return HandRank.ThreeOfAKind;

            // Checks for Two Pair
            int count = 0;
            foreach (int num in SameRank)
            {
                if (num == 2)
                    count++;
            }
            if (count >= 2)
                return HandRank.TwoPair;

            // Checks for Pair
            if (count == 1)
                return HandRank.Pair;

            // Returns High Card if all else fails
            return HandRank.HighCard;
        }

        // Checks if the given hand contains a Straight.
        public static List<Card> StraightCheck(List<Card> hand)
        {
            // Sorts the cards in descending order in terms of their Value
            hand.Sort((x, y) => y.Value.CompareTo(x.Value));

            // Creating a temporary List that will contain cards that are in a row
            List<Card> straightHand = new List<Card>();
            straightHand.Add(hand[0]);
            
            int currentValue = hand[0].Value;
            Card aceCard = hand[0];

            // Goes through each card in the hand
            for (int i = 1; i < hand.Count; i++)
            {
                // If the current card is an Ace, it hols in case of "special" case
                if (hand[i].Rank == "Ace")
                    aceCard = hand[i];

                // Checks if the current card follows the Straight pattern
                if (hand[i].Value == currentValue - 1)
                {
                    straightHand.Add(hand[i]);
                    currentValue = hand[i].Value;

                    // Returns if straightHand has 5 cards. Evidence of straight
                    if (straightHand.Count == 5)
                        return straightHand;
                }
                else if (hand[i].Value == currentValue)
                {
                    continue;
                }
                else // If the next Card doesn't follow the pattern, resets List
                {
                    straightHand.Clear();
                    straightHand.Add(hand[i]);
                    currentValue = hand[i].Value;
                }
            }

            // Special case where hand contains 2, 3, 4, 5 with an Ace
            if (straightHand.Count == 4 && straightHand[3].Value == 2 && aceCard.Rank == "Ace")
            {
                aceCard.Value = 1;
                straightHand.Add(aceCard);
            }

            return straightHand;
        }

        // Checks if the given hand contains a Flush.
        public static List<Card> FlushCheck(List<Card> hand)
        {
            // Ordering the given hand by suit and then by Card value
            hand = hand.OrderBy(card => card.Suit)
                .ThenByDescending(card => card.Value)
                .ToList();
            
            // Creating a temporary list to hold a potential flush
            List<Card> tempList = new List<Card>();
            tempList.Add(hand[0]);
            string suit = hand[0].Suit;

            for (int i = 1; i < hand.Count; i++)
            {
                if (hand[i].Suit == suit) // Checks to see if this card's suit matches
                {
                    tempList.Add(hand[i]);

                    if (tempList.Count >= 5)
                        return tempList;
                }
                else // If they don't match, resets the List and changes Suit
                {
                    tempList.Clear();
                    tempList.Add(hand[i]);
                    suit = hand[i].Suit;
                }
            }

            return tempList;
        }

        // Checks for Royal Flush and Straight Flush
                // "hand" parameter is guaranteed to have a length of 5
        public static HandRank FlushStraightCheck(List<Card> hand)
        {
            bool isRoyal = true;

            // Checks for Royal Flush
            if (hand[0].Rank == "Ace" &&
                hand[1].Rank == "King" &&
                hand[2].Rank == "Queen" &&
                hand[3].Rank == "Jack" &&
                hand[4].Rank == "10")
            {
                return HandRank.RoyalFlush;
            }

            // Checks for a Straight Flush
            if (StraightCheck(hand).Count == 5)
                return HandRank.StraightFlush;

            return HandRank.Flush;
        }
    }
}
