using System;
namespace CardLibrary
{
    public class Card : ICloneable, IComparable
    {
        // the different suits a card can be
        public enum Suit { CLUBS, DIAMONDS, HEARTS, SPADES };
        public enum CardValue {TWO = 2, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE };

        // max and min possible card values
        private const int MIN_VALUE = (int)CardValue.TWO;
        private const int MAX_VALUE = (int)CardValue.ACE;

        public Suit mySuit; //public for now
        public CardValue myValue;

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

        public Card Clone()
        {
            return (Card)this.MemberwiseClone();
        }
    }
}
