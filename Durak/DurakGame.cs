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

        protected GenericPlayer attackingPlayer;
        protected GenericPlayer defendingPlayer;
        protected int playersTurnIndex;

        //public int getAttackingPlayerIndex
        //{
        //    get { return attackingPlayer; }
        //}

        //public int getDefendingPlayerIndex
        //{
        //    get { return defendingPlayer; }
        //}


        public bool IsAiGame
        {
            get { return isAiGame; }
        }

        internal DurakGame(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.FIFTY_TWO, 
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
        {
            if (numberOfPlayers < MIN_PLAYERS || numberOfPlayers > MAX_PLAYERS)
                throw new ArgumentException("Number of players entered must be between "+MIN_PLAYERS+" and "+MAX_PLAYERS+" you entered " 
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
                myPlayers.Add(new HumanPlayer());

                for (int i = 1; i < numberOfPlayers; ++i)
                {
                    switch (difficulty)
                    {
                        case ComputerPlayer.AIDifficulty.Basic:
                            myPlayers.Add(new BasicAI(this));
                            break;
                    }
                }
            }

            myDeck = new Deck(theDeckSize);
            myDeck.shuffle();
            trumpCard = myDeck[0];
            dealCards(6);

            attackingPlayer = myPlayers[0];
            defendingPlayer = myPlayers[1];

        }

        internal void play()
        {

            attackingPlayer.myHand.giveCardTo(myBout, attackingPlayer.Attack());

            defendingPlayer.myHand.giveCardTo(myBout, defendingPlayer.Defend());

            Functions.swap<GenericPlayer>(ref attackingPlayer, ref defendingPlayer);

            dealCards(attackingPlayer.myHand, 6 - attackingPlayer.myHand.GetCardCount);
            dealCards(defendingPlayer.myHand, 6 - attackingPlayer.myHand.GetCardCount);
            


        }

        internal void dealCards(Hand aHand, int numCards = 1)
        {
            for (int i = 0; i < numCards; ++i)
            {
                myDeck.deal(aHand);
            }
        }

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
