using System;
using System.Drawing;
using System.Windows.Forms;

namespace B20_Window
{
    partial class SettingsForm
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
            this.StartButton = new System.Windows.Forms.Button();
            this.FirstPlayerTextBox = new System.Windows.Forms.TextBox();
            this.SecondPlayerTextBox = new System.Windows.Forms.TextBox();
            this.FirstPlayerLabel = new System.Windows.Forms.Label();
            this.SecondPlayerLabel = new System.Windows.Forms.Label();
            this.BoardSizeLabel = new System.Windows.Forms.Label();
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.SecondPlayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.PaleGreen;
            this.StartButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.StartButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.StartButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.StartButton.Location = new System.Drawing.Point(959, 341);
            this.StartButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(200, 55);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // FirstPlayerTextBox
            // 
            this.FirstPlayerTextBox.Location = new System.Drawing.Point(344, 29);
            this.FirstPlayerTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.FirstPlayerTextBox.Name = "FirstPlayerTextBox";
            this.FirstPlayerTextBox.Size = new System.Drawing.Size(260, 38);
            this.FirstPlayerTextBox.TabIndex = 1;
            // 
            // SecondPlayerTextBox
            // 
            this.SecondPlayerTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SecondPlayerTextBox.Enabled = false;
            this.SecondPlayerTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.SecondPlayerTextBox.Location = new System.Drawing.Point(344, 91);
            this.SecondPlayerTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.SecondPlayerTextBox.Name = "SecondPlayerTextBox";
            this.SecondPlayerTextBox.Size = new System.Drawing.Size(260, 38);
            this.SecondPlayerTextBox.TabIndex = 2;
            this.SecondPlayerTextBox.Text = "- computer -";
            // 
            // FirstPlayerLabel
            // 
            this.FirstPlayerLabel.AutoSize = true;
            this.FirstPlayerLabel.Location = new System.Drawing.Point(35, 31);
            this.FirstPlayerLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.FirstPlayerLabel.Name = "FirstPlayerLabel";
            this.FirstPlayerLabel.Size = new System.Drawing.Size(248, 32);
            this.FirstPlayerLabel.TabIndex = 3;
            this.FirstPlayerLabel.Text = "First Player Name:";
            // 
            // SecondPlayerLabel
            // 
            this.SecondPlayerLabel.AutoSize = true;
            this.SecondPlayerLabel.Location = new System.Drawing.Point(35, 98);
            this.SecondPlayerLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.SecondPlayerLabel.Name = "SecondPlayerLabel";
            this.SecondPlayerLabel.Size = new System.Drawing.Size(290, 32);
            this.SecondPlayerLabel.TabIndex = 3;
            this.SecondPlayerLabel.Text = "Second Player Name:";
            // 
            // BoardSizeLabel
            // 
            this.BoardSizeLabel.AutoSize = true;
            this.BoardSizeLabel.Location = new System.Drawing.Point(35, 184);
            this.BoardSizeLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.BoardSizeLabel.Name = "BoardSizeLabel";
            this.BoardSizeLabel.Size = new System.Drawing.Size(162, 32);
            this.BoardSizeLabel.TabIndex = 3;
            this.BoardSizeLabel.Text = "Board Size:";
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.BackColor = System.Drawing.Color.MediumPurple;
            this.BoardSizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoardSizeButton.Location = new System.Drawing.Point(41, 231);
            this.BoardSizeButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(285, 165);
            this.BoardSizeButton.TabIndex = 0;
            this.BoardSizeButton.Text = "4 x 4";
            this.BoardSizeButton.UseVisualStyleBackColor = false;
            this.BoardSizeButton.Click += new System.EventHandler(this.BoardSizeButton_Click);
            // 
            // SecondPlayerButton
            // 
            this.SecondPlayerButton.Font = new System.Drawing.Font("Miriam", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecondPlayerButton.Location = new System.Drawing.Point(627, 91);
            this.SecondPlayerButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.SecondPlayerButton.Name = "SecondPlayerButton";
            this.SecondPlayerButton.Size = new System.Drawing.Size(267, 48);
            this.SecondPlayerButton.TabIndex = 0;
            this.SecondPlayerButton.Text = "Against A Friend";
            this.SecondPlayerButton.UseVisualStyleBackColor = true;
            this.SecondPlayerButton.Click += new System.EventHandler(this.SecondPlayerButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 412);
            this.Controls.Add(this.BoardSizeLabel);
            this.Controls.Add(this.SecondPlayerLabel);
            this.Controls.Add(this.FirstPlayerLabel);
            this.Controls.Add(this.SecondPlayerTextBox);
            this.Controls.Add(this.FirstPlayerTextBox);
            this.Controls.Add(this.BoardSizeButton);
            this.Controls.Add(this.SecondPlayerButton);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox FirstPlayerTextBox;
        private System.Windows.Forms.TextBox SecondPlayerTextBox;
        private System.Windows.Forms.Label FirstPlayerLabel;
        private System.Windows.Forms.Label SecondPlayerLabel;
        private System.Windows.Forms.Label BoardSizeLabel;
        private System.Windows.Forms.Button BoardSizeButton;
        private System.Windows.Forms.Button SecondPlayerButton;
    }
}