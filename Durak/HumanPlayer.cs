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
        // the card index of the card the human has selected to play
        private int cardIndex;

        /// <summary>
        /// Calls base constructor and passes arguments
        /// </summary>
        /// <param name="theGame"></param>
        /// <param name="name"></param>
        public HumanPlayer(DurakGame theGame, String name = "Human Player") : base(theGame, name)
        {
        }

        #region "Methods"

        /// <summary>
        /// Used to set the human players selected card index
        /// </summary>
        /// <param name="cardIndex"></param>
        internal virtual void play(int cardIndex)
        {
            this.cardIndex = cardIndex;
        }

        /// <summary>
        /// Overrides Generic players attack and returns the card index value of the selected card
        /// </summary>
        /// <returns></returns>
        internal override int Attack()
        {
            return cardIndex;
        }

        /// <summary>
        /// Overrides Generic players attack and returns the card index value of the selected card
        /// </summary>
        /// <returns></returns>
        internal override int Defend()
        {
            return cardIndex;
        }

        #endregion


    }
}
