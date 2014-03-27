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
        internal Hand myHand = new Hand();

        protected const int PASS = -1;

        internal int GetCardCount
        {
            get { return myHand.GetCardCount; }
        }

        
        internal virtual int Attack() { return 0; }

        internal virtual int Defend() { return 0; }

        
    }
}
