
﻿using System;
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
        private List<Card> deck;

        // Maximum number of cards in this Deck
        private DeckSize myDeckSize;

        #region "Properties"

        /// <summary>
        /// Returns true if the deck is empty of cards.
        /// </summary>
        public bool Empty
        {
            get { return (deck.Count > 0 ? false : true); }
        }

        /// <summary>
        /// returns the number of cards in the deck at construction.
        /// </summary>
        public int GetDeckSize
        {
            get { return (int)myDeckSize; }
        }

        /// <summary>
        /// Returns the number of cards left in this deck.
        /// </summary>
        public int GetCardCount
        {
            get { return deck.Count; }
        }

        #endregion

        #region "Constructors"
        /// <summary>
        /// Constructs a deck based on the selected deck size.
        /// </summary>
        /// <param name="size"></param>
        public Deck(DeckSize size = DeckSize.FIFTY_TWO) 
        {
            deck = new List<Card>();
            generateDeck(size);
        }
        #endregion

        #region "Methods"
        /// <summary>
        /// Creates a deck using the selected deck size.
        /// </summary>
        /// <param name="size"></param>
        private void generateDeck(DeckSize size = DeckSize.FIFTY_TWO)
        {

            // the number of suits in a card deck
            const int NUMBER_SUITS = 4;
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

            // set the deck size field
            myDeckSize = size;

            // start with the lowest suit and add all values, than continue
            // doing the same with all other suits.
            for (int i = 0; i < NUMBER_SUITS; ++i)
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
            int randomNumber;

            for (int i = deck.Count-1; i > 0; --i)
            {
                randomNumber = randGenerator.Next(i);
          
                Functions.swap<Card>(deck, i, randomNumber);
            }
        }

        /// <summary>
        /// Empty and reset the deck
        /// </summary>
        private void clear()
        {
            // clear old deck
            deck.Clear();
        }

        /// <summary>
        /// Deal a card to a hand.
        /// </summary>
        /// <param name="aHand"></param>
        public void deal(Hand aHand)
        {
            if (deck.Count > 0)
            {
                aHand.myHand.Add(deck[deck.Count - 1]);
                deck.RemoveAt(deck.Count - 1);
            }
        }
        #endregion
    }
}
