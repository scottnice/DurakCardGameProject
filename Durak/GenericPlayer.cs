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
        // A delegate type for hooking up change notifications.
        public delegate void WonGameEvent(object sender, EventArgs e);

        public event WonGameEvent Won;

        protected DurakGame theGame;

        internal string name;

        internal Hand myHand = new Hand();

        internal int GetCardCount
        {
            get { return myHand.GetCardCount; }
        }

        internal GenericPlayer(DurakGame theGame, String name = "")
        {
            this.theGame = theGame;
            this.name = name;
        }
        
        internal virtual int Attack() { return 0; }

        internal virtual int Defend() { return 0; }

        
    }
}
