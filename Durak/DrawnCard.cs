using System;
using System.Drawing;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// This class is the visual representation of a playing card in the deck.
    /// It is used to update the movements and draw the card to the form.
    /// </summary>
    public class DrawnCard
    {
        // Cards width in pixels
        const int CARD_WIDTH = 79;
        // cards height in pixels
        const int CARD_HEIGHT = 123;
        // number of updates it takes to complete an animation
        const int UPDATES_PER_MOVE = 5;
        // the minimum distance a card has to get to its final location for 
        // the card to instantly snap to the location
        const int CARD_SNAP_DISTANCE = 5;
        
        // The current X position of the control
        protected int xPosition;
        // The current Y position of the control
        protected int yPosition;
        // The speed the control is moving in the X axis
        protected int xMove;
        // The speed the control is moving in the Y axis
        protected int yMove;
        // whether the card is faceup
        protected bool faceUp;
        // the image linked to this card
        public Bitmap cardImage; 
        // the cards face down image
        public static Bitmap faceDownImage; 

        private int xEnd;
        private int yEnd;

        // reference to the represented card
        public readonly Card theCard;

        /// <summary>
        /// Property to get set the x position for the left side of the card
        /// </summary>
        public int Left
        {
            get
            {
                return xPosition;
            }
            set
            {
                xPosition = value;
            }
        }

        /// <summary>
        /// Property to get or set the Y position for the top of the card.
        /// </summary>
        public int Top
        {
            get
            {
                return yPosition;
            }
            set
            {
                yPosition = value;
            }
        }

        /// <summary>
        /// gets or sets the speed the card moves in the x direction
        /// </summary>
        public int LeftMove
        {
            get
            {
                return xMove;
            }
            set
            {
                xMove = value;
            }
        }

        /// <summary>
        /// gets or sets the speed the card moves in the y position
        /// </summary>
        public int TopMove
        {
            get
            {
                return yMove;
            }
            set
            {
                yMove = value;
            }
        }

        /// <summary>
        /// Property to return whether the card is faceup or not
        /// </summary>
        public bool FaceUp
        {
            get
            {
                return faceUp;
            }
            set
            {
                faceUp = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="aCard"></param>
        public DrawnCard(int x, int y, Card aCard)
        {
            xPosition = x;
            yPosition = y;
            xEnd = x;
            yEnd = y;
            faceUp = false;
            theCard = aCard;
            // get the image associated with this card
            cardImage = GameRoom.getCardImage(aCard);
        }

        /// <summary>
        /// Sets the speed that the card moves
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        public void MoveCard(int newX, int newY)
        {
            xEnd = newX;
            yEnd = newY;
            LeftMove = (xEnd - Left) / UPDATES_PER_MOVE;
            TopMove = (yEnd - Top) / UPDATES_PER_MOVE;
        }

        /// <summary>
        /// Returns a point representing the x and y position of the cards top left corner
        /// </summary>
        /// <returns></returns>
        public Point getPoint()
        {
            return new Point(Left,Top);
        }

        /// <summary>
        /// Updates the position of the card by moving the card towards the end location
        /// </summary>
        public void updatePosition()
        {
            if (Math.Abs(Left - xEnd) >= CARD_SNAP_DISTANCE)
            {
                Left += LeftMove;
            }
            else
            {
                Left = xEnd;
            }
            if (Math.Abs(Top - yEnd) >= CARD_SNAP_DISTANCE)
            {
                Top += TopMove;
            }
            else
            {
               Top = yEnd;
            }
        }
    }
}
