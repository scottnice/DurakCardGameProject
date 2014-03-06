using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //maybe Play delegate that handles Attack, DefendBasic, and DefendPassing functions 
        void Attack(ref GenericPlayer defender)
        {
        }

        
        bool Defend ()
        {
            bool trumpPlayed = false;


            

            return trumpPlayed;
        }

        
    }
}
