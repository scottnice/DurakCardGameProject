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
    /// </summary>
    abstract class GenericPlayer
    {
        internal Hand myHand = new Hand();

        void Attack(ref GenericPlayer defender)
        {
        }

        void Take()
        {
        }

        void Pass()
        {
        }
    }
}
