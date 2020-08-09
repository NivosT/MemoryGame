using B20_GameLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B20_Window
{
    public partial class GameForm : Form
    {
        private readonly int r_Gap;
        private readonly CardButton[,] r_CardButtons;
        private PlayerLabel m_CurrPlayerLabel;
        private PlayerLabel m_PlayerOneLabel;
        private PlayerLabel m_PlayerTwoLabel;

        public event EventHandler<CardClickedEventArgs> CardClicked;

        public GameForm(DisplayParams i_DisplayParams)
        {
            InitializeComponent();
            this.r_Gap = 10;
            int rows = i_DisplayParams.Rows;
            int cols = i_DisplayParams.Cols; 
            
            // Create buttons
            this.r_CardButtons = new CardButton[rows, cols];
            createCardButtons(rows, cols);
            // Players labels
            int playerLabelsTopLocation = CardButton.CardButtonHeight * (cols + 1) - 3 * r_Gap;
            addPlayersDisplay(i_DisplayParams, playerLabelsTopLocation);

            this.Size = new Size(CardButton.CardButtonWidth * (rows + 1) - getWidthGap(rows), m_PlayerTwoLabel.Bottom + 6 * r_Gap);
        }

        private int getWidthGap(int i_RowsNum)
        {
            // Determine gap by given number of rows
            int widthGap = 3*r_Gap;
            if (i_RowsNum == 5)
            {
                widthGap -= r_Gap;
            }
            else if (i_RowsNum == 6)
            {
                widthGap -= 2*r_Gap;
            }

            return widthGap;
        }

        private void addPlayersDisplay(DisplayParams i_DisplayParams, int i_TopStartPoint)
        {
            // Current Player Label
            m_CurrPlayerLabel = new PlayerLabel("Current Player", i_DisplayParams.FirstPlayerColor, i_DisplayParams.FirstPlayerName, true)
            {
                Top = i_TopStartPoint,
                Left = this.Left + r_Gap
            };
            this.Controls.Add(m_CurrPlayerLabel);
            // Player One Label
            m_PlayerOneLabel = new PlayerLabel(i_DisplayParams.FirstPlayerName, i_DisplayParams.FirstPlayerColor)
            {
                Top = m_CurrPlayerLabel.Bottom + r_Gap,
                Left = this.Left + r_Gap
            };
            this.Controls.Add(m_PlayerOneLabel);
            // Player Two Label
            m_PlayerTwoLabel = new PlayerLabel(i_DisplayParams.SecondPlayerName, i_DisplayParams.SecondPlayerColor)
            {
                Top = m_PlayerOneLabel.Bottom + r_Gap,
                Left = this.Left + r_Gap
            };
            this.Controls.Add(m_PlayerTwoLabel);
        }

        private void createCardButtons(int i_Rows, int i_Cols)
        {
            int iPos;
            int jPos;
            
            for (int i = 0; i < i_Rows; i++)
            {
                for (int j = 0; j < i_Cols; j++)
                {
                    // Create a new button foreach combination of i,j
                    r_CardButtons[i, j] = new CardButton(i, j);
                    iPos = this.Left + r_Gap +((r_Gap + r_CardButtons[i, j].Width) * i);
                    jPos = this.Top + r_Gap + ((r_Gap + r_CardButtons[i, j].Height) * j);
                    // Register created button to click
                    r_CardButtons[i, j].Location = new Point(iPos, jPos);
                    r_CardButtons[i, j].Click += CardButton_Click;
                    Controls.Add(r_CardButtons[i, j]);
                }
            }
        }

        internal void RevealCard(KeyValuePair<int,int> i_CardLoc, string i_CardVal, Color i_BackColor)
        {
            this.Refresh();

            CardButton btnToReveal = r_CardButtons[i_CardLoc.Key, i_CardLoc.Value];
            btnToReveal.Enabled = false;
            // Change display of card by the given input
            string[] cardValueParts = i_CardVal.Split('|');
            if (cardValueParts.Length == 2)
            {
                if (cardValueParts[0] == "image") // Display is a URL for an image
                {
                    btnToReveal.BackgroundImage = getImgByUrl(cardValueParts[1]);
                    btnToReveal.FlatStyle = FlatStyle.Flat;
                    btnToReveal.FlatAppearance.BorderColor = i_BackColor;
                }
                else if (cardValueParts[0] == "text") // Display is plain text
                {
                    btnToReveal.Text = cardValueParts[1];
                    btnToReveal.BackColor = i_BackColor;
                    btnToReveal.FlatStyle = default;
                }
            }
            
            this.Refresh();
        }

        private Image getImgByUrl(string i_Url)
        {
            Image imgByUrl = null;

            try
            {
                WebRequest request = WebRequest.Create(i_Url);
                using (var response = request.GetResponse())
                {
                    using (var str = response.GetResponseStream())
                    {
                        imgByUrl = Bitmap.FromStream(str);
                    }
                }
            }
            // Internet Failiure
            catch (WebException)
            {
                MessageBox.Show("Internet Failure. Exiting...", "Failure", MessageBoxButtons.OK ,MessageBoxIcon.Error);
                this.Hide();
                this.Close();
            }

            return imgByUrl;
        }

        internal void HideCard(KeyValuePair<int, int> i_CardLoc)
        {
            this.Refresh();
            // Enable button and restore all params to default
            CardButton btnToHide = r_CardButtons[i_CardLoc.Key, i_CardLoc.Value];
            btnToHide.Enabled = true;
            btnToHide.BackgroundImage = default;
            btnToHide.FlatStyle = FlatStyle.System;
            btnToHide.FlatAppearance.BorderColor = default;
            btnToHide.Text = string.Empty;
            btnToHide.BackColor = default;
            this.Refresh();
        }
        
        internal void UpdateCurrPlayer(string i_NextName, Color i_NextPlayerColor)
        {
            m_CurrPlayerLabel.BackColor = i_NextPlayerColor;
            m_CurrPlayerLabel.RightDisplay = i_NextName;
            m_CurrPlayerLabel.UpdateTextByFormat();
            this.Refresh();
        }

        internal void ScoreUpdated(int i_PlayerIdx, int i_NewScore)
        {
            // Get player label by index
            PlayerLabel pl = (i_PlayerIdx == 1) ? m_PlayerOneLabel : m_PlayerTwoLabel;
            // Set score and update it
            pl.RightDisplay = (i_NewScore == 1) ? "Pair(s)" : "Pairs";
            pl.Score = i_NewScore;
            pl.UpdateTextByFormat();
            this.Refresh();
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            if (sender is CardButton cBtn)
            {
                // Notify listeners a button was clicked with it's location
                OnCardClicked(new CardClickedEventArgs(cBtn.Row, cBtn.Col));
            }
        }

        private void OnCardClicked(CardClickedEventArgs e)
        {
            CardClicked?.Invoke(this, e);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
