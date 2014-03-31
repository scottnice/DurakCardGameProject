using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Durak
{
    /// <summary>
    /// This class will provide an interface for the human player to play durak against the 
    /// computer.
    /// </summary>
    class HumanPlayer : GenericPlayer
    {
        int cardIndex;
        public HumanPlayer()
        {

        }

        internal virtual void play(int cardIndex)
        {
            this.cardIndex = cardIndex;
        }

        internal override int Attack()
        {
            return cardIndex;
        }

        internal override int Defend()
        {
            return cardIndex;
        }

        

    }
}
