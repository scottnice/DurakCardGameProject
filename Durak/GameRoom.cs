
﻿using System;
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

        int cord0, cord1, cord2, cord3;
        Rectangle rectCropArea;

        int[] theCards = new int[51];

        Deck aDeck = new Deck(Deck.DeckSize.FIFTY_TWO);

        PictureBox[] pictureArray = new PictureBox[6];

        static Random random = new Random();

        //   int currentNumbe;
        // int currentSuit;

        const int CARD_END_POSITION_X = 335;
        const int CARD_END_POSITION_Y = 205;

        static int leftMove = 0;
        static int topMove = 0;

        int currentCard = 0;

        static int testnum = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Draw all hands

            cord0 = 0;
            cord1 = 0;
            cord2 = 79;
            cord3 = 123;

            for (int i = 0; i < theCards.Length; i++)
            {
                theCards[i] = i;
            }
            aDeck.shuffle();
            Image newImage = Image.FromFile((Environment.CurrentDirectory + "\\cards2.png"));
            Bitmap sourceBitmap = new Bitmap(newImage, 1027, 615); // make constants

            for (int i = 0; i < pictureArray.Length; i++)
            {

                pictureArray[i] = new PictureBox();
                Controls.Add(pictureArray[i]);

                // set position and size
                pictureArray[i].Location = new Point(100 * i + 100, 355);
                pictureArray[i].Size = new Size(80, 125);

                pictureArray[i].Click += new EventHandler(CardClickHandler);

                cord0 = Convert.ToInt32(aDeck.deck[theCards[i]].getValue) * 79;
                cord1 = Convert.ToInt32(aDeck.deck[theCards[i]].getSuit) * 123;

                // MessageBox.Show(aDeck.deck[theCards[i]].cardNumber.ToString());
                //  MessageBox.Show(aDeck.deck[theCards[i]].cardSuit.ToString());
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
                testnum++;
                CalculateMovement(help.Left, help.Top);
                timer1.Tag = help;
                timer1.Enabled = true;

            }
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
            leftMove = ((100 * testnum) - x) / 5;
            topMove = (CARD_END_POSITION_Y - y) / 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image newImage = Image.FromFile(@"C:\Users\My Files\Documents\Visual Studio 2010\guiproposal\guiproposal\Resources\cards2.png");
            Bitmap sourceBitmap = new Bitmap(newImage, 1027, 615); // make constants


            PictureBox newPictureBox = new PictureBox();
            Controls.Add(newPictureBox);

            // set position and size
            newPictureBox.Location = new Point(100 * currentCard + 100, 355);
            newPictureBox.Size = new Size(80, 125);

            newPictureBox.Click += new EventHandler(CardClickHandler);

            cord0 = Convert.ToInt32(aDeck.deck[theCards[currentCard]].getValue) * 79;
            cord1 = Convert.ToInt32(aDeck.deck[theCards[currentCard]].getSuit) * 123;


            //  MessageBox.Show(aDeck.deck[theCards[currentCard]].getValue.ToString());
            // MessageBox.Show(aDeck.deck[theCards[currentCard]].getSuit.ToString());

            rectCropArea = new Rectangle(cord0, cord1, cord2, cord3);
            newPictureBox.Image = (Image)sourceBitmap.Clone(rectCropArea, sourceBitmap.PixelFormat);

            currentCard++;
            sourceBitmap.Dispose();
        }

        public GameRoom()
        {
            InitializeComponent();
          
        }

        private void GameRoom_Load(object sender, EventArgs e)
        {
            //Draw all hands
        }


        //Needed events:


        //Card click

        


    }
}
