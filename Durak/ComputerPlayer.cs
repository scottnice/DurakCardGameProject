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

        readonly int DECK_SIZE;


        public ComputerPlayer(Point seat, Deck theDeck)
        {
            DECK_SIZE = theDeck.GetDeckSize;
            mySeat = seat;
        }

    }
}
