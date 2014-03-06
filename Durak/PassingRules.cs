using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak
{
    class PassingRules : DurakGame
    {
        public PassingRules(int numberOfCards, int numberOfPlayers): base ( numberOfCards,  numberOfPlayers)   // needed rules enum
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
