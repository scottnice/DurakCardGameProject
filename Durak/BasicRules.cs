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
        internal BasicRules(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.FIFTY_TWO,
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
                            : base ( numberOfPlayers,  deckSize, difficulty, isAllAI)   // needed rules enum
        {

        }



        private void Play()
        {
            if (!isGameOver)
            {
                
            }
         
        }

        public override List<int> playableAttackingCards(CardLibrary.Hand playerHand)
        {
            List<int> playableCards = new List<int>(); 

            if (myBout.GetCardCount == 0)
            {
                for (int i = 0; i < playerHand.GetCardCount; ++i )
                {
                    playableCards.Add(i);
                }
            }
            else
            {
                for (int i = 0; i < playerHand.GetCardCount; ++i)
                {
                    bool cardAdded = false;
                    for (int j = 0; j < myBout.GetCardCount && cardAdded == false; ++j)
                    {
                        if (playerHand[i] > myBout[j])
                        {
                            playableCards.Add(i);
                            cardAdded = true;
                        }
                    }
                }

            }

            return playableCards;
        }

        public override List<int> playableDefendingCards(CardLibrary.Hand playerHand)
        {
            List<int> playableCards = new List<int>();
            CardLibrary.Card lastPlayedCard = myBout[myBout.GetCardCount - 1];

            for (int i = 0; i < playerHand.GetCardCount; ++i)
            {
                if (playerHand[i].getSuit == lastPlayedCard.getSuit && playerHand[i] > lastPlayedCard)
                {
                    playableCards.Add(i);
                }
                else if(playerHand[i].getSuit == trumpCard.getSuit)
                {
                    playableCards.Add(i);
                }
            }

            return playableCards;
        }
       

    }
}
