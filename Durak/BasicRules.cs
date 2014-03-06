using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Durak
{
    class BasicRules : DurakGame
    {
        public BasicRules(int numberOfCards, int numberOfPlayers): base ( numberOfCards,  numberOfPlayers)   // needed rules enum
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
                 * DefendingPlay.Defend(); Defend returns bool for the sake of the passing rules. In this case it will always be false.
                 * 
                 * 
                 */
            }
            

            return isGameOver;
        }

       

    }
}
