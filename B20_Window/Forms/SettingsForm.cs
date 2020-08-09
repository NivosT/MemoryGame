using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using B20_GameLogic;

namespace B20_Window
{
    public partial class SettingsForm : Form
    {
        private string k_DefaultComputerName = "Computer";
        private string k_DefaultEmptyName = "Human";
        
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new GameController(new GameParams()
            {
                FirstPlayerName = (this.FirstPlayerTextBox.Text != string.Empty) ? this.FirstPlayerTextBox.Text : k_DefaultEmptyName,
                SecondPlayerName = (this.SecondPlayerTextBox.Text != "- computer -") ? getTextOrDefault(this.SecondPlayerTextBox.Text) : k_DefaultComputerName,
                SecondIsBot = (!SecondPlayerTextBox.Enabled), // Not enabled == against bot
                BoardSize = new KeyValuePair<int, int>((int)(BoardSizeButton.Text[0] - '0'), (int)(BoardSizeButton.Text[4] - '0'))
            });
            this.Close();
        }

        private void BoardSizeButton_Click(object sender, EventArgs e)
        {
            this.BoardSizeButton.Text = GameController.GetNextBoardSize(BoardSizeButton.Text);
        }

        private void SecondPlayerButton_Click(object sender, EventArgs e)
        {
            // Currently against computer -> change to against friend
            if (SecondPlayerButton.Text == "Against Computer")
            {
                this.SecondPlayerButton.Text = "Against A Friend";
                this.SecondPlayerTextBox.Enabled = false;
                this.SecondPlayerTextBox.Text = "- computer -";
                this.SecondPlayerTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
                this.SecondPlayerTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            }
            // -> change to Against Computer
            else
            {
                this.SecondPlayerButton.Text = "Against Computer";
                this.SecondPlayerTextBox.Enabled = true;
                this.SecondPlayerTextBox.Text = "";
                this.SecondPlayerTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                this.SecondPlayerTextBox.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        private string getTextOrDefault(string i_Text)
        {
            return (i_Text != string.Empty) ? i_Text : k_DefaultEmptyName;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
        }
    }
}
