using System;
using System.Collections.Generic;
using System.Text;

namespace PokerRetry
{
    // Class for the User
    public class User : Player
    {
        public User(string name)
        {
            Name = name;

            AllPlayers.Add(this);
        }
    }

    // Class for the Bot players
    public class Bot : Player
    {
        public Bot()
        {
            TotalBots++;

            Name = $"Bot {TotalBots}";

            AllPlayers.Add(this);
        }
    }

    // Class for the Dealer
    public class Dealer : Player
    {
        public Dealer()
        {
            Name = "Dealer";

            AllPlayers.Add(this);
        }
    }
}