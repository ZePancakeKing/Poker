namespace PokerRetry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Button to Begin");
            Console.ReadKey();
            Console.Clear();

            /*
            // Temporary for Testing
            List<Card> list1 = new List<Card>();
            List<Card> list2 = TestHands.testHands(2);
            List<int[]> tempScoreList = new List<int[]>();

            tempScoreList.Add(Score.FindHand(list1, list2));
            Console.WriteLine($"{Dialogue.WinningHands[tempScoreList[0][0]]}");

            Console.ReadKey();
            */

            int choice = Dialogue.WelcomeScreen();
            if (choice == 1)
            {
                Deck deck = new Deck();

                Dialogue.GameSetup(deck);
                Dialogue.Gameplay(deck.Cards, Player.AllPlayers[0], Player.AllPlayers[1]);
            }

            Console.ReadKey();
        }
    }
}
