using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Uses A.I to play against other players.
    /// </summary>
    class ComputerPlayer : GenericPlayer
    {
        public enum AIDifficulty {Basic, Advanced, Cheater};

        readonly int DECK_SIZE;
        private readonly Deck cheaterDeck;

        public ComputerPlayer(Point seat, Deck theDeck, AIDifficulty difficulty )
        {
            DECK_SIZE = theDeck.GetDeckSize;
            mySeat = seat;
        }

    }
}
