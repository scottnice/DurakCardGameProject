using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Durak
{
    class DurakGame
    {

        // Game Deck
        Deck myDeck;

        //Game Card Pile 
        Hand myHand = new Hand();

        //List of Players, to be init in constructor 
        List<GenericPlayer> myPlayers;

        //game over boolean
        protected bool isGameOver;

        public DurakGame(int numberOfCards, int numberOfPlayers ) // needed: Rules Enum
        {
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



        /* protected void dealCards(int numCards = 1)
        {
            if (numCards < 1)
                throw new ArgumentException("Cannot deal less than 1 card to each hand.");

            for (int j = numCards; j > 0; --j)
            {
                for (int i = 0; i < myPlayers.Count; ++i)
                    myDeck.deal(myPlayers[i].myHand);
            }
        }*/
    }
}
