using System;
using System.Collections.Generic;

namespace CardLibrary
{
    /// <summary>
    /// Represents a hand of playing cards. A hand of cards is basically a list of 
    /// cards but cannot shuffle or create cards on its own without influence from a deck
    /// class or another hand, this is to keep the creation of cards limited to the deck
    /// so the hands cannot create copies of their owns cards accidentally.
    /// </summary>
    public class Hand
    {
        internal List<Card> myHand;

        // starting size of a durak hand
        private const int STARTING_SIZE = 6;

        #region "Constructors"

        public Hand()
        {
            myHand = new List<Card>();
            myHand.Capacity = STARTING_SIZE;
        }

        #endregion

        
        #region "Properties"

        public bool Empty
        {
            get { return (myHand.Count > 0 ? false : true); }
        }

        /// <summary>
        /// Indexer for the hand class.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Card this[int index]
        {
            get { return myHand[index]; }
        }

        /// <summary>
        /// Returns the number of cards in this hand.
        /// </summary>
        public int GetCardCount
        {
            get { return myHand.Count; }
        }
     
        #endregion


        #region "Methods"

        /// <summary>
        /// Gives all cards to another hand and then
        /// clears all the cards from this hand.
        /// </summary>
        /// <param name="aHand"></param>
        public void giveCardsTo(Hand aHand)
        {
            aHand.myHand.AddRange(myHand);
            clear();
        }

        public void giveCardTo(Hand aHand, int index)
        {
            aHand.myHand.Add(myHand[index]);
            myHand.RemoveAt(index);
        }

        /// <summary>
        /// clears all cards from this hand.
        /// </summary>
        public void clear()
        {
            myHand.Clear();
        }

        /// <summary>
        /// Sorts all cards in this hand by suit 
        /// and then by rank from lowest to highest.
        /// </summary>
        public void sortLowToHigh()
        {
            myHand.Sort();
        }

        #endregion


    }
}
