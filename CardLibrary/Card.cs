using System;
namespace CardLibrary
{
    public class Card : ICloneable, IComparable
    {
        // max and min possible card values
        private const int MIN_VALUE = (int)CardValue.TWO;
        private const int MAX_VALUE = (int)CardValue.ACE;

        private Suit mySuit; //public for now
        private CardValue myValue;

    #region "Propeties"

        public Suit getSuit
        {
            get { return mySuit; }
        }

        public CardValue getValue
        {
            get {return myValue; }
        }


    #endregion


    #region "Constructors"
        public Card(Suit aSuit, CardValue cardNum) 
        {
            if ((int)cardNum >= MIN_VALUE && (int)cardNum <= MAX_VALUE)
            {
                mySuit = aSuit;
                myValue = cardNum;
            }
            else
                throw new System.ArgumentException("Card Value out of range", cardNum.ToString());
        }
    #endregion

    #region "Methods"
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentException("Cannot compare card to null.");

            Card aCard = obj as Card;
            if (aCard != null)
                return mySuit.CompareTo(aCard.mySuit)*100 + myValue.CompareTo(aCard.myValue);
            else
                throw new ArgumentException("Cannot compare card to non-card object.");
        }

        public object Clone()
        {
            return (Card)this.MemberwiseClone();
        }
    }
    #endregion

}
