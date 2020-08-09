using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B20_Window
{
    public class PlayerLabel : Label
    {
        private readonly bool r_ScoreIrrelevant;
        private string m_LeftDisplay;
        private int m_Score;
        private string m_RightDisplay;

        public int Score { get => this.m_Score; set => this.m_Score = value; }
        public string RightDisplay { get => this.m_RightDisplay; set => this.m_RightDisplay = value; }

        public PlayerLabel(string i_LeftDisplay, Color i_PlayerColor, string i_RightDisplay = "Pairs", bool i_ScoreIrrelevant = false)
        {
            InitializeComponent();
            this.r_ScoreIrrelevant = i_ScoreIrrelevant;
            this.m_LeftDisplay = i_LeftDisplay;
            this.m_Score = (i_ScoreIrrelevant) ? -1 : 0;
            this.m_RightDisplay = i_RightDisplay;
            this.BackColor = i_PlayerColor;
            this.AutoSize = true;
            UpdateTextByFormat();
        }

        public void UpdateTextByFormat()
        {
            string scoreDisplay = (r_ScoreIrrelevant) ? "" : m_Score.ToString() + " "; // Ignore score value
            this.Text = string.Format("{0}: {1}{2}", m_LeftDisplay, scoreDisplay, m_RightDisplay);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PlayerLabel
            // 
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5f);
            this.ResumeLayout(false);
        }
    }
}
