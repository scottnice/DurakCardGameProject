using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak
{
    public partial class CardGameMenu : Form
    {
        // game reference
        private DurakGame theGame;
        private OptionsMenu options = new OptionsMenu();

        public CardGameMenu()
        {
            InitializeComponent();
            theGame = new BasicRules();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GameRoom newGame = new GameRoom(options.getGame());
            newGame.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            options.ShowDialog();
        }
    }
}
