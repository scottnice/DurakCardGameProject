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
        public PassingRules(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.FIFTY_TWO,
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
            : base(numberOfPlayers, deckSize, difficulty, isAllAI)
        {

            Play();

        }

        public override List<int> playableAttackingCards(CardLibrary.Hand playerHand)
        {
            throw new NotImplementedException();
        }

        public override List<int> playableDefendingCards(CardLibrary.Hand playerHand)
        {
            throw new NotImplementedException();
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
