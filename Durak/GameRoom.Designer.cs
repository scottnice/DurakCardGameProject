
﻿namespace Durak
{
    partial class GameRoom
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
            this.tmrFrameUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnPass = new System.Windows.Forms.Button();
            this.tmrGamePlay = new System.Windows.Forms.Timer(this.components);
            this.GameRoomFormTips = new System.Windows.Forms.ToolTip(this.components);
            this.lblGameState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrFrameUpdate
            // 
            this.tmrFrameUpdate.Interval = 17;
            this.tmrFrameUpdate.Tick += new System.EventHandler(this.tmrFrameUpdate_Tick);
            // 
            // btnPass
            // 
            this.btnPass.Location = new System.Drawing.Point(12, 517);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(75, 23);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "&Pass";
            this.GameRoomFormTips.SetToolTip(this.btnPass, "Can also use the enter button to click activate this button.");
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // tmrGamePlay
            // 
            this.tmrGamePlay.Interval = 750;
            this.tmrGamePlay.Tick += new System.EventHandler(this.tmrGamePlay_Tick);
            // 
            // lblGameState
            // 
            this.lblGameState.AutoSize = true;
            this.lblGameState.Location = new System.Drawing.Point(356, 13);
            this.lblGameState.Name = "lblGameState";
            this.lblGameState.Size = new System.Drawing.Size(0, 13);
            this.lblGameState.TabIndex = 1;
            // 
            // GameRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 617);
            this.Controls.Add(this.lblGameState);
            this.Controls.Add(this.btnPass);
            this.Name = "GameRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak Game";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameRoom_FormClosing);
            this.Load += new System.EventHandler(this.GameRoom_Load);
            this.SizeChanged += new System.EventHandler(this.GameRoom_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrFrameUpdate;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Timer tmrGamePlay;
        private System.Windows.Forms.ToolTip GameRoomFormTips;
        private System.Windows.Forms.Label lblGameState;

    }
}

