using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Represents a cheating AI that has perfect information about the game state
    /// Knows all opponent hands, deck composition, and makes optimal decisions
    /// </summary>
    class CheaterAI : ComputerPlayer
    {
        /// <summary>
        /// Constructor calls base constructor
        /// </summary>
        /// <param name="theGame"></param>
        /// <param name="name"></param>
        public CheaterAI(DurakGame theGame, String name = "Cheater AI")
            : base(theGame, name) { }

        #region "Methods"

        /// <summary>
        /// Evaluates the strategic value of a card with perfect information
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
        /// Gets the defending player's hand
        /// </summary>
        /// <returns>Defender's hand</returns>
        private Hand GetDefenderHand()
        {
            return theGame.getDefendingPlayer.myHand;
        }

        /// <summary>
        /// Checks if the defender can defend against a specific card
        /// </summary>
        /// <param name="attackCard">The card to attack with</param>
        /// <returns>True if defender can defend, false otherwise</returns>
        private bool CanDefenderDefend(Card attackCard)
        {
            Hand defenderHand = GetDefenderHand();

            for (int i = 0; i < defenderHand.GetCardCount; i++)
            {
                Card defenderCard = defenderHand[i];

                // Check if defender has same suit and higher rank
                if (defenderCard.getSuit == attackCard.getSuit && defenderCard > attackCard)
                {
                    return true;
                }

                // Check if defender has trump (when attack is not trump)
                if (defenderCard.getSuit == theGame.trumpCard.getSuit &&
                    attackCard.getSuit != theGame.trumpCard.getSuit)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Finds the best card to attack with based on perfect information
        /// </summary>
        /// <param name="playableCards">List of playable card indices</param>
        /// <returns>Index of best card to play</returns>
        private int FindBestAttackCard(List<int> playableCards)
        {
            // Strategy: Find cards the defender CANNOT defend against
            List<int> undefendableCards = new List<int>();

            foreach (int cardIndex in playableCards)
            {
                if (!CanDefenderDefend(myHand[cardIndex]))
                {
                    undefendableCards.Add(cardIndex);
                }
            }

            // If we have cards the defender can't defend, use the highest value one
            // to force them to pick up valuable cards
            if (undefendableCards.Count > 0)
            {
                int bestCard = undefendableCards[0];
                int highestValue = EvaluateCardValue(myHand[undefendableCards[0]]);

                for (int i = 1; i < undefendableCards.Count; i++)
                {
                    int currentValue = EvaluateCardValue(myHand[undefendableCards[i]]);
                    if (currentValue > highestValue)
                    {
                        highestValue = currentValue;
                        bestCard = undefendableCards[i];
                    }
                }

                return bestCard;
            }

            // If all cards can be defended, use the lowest value card
            // to minimize our loss
            int lowestCard = playableCards[0];
            int lowestValue = EvaluateCardValue(myHand[playableCards[0]]);

            for (int i = 1; i < playableCards.Count; i++)
            {
                int currentValue = EvaluateCardValue(myHand[playableCards[i]]);
                if (currentValue < lowestValue)
                {
                    lowestValue = currentValue;
                    lowestCard = playableCards[i];
                }
            }

            return lowestCard;
        }

        /// <summary>
        /// Determines if we should continue attacking or pass
        /// </summary>
        /// <param name="playableCards">Available cards to attack with</param>
        /// <returns>True to continue attacking, false to pass</returns>
        private bool ShouldContinueAttacking(List<int> playableCards)
        {
            if (playableCards.Count == 0)
                return false;

            // Don't attack if defender has very few cards and we have many
            // (save cards for later)
            if (GetDefenderHand().GetCardCount <= 2 && myHand.GetCardCount >= 5)
            {
                return false;
            }

            // Check if we can make defender pick up
            foreach (int cardIndex in playableCards)
            {
                if (!CanDefenderDefend(myHand[cardIndex]))
                {
                    return true; // Definitely attack if we can force pickup
                }
            }

            // If bout is already large, consider passing to end the round
            if (theGame.myBout.GetCardCount >= 8)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Attacks with perfect knowledge of defender's hand
        /// </summary>
        /// <returns>Index of card to play, or PASS</returns>
        internal override int Attack()
        {
            List<int> playableCards = theGame.playableAttackingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            // For initial attack
            if (theGame.myBout.GetCardCount == 0)
            {
                return FindBestAttackCard(playableCards);
            }

            // For follow-up attacks, decide if we should continue
            if (!ShouldContinueAttacking(playableCards))
            {
                return DurakGame.PASS;
            }

            return FindBestAttackCard(playableCards);
        }

        /// <summary>
        /// Counts how many cards in hand can defend against future attacks
        /// </summary>
        /// <returns>Number of defensive cards</returns>
        private int CountDefensiveCards()
        {
            int count = 0;

            for (int i = 0; i < myHand.GetCardCount; i++)
            {
                // Trump cards and high cards are defensive
                if (myHand[i].getSuit == theGame.trumpCard.getSuit ||
                    (int)myHand[i].getRank >= 10)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Determines whether to defend or pick up based on perfect information
        /// </summary>
        /// <param name="playableCards">Cards we can use to defend</param>
        /// <returns>True if we should defend, false to pick up</returns>
        private bool ShouldDefendWithPerfectInfo(List<int> playableCards)
        {
            if (playableCards.Count == 0)
                return false;

            // Calculate bout value
            int boutValue = 0;
            for (int i = 0; i < theGame.myBout.GetCardCount; i++)
            {
                boutValue += EvaluateCardValue(theGame.myBout[i]);
            }

            // Find cheapest defense
            int cheapestDefense = int.MaxValue;
            int cheapestDefenseIndex = playableCards[0];

            foreach (int cardIndex in playableCards)
            {
                int cardValue = EvaluateCardValue(myHand[cardIndex]);
                if (cardValue < cheapestDefense)
                {
                    cheapestDefense = cardValue;
                    cheapestDefenseIndex = cardIndex;
                }
            }

            // If defending costs much more than the bout value, pick up
            if (cheapestDefense > boutValue + 15)
            {
                return false;
            }

            // If we have very few defensive cards left and bout is cheap, pick up
            if (CountDefensiveCards() <= 2 && boutValue < 10)
            {
                return false;
            }

            // Late game: defend more aggressively
            if (theGame.myDeck.Empty)
            {
                // Only pick up if bout is extremely cheap
                return boutValue >= 5 || cheapestDefense <= 15;
            }

            // Default: defend if cost is reasonable
            return true;
        }

        /// <summary>
        /// Defends with perfect knowledge of game state
        /// </summary>
        /// <returns>Index of card to play, or PASS to pick up</returns>
        internal override int Defend()
        {
            List<int> playableCards = theGame.playableDefendingCards(myHand);

            if (playableCards.Count == 0)
                return DurakGame.PASS;

            // Decide whether to defend or pick up
            if (!ShouldDefendWithPerfectInfo(playableCards))
            {
                return DurakGame.PASS;
            }

            // Find the lowest value card that can defend
            int bestCard = playableCards[0];
            int lowestValue = EvaluateCardValue(myHand[playableCards[0]]);

            // Separate trump and non-trump cards
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

            // Prefer non-trump cards for defense
            if (nonTrumpCards.Count > 0)
            {
                bestCard = nonTrumpCards[0];
                for (int i = 1; i < nonTrumpCards.Count; i++)
                {
                    if (myHand[nonTrumpCards[i]] < myHand[bestCard])
                    {
                        bestCard = nonTrumpCards[i];
                    }
                }
            }
            else
            {
                // Use lowest trump card
                bestCard = trumpCards[0];
                for (int i = 1; i < trumpCards.Count; i++)
                {
                    if (myHand[trumpCards[i]] < myHand[bestCard])
                    {
                        bestCard = trumpCards[i];
                    }
                }
            }

            return bestCard;
        }

        #endregion
    }
}
