using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Represents a basic game of durak without the use of passing rules
    /// </summary>
    class BasicRules : DurakGame
    {

        #region "Constructor"
        /// <summary>
        /// Default constructor calls durakgame's constructor and passes arguments
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="deckSize"></param>
        /// <param name="difficulty"></param>
        /// <param name="isAllAI"></param>
        internal BasicRules(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.FIFTY_TWO,
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
                            : base ( numberOfPlayers,  deckSize, difficulty, isAllAI){}

        /// <summary>
        /// Constructor with per-player AI difficulties
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="deckSize"></param>
        /// <param name="difficulties"></param>
        /// <param name="isAllAI"></param>
        internal BasicRules(int numberOfPlayers, Deck.DeckSize deckSize,
                        ComputerPlayer.AIDifficulty[] difficulties, bool isAllAI)
                            : base(numberOfPlayers, deckSize, difficulties, isAllAI) { }
        #endregion

        #region "Methods"
        /// <summary>
        /// Used to validate the humans card that is about to be played
        /// </summary>
        /// <param name="cardIndex"></param>
        internal override void validateHumanCard(int cardIndex)
        {
            base.validateHumanCard(cardIndex);
        }

        /// <summary>
        /// Returns a list of indexes that corresponds to the cards in the player's hand that can be played this turn as attacking cards
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns></returns>
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
                        if (playerHand[i] == myBout[j])
                        {
                            playableCards.Add(i);
                            cardAdded = true;
                        }
                    }
                }

            }

            return playableCards;
        }

        /// <summary>
        /// Returns a list of indexes that corresponds to the cards in the player's hand that can be played this turn as defending cards
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns></returns>
        public override List<int> playableDefendingCards(CardLibrary.Hand playerHand)
        {
            List<int> playableCards = new List<int>();

            if (myBout.Empty)
                throw new ArgumentNullException("The bout must have a card in it to defend.");
            else
            {
                CardLibrary.Card lastPlayedCard = myBout[myBout.GetCardCount - 1];

                for (int i = 0; i < playerHand.GetCardCount; ++i)
                {
                    if (playerHand[i].getSuit == lastPlayedCard.getSuit && playerHand[i] > lastPlayedCard)
                    {
                        playableCards.Add(i);
                    }
                    else if (playerHand[i].getSuit == trumpCard.getSuit && playerHand[i].getSuit != lastPlayedCard.getSuit)
                    {
                        playableCards.Add(i);
                    }
                }
            }

            return playableCards;
        }
        #endregion

    }
}
