using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CardLibrary;

namespace Durak
{
    /// <summary>
    /// Options menu form that contains all of the options to play a game of durak.
    /// </summary>
    public partial class OptionsMenu: Form
    {
        static private int playersIndex = 0;
        static private int rulesIndex = 0;
        static private int deckIndex = 0;
        static private int difficultyIndex = 0;
        static private int player1DifficultyIndex = 0;
        static private int player2DifficultyIndex = 0;

        /// <summary>
        /// Default constructor for the options menu, sets the selected indexes of the combo boxes
        /// </summary>
        internal OptionsMenu()
        {
            InitializeComponent();

            // start all indexes at 0
            cbxPlayers.SelectedIndex = playersIndex;
            cbxRules.SelectedIndex = rulesIndex;
            cbxDeckSize.SelectedIndex = deckIndex;
            cbxAIDifficulty.SelectedIndex = difficultyIndex;
            cbxPlayer1Difficulty.SelectedIndex = player1DifficultyIndex;
            cbxPlayer2Difficulty.SelectedIndex = player2DifficultyIndex;
        }

        /// <summary>
        /// Creates an instance of a durak card game and returns the game for use in the gameroom form
        /// </summary>
        /// <returns></returns>
        internal DurakGame getGame()
        {
            // If AI game mode, pass both player difficulties
            if (cboIsAiGame.Checked)
            {
                ComputerPlayer.AIDifficulty[] difficulties = new ComputerPlayer.AIDifficulty[]
                {
                    (ComputerPlayer.AIDifficulty)cbxPlayer1Difficulty.SelectedIndex,
                    (ComputerPlayer.AIDifficulty)cbxPlayer2Difficulty.SelectedIndex
                };

                if (RulesProperty == 0)
                    return new BasicRules((int)cbxPlayers.SelectedItem, (Deck.DeckSize)cbxDeckSize.SelectedItem, difficulties, cboIsAiGame.Checked);
                else
                    return new PassingRules((int)cbxPlayers.SelectedItem, (Deck.DeckSize)cbxDeckSize.SelectedItem, difficulties, cboIsAiGame.Checked);
            }
            else
            {
                // For human vs AI, use the single difficulty setting
                if (RulesProperty == 0)
                    return new BasicRules((int)cbxPlayers.SelectedItem, (Deck.DeckSize)cbxDeckSize.SelectedItem, (ComputerPlayer.AIDifficulty)cbxAIDifficulty.SelectedIndex, cboIsAiGame.Checked);
                else
                    return new PassingRules((int)cbxPlayers.SelectedItem, (Deck.DeckSize)cbxDeckSize.SelectedItem, (ComputerPlayer.AIDifficulty)cbxAIDifficulty.SelectedIndex, cboIsAiGame.Checked);
            }
        }

        /// <summary>
        /// Options menu on load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsMenu_Load(object sender, EventArgs e)
        {
            Utilities.UpdateLog("Options Dialog Opened"); 
            cbxPlayers.SelectedIndex = playersIndex;
            cbxRules.SelectedIndex = rulesIndex;
            cbxDeckSize.SelectedIndex = deckIndex;
            cbxAIDifficulty.SelectedIndex = difficultyIndex;
        }

        /// <summary>
        /// Saves the settings to the forms static variables and writes to the log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAndQuit_Click(object sender, EventArgs e)
        {
            rulesIndex = cbxRules.SelectedIndex;
            difficultyIndex = cbxAIDifficulty.SelectedIndex;
            player1DifficultyIndex = cbxPlayer1Difficulty.SelectedIndex;
            player2DifficultyIndex = cbxPlayer2Difficulty.SelectedIndex;

            deckIndex = cbxDeckSize.SelectedIndex;
            playersIndex = cbxPlayers.SelectedIndex;

            if (cboIsAiGame.Checked)
            {
                Utilities.UpdateLog("Options set to " + cbxRules.SelectedItem.ToString() + " rules, " + PlayersProperty + " Players, a " + DeckSizeProperty + " card deck, Player 1: " + Player1DifficultyProperty + ", Player 2: " + Player2DifficultyProperty);
            }
            else
            {
                Utilities.UpdateLog("Options set to " + cbxRules.SelectedItem.ToString() + " rules, " + PlayersProperty + " Players, a " + DeckSizeProperty + " card deck, and AI Difficulty of " + DifficultyProperty);
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event handler for AI Game checkbox - toggles visibility of difficulty controls
        /// </summary>
        private void cboIsAiGame_CheckedChanged(object sender, EventArgs e)
        {
            if (cboIsAiGame.Checked)
            {
                // Hide single AI difficulty, show per-player difficulties
                cbxAIDifficulty.Visible = false;
                lblDifficulty.Visible = false;

                cbxPlayer1Difficulty.Visible = true;
                lblPlayer1Difficulty.Visible = true;
                cbxPlayer2Difficulty.Visible = true;
                lblPlayer2Difficulty.Visible = true;
            }
            else
            {
                // Show single AI difficulty, hide per-player difficulties
                cbxAIDifficulty.Visible = true;
                lblDifficulty.Visible = true;

                cbxPlayer1Difficulty.Visible = false;
                lblPlayer1Difficulty.Visible = false;
                cbxPlayer2Difficulty.Visible = false;
                lblPlayer2Difficulty.Visible = false;
            }
        }

        #region Accessor Methods 

        public int PlayersProperty
        {
            get
            {
                return (int)cbxPlayers.Items[playersIndex];
            }
        }

        /// <summary>
        /// The selected index of the rules property 0 = basic 1 = passing
        /// </summary>
        public int RulesProperty
        {
            get
            {
                return rulesIndex;
            }
        }

        /// <summary>
        /// Returns the deck size enum associated with the selected property
        /// </summary>
        public Deck.DeckSize DeckSizeProperty
        {
            get
            {
                return (Deck.DeckSize)cbxDeckSize.Items[deckIndex];
            }
        }

        /// <summary>
        /// Returns the difficulty enum associated with the selected property
        /// </summary>
        internal ComputerPlayer.AIDifficulty DifficultyProperty
        {
            get
            {
                return (ComputerPlayer.AIDifficulty)cbxAIDifficulty.Items[difficultyIndex];
            }
        }

        /// <summary>
        /// Returns the difficulty enum for Player 1 in AI mode
        /// </summary>
        internal ComputerPlayer.AIDifficulty Player1DifficultyProperty
        {
            get
            {
                return (ComputerPlayer.AIDifficulty)cbxPlayer1Difficulty.Items[player1DifficultyIndex];
            }
        }

        /// <summary>
        /// Returns the difficulty enum for Player 2 in AI mode
        /// </summary>
        internal ComputerPlayer.AIDifficulty Player2DifficultyProperty
        {
            get
            {
                return (ComputerPlayer.AIDifficulty)cbxPlayer2Difficulty.Items[player2DifficultyIndex];
            }
        }

        #endregion
    }
}
