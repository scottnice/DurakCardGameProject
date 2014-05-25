namespace Durak
{
    partial class OptionsMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbxPlayers = new System.Windows.Forms.ComboBox();
            this.cbxRules = new System.Windows.Forms.ComboBox();
            this.cbxDeckSize = new System.Windows.Forms.ComboBox();
            this.cbxAIDifficulty = new System.Windows.Forms.ComboBox();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblRules = new System.Windows.Forms.Label();
            this.lblDeckSize = new System.Windows.Forms.Label();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.lblGameOptions = new System.Windows.Forms.Label();
            this.btnSaveAndQuit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboIsAiGame = new System.Windows.Forms.CheckBox();
            this.OptionsMenuTips = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cbxPlayers
            // 
            this.cbxPlayers.Enabled = false;
            this.cbxPlayers.FormattingEnabled = true;
            this.cbxPlayers.Items.AddRange(new object[] {
            2,
            3,
            4,
            5,
            6});
            this.cbxPlayers.Location = new System.Drawing.Point(78, 48);
            this.cbxPlayers.Name = "cbxPlayers";
            this.cbxPlayers.Size = new System.Drawing.Size(115, 21);
            this.cbxPlayers.TabIndex = 0;
            this.OptionsMenuTips.SetToolTip(this.cbxPlayers, "Buy full version to access feature.");
            // 
            // cbxRules
            // 
            this.cbxRules.Enabled = false;
            this.cbxRules.FormattingEnabled = true;
            this.cbxRules.Items.AddRange(new object[] {
            "Default",
            "Passing"});
            this.cbxRules.Location = new System.Drawing.Point(78, 84);
            this.cbxRules.Name = "cbxRules";
            this.cbxRules.Size = new System.Drawing.Size(115, 21);
            this.cbxRules.TabIndex = 1;
            this.OptionsMenuTips.SetToolTip(this.cbxRules, "Buy full version to access feature.");
            // 
            // cbxDeckSize
            // 
            this.cbxDeckSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeckSize.FormattingEnabled = true;
            this.cbxDeckSize.Items.AddRange(new object[] {
            36,
            52,
            20});
            this.cbxDeckSize.Location = new System.Drawing.Point(78, 120);
            this.cbxDeckSize.Name = "cbxDeckSize";
            this.cbxDeckSize.Size = new System.Drawing.Size(115, 21);
            this.cbxDeckSize.TabIndex = 2;
            this.OptionsMenuTips.SetToolTip(this.cbxDeckSize, "Select how many cards you wish to play with.");
            // 
            // cbxAIDifficulty
            // 
            this.cbxAIDifficulty.Enabled = false;
            this.cbxAIDifficulty.FormattingEnabled = true;
            this.cbxAIDifficulty.Items.AddRange(new object[] {
            Durak.ComputerPlayer.AIDifficulty.Basic,
            Durak.ComputerPlayer.AIDifficulty.Advanced,
            Durak.ComputerPlayer.AIDifficulty.Cheater});
            this.cbxAIDifficulty.Location = new System.Drawing.Point(78, 156);
            this.cbxAIDifficulty.Name = "cbxAIDifficulty";
            this.cbxAIDifficulty.Size = new System.Drawing.Size(115, 21);
            this.cbxAIDifficulty.TabIndex = 3;
            this.OptionsMenuTips.SetToolTip(this.cbxAIDifficulty, "Buy full version to access feature.");
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(12, 51);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(41, 13);
            this.lblPlayers.TabIndex = 4;
            this.lblPlayers.Text = "Players";
            // 
            // lblRules
            // 
            this.lblRules.AutoSize = true;
            this.lblRules.Location = new System.Drawing.Point(12, 87);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(34, 13);
            this.lblRules.TabIndex = 5;
            this.lblRules.Text = "Rules";
            // 
            // lblDeckSize
            // 
            this.lblDeckSize.AutoSize = true;
            this.lblDeckSize.Location = new System.Drawing.Point(12, 123);
            this.lblDeckSize.Name = "lblDeckSize";
            this.lblDeckSize.Size = new System.Drawing.Size(56, 13);
            this.lblDeckSize.TabIndex = 6;
            this.lblDeckSize.Text = "Deck Size";
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Location = new System.Drawing.Point(12, 159);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(60, 13);
            this.lblDifficulty.TabIndex = 7;
            this.lblDifficulty.Text = "AI Difficulty";
            // 
            // lblGameOptions
            // 
            this.lblGameOptions.AutoSize = true;
            this.lblGameOptions.Location = new System.Drawing.Point(65, 9);
            this.lblGameOptions.Name = "lblGameOptions";
            this.lblGameOptions.Size = new System.Drawing.Size(74, 13);
            this.lblGameOptions.TabIndex = 8;
            this.lblGameOptions.Text = "Game Options";
            // 
            // btnSaveAndQuit
            // 
            this.btnSaveAndQuit.Location = new System.Drawing.Point(12, 188);
            this.btnSaveAndQuit.Name = "btnSaveAndQuit";
            this.btnSaveAndQuit.Size = new System.Drawing.Size(84, 21);
            this.btnSaveAndQuit.TabIndex = 9;
            this.btnSaveAndQuit.Text = "Save and Quit";
            this.OptionsMenuTips.SetToolTip(this.btnSaveAndQuit, "Saves options and exits.");
            this.btnSaveAndQuit.UseVisualStyleBackColor = true;
            this.btnSaveAndQuit.Click += new System.EventHandler(this.btnSaveAndQuit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(109, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 21);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.OptionsMenuTips.SetToolTip(this.btnCancel, "Cancels current options and exits");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboIsAiGame
            // 
            this.cboIsAiGame.AutoSize = true;
            this.cboIsAiGame.Location = new System.Drawing.Point(125, 25);
            this.cboIsAiGame.Name = "cboIsAiGame";
            this.cboIsAiGame.Size = new System.Drawing.Size(67, 17);
            this.cboIsAiGame.TabIndex = 11;
            this.cboIsAiGame.Text = "AI Game";
            this.OptionsMenuTips.SetToolTip(this.cboIsAiGame, "Watch a game played by computer players.");
            this.cboIsAiGame.UseVisualStyleBackColor = true;
            // 
            // OptionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 221);
            this.Controls.Add(this.cboIsAiGame);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAndQuit);
            this.Controls.Add(this.lblGameOptions);
            this.Controls.Add(this.lblDifficulty);
            this.Controls.Add(this.lblDeckSize);
            this.Controls.Add(this.lblRules);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.cbxAIDifficulty);
            this.Controls.Add(this.cbxDeckSize);
            this.Controls.Add(this.cbxRules);
            this.Controls.Add(this.cbxPlayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OptionsMenu";
            this.Load += new System.EventHandler(this.OptionsMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxPlayers;
        private System.Windows.Forms.ComboBox cbxRules;
        private System.Windows.Forms.ComboBox cbxDeckSize;
        private System.Windows.Forms.ComboBox cbxAIDifficulty;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblRules;
        private System.Windows.Forms.Label lblDeckSize;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.Label lblGameOptions;
        private System.Windows.Forms.Button btnSaveAndQuit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cboIsAiGame;
        private System.Windows.Forms.ToolTip OptionsMenuTips;
    }
}