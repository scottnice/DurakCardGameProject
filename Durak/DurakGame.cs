using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    abstract class DurakGame
    {
        // Constants
        protected const int MIN_PLAYERS = 2;
        protected const int MAX_PLAYERS = 6;
        protected const int MIN_CARDS_PER_HAND = 6;

        // Game Deck
        internal Deck myDeck;

        //Game Card Pile 
        internal Hand myBout = new Hand();

        //List of Players, to be init in constructor 
        internal List<GenericPlayer> myPlayers = new List<GenericPlayer>();

        //game over boolean
        protected bool isGameOver = false;

        // trump card for deck
        internal Card trumpCard;

        internal Hand discardPile = new Hand();

        public abstract List<int> playableAttackingCards(Hand playerHand);

        public abstract List<int> playableDefendingCards(Hand playerHand);

        internal int numberOfPlayers;

        internal Deck.DeckSize theDeckSize;

        protected bool isAiGame;

        protected int attackingPlayer;
        protected int defendingPlayer;

        protected bool isAttacking;
        protected int currentPlayer;

        public GenericPlayer getCurrentPlayer
        {
            get { return myPlayers[currentPlayer]; }
        }

        public GenericPlayer getAttackingPlayer
        {
            get { return myPlayers[attackingPlayer]; }
        }

        public GenericPlayer getDefendingPlayer
        {
            get { return myPlayers[defendingPlayer]; }
        }

        public bool IsAiGame
        {
            get { return isAiGame; }
        }

        internal DurakGame(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.FIFTY_TWO, 
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
        {
            if (numberOfPlayers < MIN_PLAYERS || numberOfPlayers > MAX_PLAYERS)
                throw new ArgumentException("Number of players entered must be between "
                    +MIN_PLAYERS+" and "+MAX_PLAYERS+" you entered " 
                    + numberOfPlayers.ToString(),"numberOfPlayers");

            theDeckSize = deckSize;
            this.numberOfPlayers = numberOfPlayers;
            isAiGame = isAllAI;

            if (isAiGame)
            {
                for (int i = 0; i < numberOfPlayers; ++i)
                {
                    switch (difficulty)
                    {
                        case ComputerPlayer.AIDifficulty.Basic:
                            myPlayers.Add(new BasicAI(this));
                            break;
                    }
                }
            }
            else
            {
                myPlayers.Add(new HumanPlayer(this));

                for (int i = 1; i < numberOfPlayers; ++i)
                {
                    switch (difficulty)
                    {
                        case ComputerPlayer.AIDifficulty.Basic:
                            myPlayers.Add(new BasicAI(this));
                            break;
                        case ComputerPlayer.AIDifficulty.Advanced:
                            // add stuff here
                            break;
                        case ComputerPlayer.AIDifficulty.Cheater:
                            // add stuff here
                            break;
                    }
                }
            }

            myDeck = new Deck(theDeckSize);
            myDeck.shuffle();
            trumpCard = myDeck[0];
            fillHands();

            isAttacking = true;
            currentPlayer = 0;
            attackingPlayer = 0;
            defendingPlayer = 1;

        }


        internal virtual void validateHumanCard(int cardIndex)
        {
            if(cardIndex == -1)
            {    
                ((HumanPlayer)myPlayers[0]).play(cardIndex);
                play();
            }
            else if(attackingPlayer == 0)
            {
                List<int> playableCards = playableAttackingCards(getAttackingPlayer.myHand);

                if(playableCards.Count > 0)
                {
                    for(int i = 0; i < playableCards.Count; ++i)
                    {
                        if(cardIndex == playableCards[i])
                        {
                            ((HumanPlayer)myPlayers[0]).play(cardIndex);
                            play();
                        }
                    }
                }

            }
            else if(defendingPlayer == 0)
            {
                List<int> playableCards = playableDefendingCards(getDefendingPlayer.myHand);

                if(playableCards.Count > 0)
                {
                    for(int i = 0; i < playableCards.Count; ++i)
                    {
                        if(cardIndex == playableCards[i])
                        {
                            ((HumanPlayer)myPlayers[0]).play(cardIndex);
                            play();
                        }
                    }
                }
            }

        }

        private void nextAttacker()
        {
            attackingPlayer = (attackingPlayer + 1) / myPlayers.Count;
            defendingPlayer = (defendingPlayer + 1)/ myPlayers.Count;\
            currentPlayer = attackingPlayer;
        }

        private void attackerPass()
        {
            myBout.giveCardsTo(discardPile);
            fillHands();
            nextAttacker();
        }

        private void defenderPickup()
        {
            pickup();
            fillHands();
        }

        internal void play()
        {
            int attackingCard;
            int defendingCard;

            if (isAttacking)
            {
                if (isAiGame || attackingPlayer != 0)
                {
                    attackingCard = getAttackingPlayer.Attack();

                    if (attackingCard >= 0)
                        getAttackingPlayer.myHand.giveCardTo(myBout, attackingCard);
                    else
                        attackerPass();

                    // check if next player is a computer
                    if (isAiGame || defendingPlayer != 0)
                    {
                        // defender is computer player
                        defendingCard = getDefendingPlayer.Defend();

                        if(defendingCard >= 0)
                            getDefendingPlayer.myHand.giveCardTo(myBout, defendingCard);
                        else
                            defenderPickup();
                    }
                }
            }
            else
            {

            }

        }

        internal void pickup()
        {
            myBout.giveCardsTo(myPlayers[defendingPlayer].myHand);
        }

        /// <summary>
        /// Deals cards until all players have 6 cards in hand or deck is empty
        /// </summary>
        internal void fillHands()
        {
            int numberOfCardsToDeal = 0;

            foreach (GenericPlayer aPlayer in myPlayers)
            {
                numberOfCardsToDeal += MIN_CARDS_PER_HAND - aPlayer.myHand.GetCardCount;
            }

            for (int i = 0; i < myPlayers.Count && numberOfCardsToDeal > 0; )
            {
                if (myPlayers[i].GetCardCount < MIN_CARDS_PER_HAND)
                {
                    dealCards(myPlayers[i].myHand);
                    --numberOfCardsToDeal;
                }

                if (i == myPlayers.Count - 1)
                    i = 0;
                else
                    ++i;
            }
        }

        /// <summary>
        /// Deals a number of cards to a specified hand
        /// </summary>
        /// <param name="aHand"></param>
        /// <param name="numCards"></param>
        internal void dealCards(Hand aHand, int numCards = 1)
        {
            for (int i = 0; i < numCards; ++i)
            {
                myDeck.deal(aHand);
            }
        }

        /// <summary>
        /// Deals a number of cards to all players
        /// </summary>
        /// <param name="numCards"></param>
        internal void dealCards(int numCards = 1)
        {
            if (numCards < 1)
                throw new ArgumentException("Cannot deal less than 1 card to each hand.");

            for (int j = numCards; j > 0 && !myDeck.Empty; --j)
            {
                for (int i = 0; i < myPlayers.Count && !myDeck.Empty; ++i)
                    myDeck.deal(myPlayers[i].myHand);
            }
        }
    }
}
