using System;
using System.Collections.Generic;

namespace CardLibrary
{
    /// <summary>
    /// Represents a Deck of playing cards. Decks must be constructed using a DeckSize
    /// value of either 20, 36 or 52. The default DeckSize is 52 cards.
    /// </summary>
    public class Deck
    {
        // minimum card face values for each deck size.
        private const int TWENTY_LOW_VALUE = 10;
        private const int THIRTY_SIX_LOW_VALUE = 6;
        private const int FIFTY_TWO_LOW_VALUE = 2;

        // different deck sizes
        public enum DeckSize { TWENTY = 20, THIRTY_SIX = 36, FIFTY_TWO = 52 }

        // the list of cards the deck contains
        public List<Card> deck;

        // how many cards in this deck
        private DeckSize size;

        public Deck(DeckSize size = DeckSize.FIFTY_TWO) 
        {
            deck = new List<Card>();
            generateDeck(size);
        }

        public void generateDeck(DeckSize size = DeckSize.FIFTY_TWO)
        {
            int lowCard;
            
            if (size == DeckSize.TWENTY)
                lowCard = TWENTY_LOW_VALUE;   // 10 is the low value in a 20 card deck
            else if (size == DeckSize.THIRTY_SIX)
                lowCard = THIRTY_SIX_LOW_VALUE;    // 6 is the low value in a 36 card deck
            else if (size == DeckSize.FIFTY_TWO)
                lowCard = FIFTY_TWO_LOW_VALUE;    // 2 is the low value in a 52 card deck
            else
                throw new System.ArgumentException("Cannot create a deck of size " + (size.ToString()));
            
            // clear old deck
            clear();
            
            // now re-allocate memory
            deck.Capacity = (int)size;
            
            this.size = size;

            for (int i = (int)Suit.CLUBS; i <= (int)Suit.SPADES; ++i)
            {
                for (int j = lowCard; j <= (int)CardValue.ACE; ++j)
                {
                    deck.Add(new Card((Suit)i, (CardValue)j));
                }
            }
        }
        /// <summary>
        /// Shuffles the deck using the Fisher-Yates Shuffle Algorithm
        /// Source: http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// Author: Scott Nice
        /// Date: 09/01/2014
        /// </summary>
        public void shuffle()
        {
            Random randGenerator = new Random();
            int topEnd = (int)size;
            int randomNumber;

            for (int i = (int)size-1; i > 0; --i)
            {
                randomNumber = randGenerator.Next(topEnd);
                --topEnd;
          
                Functions.swap<Card>(deck, i, randomNumber);
            }
        }

        private void clear()
        {
            // clear old deck
            deck.Clear();
        }
    }
}
