using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace Durak
{
    class BasicAI : ComputerPlayer
    {
        public BasicAI(DurakGame theGame, String name = "Basic AI")
            : base(theGame, name) { }

        internal override int Attack()
        {
            int bestCard;

            List<int> playableCards = theGame.playableAttackingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            bestCard = 0;

            for (int i = 1; i < playableCards.Count; ++i)
            {
                if (playableCards[bestCard] > playableCards[i])
                    bestCard = i;
            }

            return playableCards[bestCard];

        }

        internal override int Defend()
        {
            int bestCard;

            List<int> playableCards = theGame.playableDefendingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            bestCard = 0;

            for (int i = 1; i < playableCards.Count; ++i)
            {
                if (playableCards[bestCard] >= playableCards[i])
                    bestCard = i;
            }

            return playableCards[bestCard];
        }

    }
}
