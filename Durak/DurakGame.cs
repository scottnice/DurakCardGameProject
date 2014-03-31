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

        // Game Deck
        protected Deck myDeck;

        //Game Card Pile 
        public Hand myHand = new Hand();

        //List of Players, to be init in constructor 
        protected List<GenericPlayer> myPlayers;

        //game over boolean
        protected bool isGameOver = false;

        protected int attackingPlayer;
        protected int defendingPlayer;

        public DurakGame(Deck theDeck, List<GenericPlayer> thePlayers) // needed: Rules Enum
        {

            myPlayers = thePlayers;
            myDeck = theDeck;
            attackingPlayer = 0;
            defendingPlayer = 1;
           //Initializations
 
            //init Deck with numberOfCards
            //init myPlayers with numberOfPlayers

            //set starting player
            //set defending player

            /*
                if(rules == Rules.basic)
             * {
             *      instantiate and init basic rules class
             *      BasicRules.play();
             * 
             * }
             * else
             * {
             *      instantiate and init passing rules class
             *      PassingRules.play();
             * }
             
             
             */

        }

        //public abstract virtual void Play();

        public void dealCards(int numCards = 1)
        {
            if (numCards < 1)
                throw new ArgumentException("Cannot deal less than 1 card to each hand.");

            for (int j = numCards; j > 0 && !myDeck.Empty; --j)
            {
                for (int i = 0; i < myPlayers.Count; ++i)
                    myDeck.deal(myPlayers[i].myHand);
            }
        }
    }
}
