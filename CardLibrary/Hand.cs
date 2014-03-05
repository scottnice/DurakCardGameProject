using System;
using System.Collections.Generic;

namespace CardLibrary
{
    /// <summary>
    /// Represents a hand of playing cards. A hand of cards is basically a list of 
    /// cards but cannot shuffle or create cards on its own without influence from a deck
    /// class.
    /// </summary>
    public class Hand
    {
        private List<Card> myHand;
        // starting size of a durak hand
        private const int STARTING_SIZE = 6;

        public Hand()
        {
            myHand = new List<Card>();
            myHand.Capacity = STARTING_SIZE;
        }

        public void add(Card aCard)
        {
            myHand.Add(aCard);
        }

        public void add(object obj)
        {
            Card aCard = obj as Card;

            if (aCard == null)
                throw new ArgumentException("Cannot add a non Card type to a hand type.");

            myHand.Add(aCard);
        }

        public void clear()
        {
            myHand.Clear();
        }

        public void sortLowToHigh()
        {
            myHand.Sort();
        }

    }
}
