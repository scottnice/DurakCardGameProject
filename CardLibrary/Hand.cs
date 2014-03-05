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

        /// <summary>
        /// Removes cards from one hand and copies them to another.
        /// Scott Nice
        /// 05/03/2014
        /// </summary>
        /// <param name="aHand"></param>
        public void add(Hand aHand)
        {
            for (int i = 0; i < aHand.myHand.Count; ++i)
                add(aHand.myHand[i].Clone());
        }

        public void add(object obj)
        {
            Card aCard = obj as Card;
            if (aCard == null)
                throw new ArgumentException("Cannot add a none card type to a hand.");

            add(aCard);
        }

        public void add(Card aCard)
        {
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
