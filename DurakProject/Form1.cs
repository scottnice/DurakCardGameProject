using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CardLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int cord0, cord1, cord2, cord3;
        Rectangle rectCropArea;

        int[] theCards = new int[51];

        Deck aDeck = new Deck(Deck.DeckSize.FIFTY_TWO);
      
        PictureBox [] pictureArray = new PictureBox[6];

        static Random random = new Random();

        int currentNumbe;
        int currentSuit;

        const  int CARD_END_POSITION_X = 335;
        const int CARD_END_POSITION_Y = 205;

        static int leftMove = 0;
        static int topMove = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Image newImage = Image.FromFile(@"C:\Users\My Files\Documents\Visual Studio 2010\guiproposal\guiproposal\Resources\cards.png");
            Bitmap sourceBitmap = new Bitmap(newImage, 1027, 615); // make constants
            Graphics g = destination.CreateGraphics();


           // int randomNumber = random.Next(0, 12);
           // int randomSuit = random.Next(0, 3);

           /* if (cord0 == 1027)
            {
                cord0 = 0;
                cord1 += 123;
            }
            if (cord1 == 492 && cord0 == 237)
            {
                cord1 = 0;
                cord0 = 0;
            }*/

            //cord0 += 79;
            cord0 = (Convert.ToInt32(aDeck.deck[theCards[currentNumbe]].myValue) * 79) - 79;
            cord1 = Convert.ToInt32(aDeck.deck[theCards[currentNumbe]].mySuit) * 123;

            // cord0 = 711;
             //cord1 = 369;
            label1.Text = currentNumbe.ToString() + " " + 
               (aDeck.deck[theCards[currentNumbe]].myValue).ToString() + " " +
               (aDeck.deck[theCards[currentNumbe]].mySuit).ToString();
          //  MessageBox.Show(currentNumbe.ToString());
           // MessageBox.Show(aDeck.deck[theCards[currentNumbe]].myValue.ToString());
           // MessageBox.Show(aDeck.deck[theCards[currentNumbe]].theSuit.ToString());
            currentNumbe++;
          //  if (currentNumbe >= 13)
            {
             //   currentSuit++;
             //   currentNumbe = 0;
            }

            rectCropArea = new Rectangle(cord0, cord1, cord2, cord3);

            g.DrawImage(sourceBitmap, new Rectangle(0, 0, destination.Width, destination.Height),
    rectCropArea, GraphicsUnit.Pixel);

            //Good practice to dispose the System.Drawing objects when not in use.
            sourceBitmap.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cord0 = 0;
            cord1 = 0;
            cord2 = 79;
            cord3 = 123;

             for (int i = 0; i < theCards.Length; i++)
            {
                theCards[i] = i;
            }

          //  Shuffle(theCards);

            currentNumbe = 0;
            currentSuit = 0;

            Image newImage = Image.FromFile(@"C:\Users\My Files\Documents\Visual Studio 2010\guiproposal\guiproposal\Resources\cards.png");
            Bitmap sourceBitmap = new Bitmap(newImage, 1027, 615); // make constants

            for (int i = 0; i < pictureArray.Length; i++)
            {


                int randomNumber = random.Next(0, 12);
                int randomSuit = random.Next(0, 3);

                pictureArray[i] = new PictureBox();
                Controls.Add(pictureArray[i]);

                // set position and size
                pictureArray[i].Location = new Point(100 * i + 100, 50);
                pictureArray[i].Size = new Size(80, 125);

                pictureArray[i].Click += new EventHandler(CardClickHandler);

                cord0 = randomNumber * 79;
                cord1 = randomSuit * 123;

                rectCropArea = new Rectangle(cord0, cord1, cord2, cord3);
                pictureArray[i].Image = (Image)sourceBitmap.Clone(rectCropArea, sourceBitmap.PixelFormat);
                
            }
            sourceBitmap.Dispose();        
        }

        private void CardClickHandler(object sender, System.EventArgs e)
        {
            if (!timer1.Enabled)
            {
                var help = (PictureBox)sender;
               // MessageBox.Show(help.Left.ToString());
                CalculateMovement(help.Left, help.Top);
                timer1.Tag = help;
                timer1.Enabled = true;
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            var help = (PictureBox)timer1.Tag;

            if (help.Left < CARD_END_POSITION_X)
            {
                if (help.Top >= CARD_END_POSITION_Y)
                {
                    help.Top = CARD_END_POSITION_Y;
                }
                if (help.Left >= CARD_END_POSITION_X)
                {
                    help.Left = CARD_END_POSITION_X;
                }
            }
            else
            {
                if (help.Top >= CARD_END_POSITION_Y)
                {
                    help.Top = CARD_END_POSITION_Y;
                }
                if (help.Left <= CARD_END_POSITION_X)
                {
                    help.Left = CARD_END_POSITION_X;
                }
            }

            help.Left += leftMove;
            help.Top += topMove;

            if (help.Top == CARD_END_POSITION_Y && help.Left == CARD_END_POSITION_X)
            {    
                timer1.Enabled = false;

            }
        }

        public static void Shuffle(int[] array)
        {
            for (int i = array.Length; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                // Swap.
                int tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }

        public static void CalculateMovement(int x, int y)
        {
            leftMove = (CARD_END_POSITION_X - x) / 5;
            topMove = (CARD_END_POSITION_Y - y) / 5;
        }
    }
}
