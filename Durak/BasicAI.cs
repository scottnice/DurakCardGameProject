using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Represents the most basic form of computer player - has very simple logic
    /// </summary>
    class BasicAI : ComputerPlayer
    {
        /// <summary>
        /// Default Constructor calls base constructor
        /// </summary>
        /// <param name="theGame"></param>
        /// <param name="name"></param>
        public BasicAI(DurakGame theGame, String name = "Basic AI")
            : base(theGame, name) { }

        #region "Methods"
        /// <summary>
        /// Plays the card with the lowest possible face value that is also playable, whether trump or not
        /// </summary>
        /// <returns></returns>
        internal override int Attack()
        {
            
            int bestCard;

            List<int> playableCards = theGame.playableAttackingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            bestCard = 0;

            for (int i = 1; i < playableCards.Count; ++i)
            {
                if (myHand[playableCards[bestCard]] > myHand[playableCards[i]])
                    bestCard = i;
            }

            return playableCards[bestCard];

        }

        /// <summary>
        /// Selects from a list of playable defending cards and plays the lowest rank of these cards
        /// </summary>
        /// <returns></returns>
        internal override int Defend()
        {

            int bestCard;

            List<int> playableCards = theGame.playableDefendingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            bestCard = 0;

            for (int i = 1; i < playableCards.Count; ++i)
            {
                if (myHand[playableCards[bestCard]] >= myHand[playableCards[i]])
                    bestCard = i;
            }

            return playableCards[bestCard];
        }
        #endregion

    }
}
