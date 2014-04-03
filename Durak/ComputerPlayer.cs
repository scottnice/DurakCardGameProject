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
        internal enum AIDifficulty { Basic, Advanced, Cheater };

        readonly Deck.DeckSize DECK_SIZE;

        protected Hand discardPile;

        protected Hand bout;

        protected Suit trumpSuit;

        protected DurakGame theGame;

        public ComputerPlayer(DurakGame theGame)
        {
            this.theGame = theGame;   
        }

    }
}
