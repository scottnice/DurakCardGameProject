using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Represents a basic durak player. Will be used as a base class for the computer
    /// and human durak player classes.
    /// 
    /// </summary>
    abstract class GenericPlayer
    {
        // reference to the game object
        protected DurakGame theGame;

        // this players name
        internal string name;

        // this players hand
        internal Hand myHand = new Hand();

        #region "Properties"
        // returns the number of cards in this players hand
        internal int GetCardCount
        {
            get { return myHand.GetCardCount; }
        }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Default Constructor sets game object reference and name
        /// </summary>
        /// <param name="theGame"></param>
        /// <param name="name"></param>
        internal GenericPlayer(DurakGame theGame, String name = "")
        {
            this.theGame = theGame;
            this.name = name;
        }
        
        #endregion

        #region "Method"
        /// <summary>
        /// Virtual function that all player types use for attacking
        /// </summary>
        /// <returns>returns the index of the card to be played or PASS (-1) if the player passes</returns>
        internal virtual int Attack() { return 0; }

        /// <summary>
        /// Virtual function that all player types use for defending
        /// </summary>
        /// <returns>returns the index of the card to be played or PASS (-1) if the player passes</returns>
        internal virtual int Defend() { return 0; }

        #endregion

    }
}
