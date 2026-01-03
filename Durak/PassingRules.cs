using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Represents a game of durak with passing rules enabled - warning file not complete do not use until implemented
    /// </summary>
    class PassingRules : DurakGame
    {
        #region "Constructors"
        /// <summary>
        /// Calls base constructor and passes arguments - not yet implemented
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="deckSize"></param>
        /// <param name="difficulty"></param>
        /// <param name="isAllAI"></param>
        public PassingRules(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.FIFTY_TWO,
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
            : base(numberOfPlayers, deckSize, difficulty, isAllAI)
        {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Constructor with per-player AI difficulties - not yet implemented
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="deckSize"></param>
        /// <param name="difficulties"></param>
        /// <param name="isAllAI"></param>
        public PassingRules(int numberOfPlayers, Deck.DeckSize deckSize,
                        ComputerPlayer.AIDifficulty[] difficulties, bool isAllAI)
            : base(numberOfPlayers, deckSize, difficulties, isAllAI)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Returns a list of indexes that corresponds to the cards in the player's hand that can be played this turn as attacking cards
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns></returns>
        public override List<int> playableAttackingCards(CardLibrary.Hand playerHand)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of indexes that corresponds to the cards in the player's hand that can be played this turn as defending cards
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns></returns>
        public override List<int> playableDefendingCards(CardLibrary.Hand playerHand)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unfinished code - will be added in next patch update ....
        /// </summary>
        /// <returns></returns>
        internal void Play()
        {
            throw new NotImplementedException();
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
        }

        #endregion

    }
}
