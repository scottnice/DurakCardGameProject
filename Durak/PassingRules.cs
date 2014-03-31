using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    class PassingRules : DurakGame
    {
        public PassingRules(Deck theDeck, List<GenericPlayer> thePlayers) : base(theDeck, thePlayers) // needed rules enum
        {

            Play();

        }



        private bool Play()
        {
            if (!isGameOver)
            {
                /*
                 * 
                 * AttackingPlayer.Attack();
                 * 
                 * DefendingPlay.Defend(); Defend returns bool for the sake of the passing rules. Defend can return true or false 
                 * 
                 * 
                 * 
                 */
            }
            

            return isGameOver;
        }
    }
}
