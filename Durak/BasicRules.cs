using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    class BasicRules : DurakGame
    {
        public BasicRules(Deck theDeck, List<GenericPlayer> thePlayers) : base(theDeck, thePlayers)   // needed rules enum
        {
        }



        public bool Defend(Card selectedCard)
        {
            if (!isGameOver)
            {
                if (selectedCard > myHand[myHand.GetCardCount - 1])
                {
                    return true;
                }
                else
                    return false;
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

        public bool Attack(Card selectedCard)
        {
            if(myHand.GetCardCount == 0)
            {
                return true;
            }
            else if(selectedCard.getRank != myHand[0].getRank)
            {
                return false;
            }
            return false;
        }

       

    }
}
