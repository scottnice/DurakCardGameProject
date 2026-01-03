using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// This form is used to run and display the card game class objects.
    /// </summary>
    public partial class GameRoom : Form
    {
        // Constants
        const int NUMBER_OF_SUITS = 4;
        const int NUMBER_OF_RANKS = 13;
        const int CARD_WIDTH = 79;
        const int CARD_HEIGHT = 123;
        const int CARD_DRAW_OFFSET = 25;

        // get the image resource from the project
        private static Bitmap cardImageFile = new Bitmap("CardImages.png");
        
        private Point deckLocation;
        // the players seating positions for a max of 6 players
        private Point[] mySeats = new Point[6];
        private static DrawnCard[,] myDrawnCards;
        // an array of all the card images
        private static Bitmap[,] myCardImages = new Bitmap[NUMBER_OF_SUITS, NUMBER_OF_RANKS];
        // the image of a flipped card
        private Bitmap myFlippedCardImage;
        // the durak game object
        internal DurakGame myGame;

        #region "Event Handlers"

        // Used by the human player to pass 
        private void btnPass_Click(object sender, EventArgs e)
        {
            if (!myGame.IsAiGame)
            {
                myGame.validateHumanCard(DurakGame.PASS);
                this.Invalidate();
            }
            else
            {
                if (tmrFrameUpdate.Enabled)
                {
                    tmrFrameUpdate.Stop();
                    tmrGamePlay.Stop();
                }
                else
                {
                    tmrFrameUpdate.Start();
                    tmrGamePlay.Start();
                }
            }
        }

        /// <summary>
        /// Overrided on paint event repaints the form with the bitmaps
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            lblGameState.Text = myGame.getAttackingPlayer.name + " is attacking." 
                                + "\n" + myGame.getDefendingPlayer.name + " is defending."
                                    + "\n" + myGame.trumpCard.getSuit.ToString() + " is the trump suit.";
            base.OnPaint(e);

            // Create a local version of the graphics object for the PictureBox.
            Graphics gfx = e.Graphics;
            drawGame(gfx);
        }

        /// <summary>
        /// Card Click handler for the human players cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardClickHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Check if it is not an all ai game if the current player is a human, if the humans hand is not empty
            // then check that the cursor object e is inside the bounds of the humans cards if true 
            // than play the selected card
            if (!myGame.IsAiGame && myGame.currentPlayer == 0 && !myGame.myPlayers[0].myHand.Empty && e.X > mySeats[0].X
                && e.X < mySeats[0].X + myGame.myPlayers[0].myHand.GetCardCount * CARD_DRAW_OFFSET + CARD_WIDTH - CARD_DRAW_OFFSET 
                && e.Y > mySeats[0].Y && e.Y < mySeats[0].Y + CARD_HEIGHT)
            {
                int selectedCard;

                selectedCard = (e.X - mySeats[0].X) / CARD_DRAW_OFFSET;

                if (selectedCard > myGame.myPlayers[0].myHand.GetCardCount - 1)
                    selectedCard = myGame.myPlayers[0].myHand.GetCardCount - 1;

                myGame.validateHumanCard(selectedCard);
            }

        }


        /// <summary>
        /// Runs when the form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameRoom_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Beige;
            this.MouseClick += this.CardClickHandler;
        }

        /// <summary>
        /// Frame update timer tick event used to update the card images and animations, 
        /// for a consistent FPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrFrameUpdate_Tick(object sender, EventArgs e)
        {
            // get the cursors position
            Point point = Cursor.Position;

            if (!myGame.IsAiGame && myGame.currentPlayer == 0 && !myGame.myPlayers[0].myHand.Empty && point.X > mySeats[0].X
                && point.X < mySeats[0].X + myGame.myPlayers[0].myHand.GetCardCount * CARD_DRAW_OFFSET + CARD_WIDTH - CARD_DRAW_OFFSET
                && point.Y > mySeats[0].Y && point.Y < mySeats[0].Y + CARD_HEIGHT)

            {
                int selectedCard;

                selectedCard = (point.X - mySeats[0].X) / CARD_DRAW_OFFSET;

                if (selectedCard > myGame.myPlayers[0].myHand.GetCardCount - 1)
                    selectedCard = myGame.myPlayers[0].myHand.GetCardCount - 1;

                Card playerCard = myGame.myPlayers[0].myHand[selectedCard];
                if (myGame.attackingPlayer == 0)
                {
                    foreach (int validCard in myGame.playableAttackingCards(myGame.getAttackingPlayer.myHand))
                    {
                        if (selectedCard == validCard)
                        {
                            myDrawnCards[(int)playerCard.getSuit, (int)playerCard.getRank].MoveCard(mySeats[0].X + selectedCard * CARD_DRAW_OFFSET, mySeats[0].Y - 20);
                        }
                    }

                }
                else if (myGame.defendingPlayer == 0)
                {
                    if (!myGame.myBout.Empty)
                    {
                        foreach (int validCard in myGame.playableDefendingCards(myGame.getDefendingPlayer.myHand))
                        {
                            if (selectedCard == validCard)
                            {
                                myDrawnCards[(int)playerCard.getSuit, (int)playerCard.getRank].MoveCard(mySeats[0].X + selectedCard * CARD_DRAW_OFFSET, mySeats[0].Y - 20);
                            }
                        }
                    }
                }
            }

            foreach (DrawnCard c in myDrawnCards)
            {
                if (c != null)
                {
                    c.updatePosition();
                }
            }
            this.Invalidate();
        }

        /// <summary>
        /// Game play timer tick event this sets the speed of the game for the player and computers
        /// a computer will play a card each time this event occurs if it can and if it is the computers turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrGamePlay_Tick(object sender, EventArgs e)
        {
            if (myGame.IsAiGame)
                myGame.play();
            else if (myGame.currentPlayer != 0)
            {
                myGame.play();
            }

            if (myGame.IsGameOver)
            {
                tmrGamePlay.Stop();
                MessageBox.Show("Game is over " + myGame.getWinnerName + " has won the game");
                resetGame();
                tmrGamePlay.Start();
            }

        }

        // stop the timers when the form is closed
        private void GameRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrFrameUpdate.Stop();
            tmrGamePlay.Stop();
        }

        // Reposition the seats each time the forms size is changed
        private void GameRoom_SizeChanged(object sender, EventArgs e)
        {
            deckLocation = new Point(this.Width - CARD_WIDTH * 2, Height / 2);

            mySeats[0] = new Point((int)(100), this.Height - 200);
            mySeats[1] = new Point((int)(100), 100);
            mySeats[2] = new Point(400, 100);
            mySeats[3] = new Point(400, 1100);
            mySeats[4] = new Point(400, 1100);
            mySeats[5] = new Point(400, 1100);
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Loads the cards images from the png resource file to a bitmap array
        /// </summary>
        private void loadCardImages()
        {
            // declarations
            // used to crop the bitmap images
            //Rectangle rectCropArea;

            // location of the flipped card
            Rectangle flippedCardBox = new Rectangle(CARD_WIDTH * 2, CARD_HEIGHT * 4, CARD_WIDTH, CARD_HEIGHT);

            // set the flipped card image
            myFlippedCardImage = cardImageFile.Clone(flippedCardBox, cardImageFile.PixelFormat);

            // load all the rest of the face up images of the cards
            for (int i = 0; i < NUMBER_OF_SUITS; ++i)
            {
                for (int j = 0; j < NUMBER_OF_RANKS; ++j)
                {
                    Rectangle rectCropArea = new Rectangle(j * CARD_WIDTH, i * CARD_HEIGHT, CARD_WIDTH, CARD_HEIGHT);
                    myCardImages[i, j] = cardImageFile.Clone(rectCropArea, cardImageFile.PixelFormat);
                }
            }
        }

        /// <summary>
        /// Used to display all of the active drawn cards in their respective hands and positions
        /// </summary>
        /// <param name="gfx"></param>
        private void drawGame(Graphics gfx)
        {

            if (gfx == null)
                throw new ArgumentException("Cannot draw to a null Graphics object.");

            // Draw the first players cards always face up
            for (int i = 0; i < myGame.myPlayers[0].GetCardCount; ++i)
            {
                Card playerCard = myGame.myPlayers[0].myHand[i];

                // set where the card needs to go, because this happens each time the card gets closer
                // to the end location it behaves like a quadratic function slowing the card down as it
                // approaches the final location giving the animations a cool effect
                myDrawnCards[(int)playerCard.getSuit, (int)playerCard.getRank].MoveCard(mySeats[0].X + i * CARD_DRAW_OFFSET, mySeats[0].Y);

                // draw the image of the card at the current location of the card
                gfx.DrawImage(myDrawnCards[(int)playerCard.getSuit, (int)playerCard.getRank].cardImage,
                                myDrawnCards[(int)playerCard.getSuit, (int)playerCard.getRank].getPoint());
            }

            // Draw player 0 name above their hand (they are at bottom of screen)
            if (myGame.myPlayers[0].GetCardCount > 0)
            {
                using (Font font = new Font("Arial", 12, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    gfx.DrawString(myGame.myPlayers[0].name, font, brush, mySeats[0].X, mySeats[0].Y - 25);
                }
            }

            // then the computer ones
            for (int i = 1; i < myGame.myPlayers.Count; ++i)
            {
                Point p = mySeats[i];

                for (int j = 0; j < myGame.myPlayers[i].GetCardCount; ++j)
                {
                    Card currentCard = myGame.myPlayers[i].myHand[j];
                    myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].MoveCard(p.X + CARD_DRAW_OFFSET * j, p.Y);

                    // Draw face up if its an all AIGAME
                    if(myGame.IsAiGame)
                        gfx.DrawImage(myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].cardImage,
                            myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].getPoint());
                    else // if a human game draw the computers cards face down
                        gfx.DrawImage(myFlippedCardImage,
                            myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].getPoint());

                }

                // Draw player name below their hand (they are at top of screen)
                if (myGame.myPlayers[i].GetCardCount > 0)
                {
                    using (Font font = new Font("Arial", 12, FontStyle.Bold))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        gfx.DrawString(myGame.myPlayers[i].name, font, brush, p.X, p.Y + CARD_HEIGHT + 5);
                    }
                }
            }

            // draw all the cards in the bout
            for (int i = 0; i < myGame.myBout.GetCardCount; ++i)
            {
                Card currentCard = myGame.myBout[i];
                myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].MoveCard(this.Width/3 + (i / 2) * CARD_DRAW_OFFSET, this.Height/3 + 100 * ((i + 2) % 2));
                gfx.DrawImage(myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].cardImage,
                    myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank].getPoint());
            }

            // draw the cards in the discard pile as face down images
            for (int i = 0; i < myGame.discardPile.GetCardCount; ++i)
            {
                Card currentCard = myGame.discardPile[i];
                var drawnCard = myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank];
                drawnCard.MoveCard(0, this.Height / 2);
                gfx.DrawImage(myFlippedCardImage, drawnCard.getPoint());
            }

            // draw the deck as a face down card and the trump card, the trump card is the last card 
            // to be dealt from the deck so only draw that if the deck is not empty, and only draw the face
            // down card representing another card in the deck if there are more than 1 cards left in the deck
            if (!myGame.myDeck.Empty)
            {
                gfx.DrawImage(getCardImage(myGame.myDeck[0]), deckLocation.X, deckLocation.Y - CARD_HEIGHT / 2);

                if (myGame.myDeck.GetCardCount > 1)
                    gfx.DrawImage(myFlippedCardImage, deckLocation);
            }

        }

        /// <summary>
        /// Uses the cards rank and suit to get the image from the card image array
        /// </summary>
        /// <param name="aCard"></param>
        /// <returns></returns>
        internal static Bitmap getCardImage(Card aCard)
        {
            return myCardImages[(int)aCard.getSuit, (int)aCard.getRank];
        }


        // resets the game when the game ends
        private void resetGame()
        {
            myGame.resetGame();
            setDrawnCards();
            myGame.fillHands();
        }

        // used to setup the drawn cards and to reset them each time a new game is created
        private void setDrawnCards()
        {
            myDrawnCards = new DrawnCard[NUMBER_OF_SUITS, NUMBER_OF_RANKS];

            for (int i = 0; i < myGame.myDeck.GetCardCount; ++i)
            {
                Card currentCard = myGame.myDeck[i];
                myDrawnCards[(int)currentCard.getSuit, (int)currentCard.getRank] = new DrawnCard(deckLocation.X, deckLocation.Y, currentCard);
            }
        }

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a game room form based on the game type passed in
        /// </summary>
        /// <param name="theGame"></param>
        internal GameRoom(DurakGame theGame)
        {
            // load the card images to memory from the sprite sheet
            loadCardImages();
            
            InitializeComponent();
            
            this.WindowState = FormWindowState.Maximized;
            deckLocation = new Point(this.Width - CARD_WIDTH * 2, Height / 2);
            this.DoubleBuffered = true;

            myGame = theGame;

            if (myGame.IsAiGame)
                btnPass.Text = "Pause";
            else
                btnPass.Text = "End Turn";

            setDrawnCards();
            myGame.fillHands();

            tmrFrameUpdate.Start();
            tmrGamePlay.Start();
        }

        #endregion


    }
}
