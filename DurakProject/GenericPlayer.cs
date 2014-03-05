using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardLibrary
{
    /// <summary>
    /// Represents a basic durak player. Will be used as a base class for the computer
    /// and human durak player classes.
    /// </summary>
    abstract class GenericPlayer
    {
        Hand hand;

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
