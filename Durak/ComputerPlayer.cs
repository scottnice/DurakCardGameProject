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
    abstract class ComputerPlayer : GenericPlayer
    {
        // list of computer difficulties
        internal enum AIDifficulty { Basic, Advanced, Cheater };

        /// <summary>
        /// used to constructor chain back to Generic Player
        /// </summary>
        /// <param name="theGame"></param>
        /// <param name="name"></param>
        public ComputerPlayer(DurakGame theGame, String name = "Computer Player") : base(theGame, name)
        {
     
        }

    }
}
