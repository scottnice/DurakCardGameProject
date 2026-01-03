using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// abstract class that represents a game of durak has two 
    /// sub classes names PassingRules and BasicRules
    /// </summary>
    abstract class DurakGame
    {

        protected String winningPlayerName = "";

        // valued used to pass on a hand in durak
        internal const int PASS = -1;

        // Constants
        protected const int MIN_PLAYERS = 2;
        protected const int MAX_PLAYERS = 6;
        protected const int MIN_CARDS_PER_HAND = 6;
        protected const int MAX_CARDS_PER_BOUT = 12;

        // Game Deck
        internal Deck myDeck;

        //Game Card Pile 
        internal Hand myBout = new Hand();

        //List of Players, to be init in constructor 
        // THE HUMAN PLAYER IS ALWAYS AT ARRAY POSITION 0
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

        // flag used to tell if this is an AI game or not
        protected bool isAiGame;

        // indexes of the attacking and defending players
        internal protected int attackingPlayer;
        internal protected int defendingPlayer;

        // the index of the current player
        internal int currentPlayer;

        // flag used to tell if the game is in an attacking or a defending state
        protected bool isAttacking;

        #region "Properties"

        internal String getWinnerName
        {
            get { return winningPlayerName; }
        }

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

        internal bool IsGameOver
        {
            get { return isGameOver; }
        }

        #endregion

        #region "Constructors"
        /// <summary>
        /// Default Constructor for the game of durak
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="deckSize"></param>
        /// <param name="difficulty"></param>
        /// <param name="isAllAI"></param>
        internal DurakGame(int numberOfPlayers = MIN_PLAYERS, Deck.DeckSize deckSize = Deck.DeckSize.THIRTY_SIX,
                        ComputerPlayer.AIDifficulty difficulty = ComputerPlayer.AIDifficulty.Basic, bool isAllAI = false)
            : this(numberOfPlayers, deckSize, new ComputerPlayer.AIDifficulty[] { difficulty, difficulty }, isAllAI, false)
        {
        }

        /// <summary>
        /// Constructor for durak game with per-player AI difficulties
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="deckSize"></param>
        /// <param name="difficulties">Array of AI difficulties for each player</param>
        /// <param name="isAllAI"></param>
        internal DurakGame(int numberOfPlayers, Deck.DeckSize deckSize,
                        ComputerPlayer.AIDifficulty[] difficulties, bool isAllAI)
            : this(numberOfPlayers, deckSize, difficulties, isAllAI, true)
        {
        }

        /// <summary>
        /// Private constructor that does the actual initialization
        /// </summary>
        private DurakGame(int numberOfPlayers, Deck.DeckSize deckSize,
                        ComputerPlayer.AIDifficulty[] difficulties, bool isAllAI, bool usePerPlayerDifficulties)
        {
            // validate the number of players
            if (numberOfPlayers < MIN_PLAYERS || numberOfPlayers > MAX_PLAYERS)
                throw new ArgumentException("Number of players entered must be between "
                    +MIN_PLAYERS+" and "+MAX_PLAYERS+" you entered " 
                    + numberOfPlayers.ToString(),"numberOfPlayers");

            theDeckSize = deckSize;
            this.numberOfPlayers = numberOfPlayers;
            isAiGame = isAllAI;

            // If its an AI game load only AI's
            if (isAiGame)
            {
                for (int i = 0; i < numberOfPlayers; ++i)
                {
                    // Use per-player difficulty if available, otherwise use first difficulty for all
                    ComputerPlayer.AIDifficulty currentDifficulty = (usePerPlayerDifficulties && i < difficulties.Length)
                        ? difficulties[i]
                        : difficulties[0];

                    switch (currentDifficulty)
                    {
                        case ComputerPlayer.AIDifficulty.Basic:
                            myPlayers.Add(new BasicAI(this));
                            break;
                        case ComputerPlayer.AIDifficulty.Advanced:
                            myPlayers.Add(new AdvancedAI(this));
                            break;
                        case ComputerPlayer.AIDifficulty.Cheater:
                            myPlayers.Add(new CheaterAI(this));
                            break;
                    }

                    // Set name to show difficulty level first
                    myPlayers[i].name = currentDifficulty.ToString() + " Player " + (i + 1);
                }
            }
            else // load the player and then the AI's
            {
                myPlayers.Add(new HumanPlayer(this, "Human Player"));

                for (int i = 1; i < numberOfPlayers; ++i)
                {
                    switch (difficulties[0])
                    {
                        case ComputerPlayer.AIDifficulty.Basic:
                            myPlayers.Add(new BasicAI(this));
                            break;
                        case ComputerPlayer.AIDifficulty.Advanced:
                            myPlayers.Add(new AdvancedAI(this));
                            break;
                        case ComputerPlayer.AIDifficulty.Cheater:
                            myPlayers.Add(new CheaterAI(this));
                            break;
                    }
                }
            }

            // create a new deck
            myDeck = new Deck(theDeckSize);
            myDeck.shuffle();
            // set the trump card
            trumpCard = myDeck[0];

            // set game state and current attacking and defending players
            isAttacking = true;
            currentPlayer = 0;
            attackingPlayer = 0;
            defendingPlayer = 1;
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// This function is called each time a move is played to determine if the game has ended or not
        /// Displays the name of the winning player
        /// </summary>
        protected void playerWinsHandler()
        {
            // Check if deck is empty first
            if (myDeck.Empty)
            {
                // Check all players to see if any have won (first to empty their hand wins)
                for (int i = 0; i < myPlayers.Count; i++)
                {
                    if (myPlayers[i].GetCardCount == 0)
                    {
                        isGameOver = true;
                        winningPlayerName = myPlayers[i].name;

                        // if not an ai game record the player's stats against the computer
                        if (!IsAiGame && i == 0)
                            Utilities.UpdateStats(false);
                        else if (!IsAiGame)
                            Utilities.UpdateStats(true);

                        break; // Found a winner, stop checking
                    }
                }
            }
        }

        /// <summary>
        /// Validates the human player's cards to determine if the card the human played is
        /// valid at this time or not
        /// </summary>
        /// <param name="cardIndex"></param>
        internal virtual void validateHumanCard(int cardIndex)
        {
            if (currentPlayer == 0)
            {
                List<int> playableCards = null;
                // check if the player has passed
                if (cardIndex == PASS)
                {
                    ((HumanPlayer)myPlayers[0]).play(cardIndex);
                    play();
                }
                else if (attackingPlayer == 0)
                {
                    // zero is always the humans seat if he is attacking get all possible attacking cards
                    playableCards = playableAttackingCards(getAttackingPlayer.myHand);

                }
                else if (defendingPlayer == 0)
                {
                    // get all possible defending cards
                    playableCards = playableDefendingCards(getDefendingPlayer.myHand);
                }

                // check if the human has playable cards and that the card the human has 
                // picked is in this list
                if (playableCards != null && playableCards.Contains(cardIndex))
                {
                    // the card is valid select the card using the human players play function
                    ((HumanPlayer)myPlayers[0]).play(cardIndex);
                    // play the card 
                    play();
                }
            }
        }

        /// <summary>
        /// Logic that occurs when the defender loses or the attacker passes
        /// </summary>
        private void nextAttacker()
        {
            attackingPlayer = (attackingPlayer + 1) % myPlayers.Count;
            defendingPlayer = (defendingPlayer + 1)% myPlayers.Count;
            currentPlayer = attackingPlayer;
            isAttacking = true;
        }

        /// <summary>
        /// Logic that occurs when the defender successfully defends six cards
        /// </summary>
        private void defenderWins()
        {
            attackingPlayer = defendingPlayer;
            defendingPlayer = (attackingPlayer + 1) % myPlayers.Count;
            isAttacking = true;
            currentPlayer = attackingPlayer;
            // clear the bout to reset the game play
            myBout.giveCardsTo(discardPile);
        }

        /// <summary>
        /// Flips the game state from attacking to defending and vice versa
        /// </summary>
        private void flipAttacking()
        {
            isAttacking = !isAttacking;
        }

        /// <summary>
        /// Logic that occurs when the attacker passes his turn.
        /// </summary>
        private void attackerPass()
        {
            myBout.giveCardsTo(discardPile);
            fillHands();
            nextAttacker();
        }

        /// <summary>
        /// Logic that occurs when the defender loses and must pickup the cards.
        /// </summary>
        private void defenderPickup()
        {
            // pickup the cards and refill the hands
            pickup();
            fillHands();
        }

        /// <summary>
        /// Attacking logic gets the attacking players card and plays it against the bout,
        /// then swaps the game state to defending
        /// </summary>
        /// <returns></returns>
        private bool attack()
        {
            int attackingCard;
            bool isSuccessful;

            attackingCard = getAttackingPlayer.Attack();

            // check the attacking card is not the pass value
            if (attackingCard > PASS)
            {
                // update log
                Utilities.UpdateLog(getCurrentPlayer.name + " has successfully attacked with " + getAttackingPlayer.myHand[attackingCard].ToString());
                // play attacking card against bout
                getAttackingPlayer.myHand.giveCardTo(myBout, attackingCard);
                // attack was successful
                isSuccessful = true;
                // game is no longer in attacking state
                isAttacking = false;
                // check if a player has won
                playerWinsHandler();
                // set the current player to the defending player
                currentPlayer = defendingPlayer;
            }
            else // the attacker has passed
            {
                // update log
                Utilities.UpdateLog(getCurrentPlayer.name + " has passed on an attack.");
                attackerPass();
                isSuccessful=false;
            }

            return isSuccessful;
        }

        /// <summary>
        /// Defending logic gets the defending players card and plays it against the bout,
        /// then swaps the game state to defending
        /// </summary>
        /// <returns></returns>
        private bool defend()
        {
            // the index of the selected defending card
            int defendingCard;
            // variable to determine whether the defense was successful or not
            bool isSuccessful;

            // get the defending card index
            defendingCard = getDefendingPlayer.Defend();

            // check that the defender has not passed
            if (defendingCard > PASS)
            {
                // update log
                Utilities.UpdateLog(getCurrentPlayer.name + " has successfully defended with " + getDefendingPlayer.myHand[defendingCard].ToString());
                // play card against the bout
                getDefendingPlayer.myHand.giveCardTo(myBout, defendingCard);
                // defense was successful
                isSuccessful = true; 
                // game state is now attacking
                isAttacking = true;
                // check if any player has won 
                playerWinsHandler();
                // set the current player
                currentPlayer = attackingPlayer;

                // if the bout is at max size than the defender has successfully defended
                if (myBout.GetCardCount == MAX_CARDS_PER_BOUT)
                    defenderWins();
            }
            else
            {
                // update log
                Utilities.UpdateLog(getCurrentPlayer.name + " has failed to defend." );
                // defense fails switch game state and give cards to defender
                isAttacking = true;
                currentPlayer = attackingPlayer;
                isSuccessful = false;
                defenderPickup();
            }
            
            return isSuccessful;

        }

        /// <summary>
        /// Checks the game state and if the game is not over it checks whether the game is in an
        /// attacking or defending state and the current player attacks or defends based on the game state
        /// </summary>
        internal void play()
        {
            if (!isGameOver)
            {
                if (isAttacking)
                {
                    attack();
                }
                else
                {
                    defend();
                }
            }
        }

        // give cards from bout to the defender
        private void pickup()
        {
            myBout.giveCardsTo(myPlayers[defendingPlayer].myHand);
        }

        /// <summary>
        /// Deals cards until all players have 6 cards in hand or deck is empty
        /// </summary>
        internal void fillHands()
        {
            // the number of cards that need to be dealt
            int numberOfCardsToDeal = 0;

            // loop through each player and determine how many cards each of them needs
            foreach (GenericPlayer aPlayer in myPlayers)
            {
                if(aPlayer.GetCardCount < MIN_CARDS_PER_HAND)
                    numberOfCardsToDeal += MIN_CARDS_PER_HAND - aPlayer.myHand.GetCardCount;
            }

            // deal cards to each player that has less than the min cards per hand until
            // the number of cards to deal is 0
            for (int i = 0; i < myPlayers.Count && numberOfCardsToDeal > 0; )
            {
                // check if this player needs cards
                if (myPlayers[i].GetCardCount < MIN_CARDS_PER_HAND)
                {
                    // give this player a card
                    dealCards(myPlayers[i].myHand);
                    // reduce the number of cards to deal
                    --numberOfCardsToDeal;
                }

                // when at the end of the list reset counter to start of list otherwise increment it 
                if (i == myPlayers.Count - 1)
                    i = 0;
                else
                    ++i;
            }

            // after each deal if a human player is playing sort his hand to allow for 
            // easier card management
            if (!isAiGame)
                myPlayers[0].myHand.sortLowToHigh();
        }

        /// <summary>
        /// Deals a number of cards to a specified hand
        /// </summary>
        /// <param name="aHand"></param>
        /// <param name="numCards"></param>
        private void dealCards(Hand aHand, int numCards = 1)
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
        private void dealCards(int numCards = 1)
        {
            if (numCards < 1)
                throw new ArgumentException("Cannot deal less than 1 card to each hand.");

            for (int j = numCards; j > 0 && !myDeck.Empty; --j)
            {
                for (int i = 0; i < myPlayers.Count && !myDeck.Empty; ++i)
                    myDeck.deal(myPlayers[i].myHand);
            }
        }

        /// <summary>
        /// Resets the game and all the players hands
        /// </summary>
        internal void resetGame()
        {
            isGameOver = false;
            // create a new deck
            myDeck = new Deck(theDeckSize);
            myDeck.shuffle();
            // set the trump card
            trumpCard = myDeck[0];

            // set game state and current attacking and defending players
            isAttacking = true;
            currentPlayer = 0;
            attackingPlayer = 0;
            defendingPlayer = 1;

            myBout.clear();
            discardPile.clear();

            foreach (GenericPlayer p in myPlayers)
                p.myHand.clear();
        }

        #endregion
    }

}
