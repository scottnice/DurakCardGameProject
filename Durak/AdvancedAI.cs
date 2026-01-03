using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Represents an advanced computer player with strategic decision-making
    /// Uses card evaluation, risk assessment, and game state tracking
    /// </summary>
    class AdvancedAI : ComputerPlayer
    {
        // Track cards that have been played to estimate remaining deck composition
        private List<Card> playedCards = new List<Card>();

        // Random number generator for tie-breaking decisions
        private Random rng = new Random();

        /// <summary>
        /// Constructor calls base constructor
        /// </summary>
        /// <param name="theGame"></param>
        /// <param name="name"></param>
        public AdvancedAI(DurakGame theGame, String name = "Advanced AI")
            : base(theGame, name) { }

        #region "Methods"

        /// <summary>
        /// Evaluates the strategic value of a card
        /// Higher values indicate more valuable cards to keep
        /// </summary>
        /// <param name="card">The card to evaluate</param>
        /// <returns>Strategic value score</returns>
        private int EvaluateCardValue(Card card)
        {
            int value = (int)card.getRank;

            // Trump cards are significantly more valuable
            if (card.getSuit == theGame.trumpCard.getSuit)
            {
                value += 20;
            }

            return value;
        }

        /// <summary>
        /// Calculates the total value of all cards in the bout
        /// </summary>
        /// <returns>Total value of cards in play</returns>
        private int CalculateBoutValue()
        {
            int totalValue = 0;
            for (int i = 0; i < theGame.myBout.GetCardCount; i++)
            {
                totalValue += EvaluateCardValue(theGame.myBout[i]);
            }
            return totalValue;
        }

        /// <summary>
        /// Evaluates overall hand strength
        /// </summary>
        /// <returns>Hand strength score</returns>
        private int EvaluateHandStrength()
        {
            int strength = 0;
            int trumpCount = 0;

            for (int i = 0; i < myHand.GetCardCount; i++)
            {
                Card card = myHand[i];
                strength += EvaluateCardValue(card);

                if (card.getSuit == theGame.trumpCard.getSuit)
                {
                    trumpCount++;
                }
            }

            // Bonus for having multiple trump cards
            strength += trumpCount * 5;

            return strength;
        }

        /// <summary>
        /// Determines if we should defend or pick up the cards
        /// </summary>
        /// <param name="playableCards">List of cards we could use to defend</param>
        /// <returns>True if we should defend, false if we should pick up</returns>
        private bool ShouldDefend(List<int> playableCards)
        {
            if (playableCards.Count == 0)
                return false;

            // Calculate the value of cards in the bout
            int boutValue = CalculateBoutValue();

            // Find the cheapest defense option
            int cheapestDefenseValue = int.MaxValue;
            foreach (int cardIndex in playableCards)
            {
                int cardValue = EvaluateCardValue(myHand[cardIndex]);
                if (cardValue < cheapestDefenseValue)
                {
                    cheapestDefenseValue = cardValue;
                }
            }

            // If the bout is very small (1-2 low cards), consider picking up
            if (theGame.myBout.GetCardCount <= 2 && boutValue < 10)
            {
                // If we'd have to use a valuable card to defend, pick up instead
                if (cheapestDefenseValue > boutValue + 10)
                {
                    return false;
                }
            }

            // Late game: be more willing to defend to avoid picking up
            if (theGame.myDeck.Empty)
            {
                // Only pick up if the bout is really cheap
                if (boutValue < 5 && cheapestDefenseValue > 25)
                {
                    return false;
                }
            }

            // If we have a strong hand and bout is cheap, defend
            if (EvaluateHandStrength() > 100 && boutValue < 20)
            {
                return true;
            }

            // Default: defend if we can
            return true;
        }

        /// <summary>
        /// Selects the best attacking card using strategic evaluation
        /// </summary>
        /// <returns>Index of the card to play, or PASS</returns>
        internal override int Attack()
        {
            List<int> playableCards = theGame.playableAttackingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            // If this is the first attack of the bout
            if (theGame.myBout.GetCardCount == 0)
            {
                return SelectInitialAttackCard(playableCards);
            }
            else
            {
                return SelectFollowUpAttackCard(playableCards);
            }
        }

        /// <summary>
        /// Selects the best card for the initial attack
        /// </summary>
        /// <param name="playableCards">List of playable card indices</param>
        /// <returns>Index of selected card</returns>
        private int SelectInitialAttackCard(List<int> playableCards)
        {
            // Separate non-trump and trump cards
            List<int> nonTrumpCards = new List<int>();
            List<int> trumpCards = new List<int>();

            foreach (int cardIndex in playableCards)
            {
                if (myHand[cardIndex].getSuit == theGame.trumpCard.getSuit)
                {
                    trumpCards.Add(cardIndex);
                }
                else
                {
                    nonTrumpCards.Add(cardIndex);
                }
            }

            // Prefer attacking with non-trump cards
            if (nonTrumpCards.Count > 0)
            {
                // Find the lowest non-trump card
                int bestCard = nonTrumpCards[0];
                for (int i = 1; i < nonTrumpCards.Count; i++)
                {
                    if (myHand[nonTrumpCards[i]] < myHand[bestCard])
                    {
                        bestCard = nonTrumpCards[i];
                    }
                }
                return bestCard;
            }
            else
            {
                // Only trump cards available - use the lowest
                int bestCard = trumpCards[0];
                for (int i = 1; i < trumpCards.Count; i++)
                {
                    if (myHand[trumpCards[i]] < myHand[bestCard])
                    {
                        bestCard = trumpCards[i];
                    }
                }
                return bestCard;
            }
        }

        /// <summary>
        /// Selects the best card for follow-up attacks (when bout already has cards)
        /// </summary>
        /// <param name="playableCards">List of playable card indices</param>
        /// <returns>Index of selected card</returns>
        private int SelectFollowUpAttackCard(List<int> playableCards)
        {
            // For follow-up attacks, prefer lower value cards
            // This makes the defender use more cards

            int bestCard = playableCards[0];
            int lowestValue = EvaluateCardValue(myHand[playableCards[0]]);

            for (int i = 1; i < playableCards.Count; i++)
            {
                int currentValue = EvaluateCardValue(myHand[playableCards[i]]);
                if (currentValue < lowestValue)
                {
                    lowestValue = currentValue;
                    bestCard = playableCards[i];
                }
            }

            return bestCard;
        }

        /// <summary>
        /// Selects the best defending card or decides to pick up
        /// </summary>
        /// <returns>Index of the card to play, or PASS to pick up</returns>
        internal override int Defend()
        {
            List<int> playableCards = theGame.playableDefendingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            // Evaluate whether we should defend or pick up
            if (!ShouldDefend(playableCards))
            {
                return DurakGame.PASS;
            }

            // We've decided to defend - choose the cheapest valid card
            return SelectDefenseCard(playableCards);
        }

        /// <summary>
        /// Selects the most efficient defending card
        /// Uses the lowest value card that can still defend
        /// </summary>
        /// <param name="playableCards">List of playable card indices</param>
        /// <returns>Index of selected card</returns>
        private int SelectDefenseCard(List<int> playableCards)
        {
            // Separate trump and non-trump defensive cards
            List<int> nonTrumpCards = new List<int>();
            List<int> trumpCards = new List<int>();

            Card attackCard = theGame.myBout[theGame.myBout.GetCardCount - 1];
            bool attackIsNonTrump = attackCard.getSuit != theGame.trumpCard.getSuit;

            foreach (int cardIndex in playableCards)
            {
                if (myHand[cardIndex].getSuit == theGame.trumpCard.getSuit)
                {
                    trumpCards.Add(cardIndex);
                }
                else
                {
                    nonTrumpCards.Add(cardIndex);
                }
            }

            // If we can defend with a non-trump card, prefer that
            if (nonTrumpCards.Count > 0)
            {
                // Use the lowest non-trump card
                int bestCard = nonTrumpCards[0];
                for (int i = 1; i < nonTrumpCards.Count; i++)
                {
                    if (myHand[nonTrumpCards[i]] < myHand[bestCard])
                    {
                        bestCard = nonTrumpCards[i];
                    }
                }
                return bestCard;
            }
            else
            {
                // Must use a trump card - use the lowest one
                int bestCard = trumpCards[0];
                for (int i = 1; i < trumpCards.Count; i++)
                {
                    if (myHand[trumpCards[i]] < myHand[bestCard])
                    {
                        bestCard = trumpCards[i];
                    }
                }
                return bestCard;
            }
        }

        #endregion
    }
}
