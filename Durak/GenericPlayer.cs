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
        internal Point mySeat;

        internal int GetCardCount
        {
            get { return myHand.GetCardCount; }
        }

        //maybe Play delegate that handles Attack, DefendBasic, and DefendPassing functions 
        internal virtual void Attack(ref GenericPlayer defender)
        {
        }

        
        internal virtual bool Defend ()
        {
            bool trumpPlayed = false;


            

            return trumpPlayed;
        }

        
    }
}
