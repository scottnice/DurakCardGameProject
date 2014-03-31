using System;
namespace CardLibrary
{
    public class Card : ICloneable, IComparable
    {
        // max and min possible card values
        private const int MIN_VALUE = (int)Rank.TWO;
        private const int MAX_VALUE = (int)Rank.ACE;

        private Suit mySuit;
        private Rank myValue;
        private bool isFaceDown;

    #region "Properties"

        public Suit getSuit
        {
            get { return mySuit; }
        }

        public Rank getRank
        {
            get {return myValue; }
        }

        public bool IsFaceDown
        {
            get { return isFaceDown; }
            set { isFaceDown = value; }
        }

        public static bool operator >(Card me, Card other)
        {
            return me.getValue > other.getValue;
        }

        public static bool operator >=(Card me, Card other)
        {
            return me.getValue >= other.getValue;
        }

        public static bool operator <(Card me, Card other)
        {
            return me.getValue < other.getValue;
        }

        public static bool operator <=(Card me, Card other)
        {
            return me.getValue <= other.getValue;
        }

        /*public static bool operator ==(Card me, Card other)
        {
            return me.getValue == other.getValue;
        }

        public static bool operator !=(Card me, Card other)
        {
            return me.getValue != other.getValue;
        } */

    #endregion


    #region "Constructors"

        /// <summary>
        /// Constructs a card based on the cards suit, the cards value and whether 
        /// the card is face down or not. Defaults to face up.
        /// </summary>
        /// <param name="aSuit"></param>
        /// <param name="cardNum"></param>
        /// <param name="isFaceDown"></param>
        public Card(Suit aSuit, Rank cardNum, bool isFaceDown = false) 
        {
            mySuit = aSuit;
            myValue = cardNum;
            this.isFaceDown = isFaceDown;
        }
    #endregion

    #region "Methods"
        /// <summary>
        /// Overloaded compareto used in the icomparable interface. This is used
        /// to sort containers that have cards in them.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentException("Cannot compare card to null.");

            // sort cards by suit first and then by value from lowest to highest 
            Card aCard = obj as Card;
            if (aCard != null)
                return mySuit.CompareTo(aCard.mySuit)*100 + myValue.CompareTo(aCard.myValue);
            else
                throw new ArgumentException("Cannot compare card to non-card object.");
        }

        /// <summary>
        /// Creates and returns a memberwise clone of this card object.
        /// Used to implement the IClonable interface.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Checks if the two card objects are equal by comparing the suits
        /// and the card rank values. Whether the card is face down or not has no effect 
        /// on the comparison.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Card aCard = obj as Card;

            if (aCard == null)
                return false;

            return (aCard.getSuit.Equals(getSuit)) && (aCard.getRank.Equals(getRank));
        }

        /// <summary>
        /// Used to return a unique hash code for the values in this object.
        /// Gets a unique value by adding the suit * 100 to the value of the card
        /// and returns the hashcode of that value.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((int)mySuit * 100 + (int)myValue).GetHashCode();
        }
    }
    #endregion

}
