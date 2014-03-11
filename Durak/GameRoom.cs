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
    public partial class GameRoom : Form
    {
        // Constants
        const int NUMBER_OF_SUITS = 4;
        const int NUMBER_OF_RANKS = 13;
        const int CARD_WIDTH = 79;
        const int CARD_HEIGHT = 123;

        // number of players in this game
        readonly int NUMBER_OF_PLAYERS;

        // determines whether the game is AI only
        private bool isAiGame;

        // the size of the deck for this game of durak
        Deck.DeckSize myDeckSize;

        int[] theCards = new int[51];

        // the playing card deck
        Deck myDeck;

        // the players list
        List<GenericPlayer> myPlayers = new List<GenericPlayer>();

        // the players seating positions for a max of 6 players
        private Point[] mySeats = new Point[6];

        // the picture box that is used to draw the cards
        private PictureBox pbDrawArea = new PictureBox();

        // the discarded cards
        private Hand myDiscardPile = new Hand();

        // the cards currently in play
        private Hand myPlayedCards = new Hand();
        
        // an array of all the card images
        private Bitmap[,] myCardImages = new Bitmap[NUMBER_OF_SUITS, NUMBER_OF_RANKS];
        // the image of a flipped card
        private Bitmap myFlippedCardImage;

        const int CARD_END_POSITION_X = 335;
        const int CARD_END_POSITION_Y = 205;

        static int leftMove = 0;
        static int topMove = 0;

        int currentCard = 0;

        static int testnum = 0;

        #region "Event Handlers"

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    pbDrawArea.Refresh();
        //    base.OnPaint(e);
        //}

        private void pbDrawArea_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics gfx = e.Graphics;
            drawPlayers(gfx);
            gfx.DrawImage(myFlippedCardImage, new Point(this.Width - 150, this.Height / 2));
            if (!myDeck.Empty)
                gfx.DrawImage(getCardImage(myDeck[0]), new Point(this.Width - 150, this.Height / 2 - 100));
        }

        private void CardClickHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if(!myPlayers[0].myHand.Empty && e.X > mySeats[0].X && e.X < mySeats[0].X + myPlayers[0].myHand.GetCardCount*25 + CARD_WIDTH - 25 && e.Y > mySeats[0].Y && e.Y < mySeats[0].Y + CARD_HEIGHT)
            {
                int selectedCard;

                selectedCard = (e.X - mySeats[0].X) / 25;

                if (selectedCard > myPlayers[0].myHand.GetCardCount - 1)
                    selectedCard = myPlayers[0].myHand.GetCardCount - 1;

                myPlayers[0].myHand.giveCardTo(myDiscardPile, selectedCard);

                pbDrawArea.Invalidate();
            }

            //if (!timer1.Enabled)
            //{
            //    var help = (PictureBox)sender;
            //    // MessageBox.Show(help.Left.ToString());
            //    testnum++;
            //    CalculateMovement(help.Left, help.Top);
            //    timer1.Tag = help;
            //    timer1.Enabled = true;

            //}
        }


        /// <summary>
        /// Runs when the form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameRoom_Load(object sender, EventArgs e)
        {
             // Dock the PictureBox to the form and set its background to white.
            pbDrawArea.Dock = DockStyle.Fill;
            pbDrawArea.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            pbDrawArea.Paint += new System.Windows.Forms.PaintEventHandler(this.pbDrawArea_Paint);
            pbDrawArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CardClickHandler);

            // Add the PictureBox control to the Form. 
            this.Controls.Add(pbDrawArea);
            loadCardImages();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            var help = (PictureBox)timer1.Tag;

            help.Left += leftMove;
            help.Top += topMove;

            if (help.Top == CARD_END_POSITION_Y && help.Left == 100 * testnum)
            {
                timer1.Enabled = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Image newImage = Durak.Properties.Resources.CardImages;
            //Bitmap sourceBitmap = new Bitmap(newImage, 1027, 615); // make constants


            //PictureBox newPictureBox = new PictureBox();
            //Controls.Add(newPictureBox);

            //// set position and size
            //newPictureBox.Location = new Point(100 * currentCard + 100, 355);
            //newPictureBox.Size = new Size(80, 125);

            //newPictureBox.Click += new EventHandler(CardClickHandler);

            //cord0 = Convert.ToInt32(aDeck.deck[theCards[currentCard]].getValue) * 79;
            //cord1 = Convert.ToInt32(aDeck.deck[theCards[currentCard]].getSuit) * 123;


            ////  MessageBox.Show(aDeck.deck[theCards[currentCard]].getValue.ToString());
            //// MessageBox.Show(aDeck.deck[theCards[currentCard]].getSuit.ToString());

            //rectCropArea = new Rectangle(cord0, cord1, cord2, cord3);
            //newPictureBox.Image = (Image)sourceBitmap.Clone(rectCropArea, sourceBitmap.PixelFormat);

            //currentCard++;
            //sourceBitmap.Dispose();
        }

        #endregion

        #region "Methods"
        private void loadCardImages()
        {
           
            // declarations
            // used to crop the bitmap images
            Rectangle rectCropArea;

            // location of the flipped card
            Rectangle flippedCardBox = new Rectangle(CARD_WIDTH*2, CARD_HEIGHT*4, CARD_WIDTH, CARD_HEIGHT);

            // get the image resource from the project
            Bitmap cardImageFile = Durak.Properties.Resources.CardImages;

            // set the flipped card image
            myFlippedCardImage = cardImageFile.Clone(flippedCardBox, cardImageFile.PixelFormat);

            // load all the rest of the face up images of the cards
            for (int i = 0; i < NUMBER_OF_SUITS; ++i)
            {
                for (int j = 0; j < NUMBER_OF_RANKS; ++j)
                {
                    rectCropArea = new Rectangle(j * CARD_WIDTH, i * CARD_HEIGHT, CARD_WIDTH, CARD_HEIGHT);

                    myCardImages[i, j] = cardImageFile.Clone(rectCropArea, cardImageFile.PixelFormat);
                }
            }
        }

        private void drawPlayers(Graphics gfx)
        {
            if (gfx == null)
                throw new ArgumentException("Cannot draw to a null Graphics object.");

            // draw the human player first
            for(int i = 0; i < myPlayers[0].GetCardCount; ++i)
            {
                Card playerCard = myPlayers[0].myHand[i];
                
                gfx.DrawImage(getCardImage(playerCard), mySeats[0].X + i * 25, mySeats[0].Y);
            }

            // then the computer ones
            for (int i = 1; i < myPlayers.Count; ++i)
            {
                gfx.DrawImage(myFlippedCardImage, mySeats[i]);
            }

        }

        /// <summary>
        /// Uses the cards rank and suit to get the image from the card image array
        /// </summary>
        /// <param name="aCard"></param>
        /// <returns></returns>
        private Bitmap getCardImage(Card aCard)
        {
            return myCardImages[(int)aCard.getSuit, (int)aCard.getValue];
        }


        public static void CalculateMovement(int x, int y)
        {
            leftMove = ((100 * testnum) - x) / 5;
            topMove = (CARD_END_POSITION_Y - y) / 5;
        }

        #endregion

        #region "Constructors"

        public GameRoom(int numPlayers = 2, Deck.DeckSize theDeckSize = Deck.DeckSize.FIFTY_TWO, bool isAiGame = false /*Difficulty AiDiff = Basic, DurakRules Basic */)
        {
            InitializeComponent();
            NUMBER_OF_PLAYERS = numPlayers;
            myDeckSize = theDeckSize;

            myDeck = new Deck(myDeckSize);
            myDeck.shuffle();
            mySeats[0] = new Point((int)(this.Width / 3.0 - 200), this.Height - 100);
            mySeats[1] = new Point(200, 100);
            mySeats[2] = new Point(400, 100);
            mySeats[3] = new Point(400, 1100);
            mySeats[4] = new Point(400, 1100);
            mySeats[5] = new Point(400, 1100);

            myPlayers.Add(new HumanPlayer(mySeats[0]));
            myDeck.deal(myPlayers[0].myHand);
            for (int i = 0; i < 50; ++i )
                myDeck.deal(myPlayers[0].myHand);

                // set the seating arrangements
                for (int i = 1; i < NUMBER_OF_PLAYERS; ++i)
                {
                    myPlayers.Add(new ComputerPlayer(mySeats[i], myDeck));
                }

        }

        #endregion


        //Needed events:


        //Card click

        


    }
}
