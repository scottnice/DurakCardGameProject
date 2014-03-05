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

        Deck myDeck;
        Hand myHand = new Hand();
        List<GenericPlayer> myPlayers = new List<GenericPlayer>();

        private void dealCards(int numCards = 1)
        {
            if (numCards < 1)
                throw new ArgumentException("Cannot deal less than 1 card to each hand.");

            for (int j = numCards; j > 0; --j)
            {
                for (int i = 0; i < myPlayers.Count; ++i)
                    myDeck.deal(myPlayers[i].myHand);
            }
        }

        public GameRoom()
        {
            InitializeComponent();
            myPlayers.Add(new HumanPlayer());
            myPlayers.Add(new ComputerPlayer());
            myDeck = new Deck(Deck.DeckSize.FIFTY_TWO);
            myDeck.shuffle();

            myDeck.deal(myHand);
            dealCards(5);
        }


    }
}
