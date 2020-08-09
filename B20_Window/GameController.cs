using B20_GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace B20_Window
{
    public class GameController
    {
        public static readonly string sr_UrlSource = "https://picsum.photos/100";

        private readonly GameParams r_GameParams; // Once assigned, every new game will have these params
        private Game m_CurrGame; // Current running game
        private GameForm m_CurrGameForm;
        private readonly Dictionary<Char, string> r_ValToImgUrl; // Cache of display for card values

        public int CurrPlayerIdx => this.m_CurrGame.TurnIdx;

        public GameController(GameParams i_GameParams)
        {
            this.r_ValToImgUrl = new Dictionary<char, string>();
            this.r_GameParams = i_GameParams;
            // Create logic game and UI with given params
            initGame(i_GameParams);

            // Register for events
            m_CurrGameForm.CardClicked += currGameForm_CardClicked;
            m_CurrGame.PlayerOne.ScoreUpdated += currGame_ScoreUpdated;
            m_CurrGame.PlayerTwo.ScoreUpdated += currGame_ScoreUpdated;
            m_CurrGame.TurnChanged += currGame_TurnChanged;
            m_CurrGame.GameFinished += currGame_GameFinished;
            
            m_CurrGameForm.ShowDialog();
        }

        private void currGame_GameFinished(object sender, GameFinishedEventArgs e)
        {
            string winnerText = (e.WinnerName != null) ? "The winner is: " + e.WinnerName + "!" : "It's a tie!";

            string message = string.Format(" {0}{1} Should we play again?", winnerText, Environment.NewLine);
            string caption = "Finished Game";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);

            // No button -> shut down
            if (result == DialogResult.No)
            {
                m_CurrGameForm.Close();
            }
            // Yes button -> New game with same params
            else if (result == DialogResult.Yes)
            {
                m_CurrGameForm.Hide();
                new GameController(this.r_GameParams);
                m_CurrGameForm.Close();
            }
        }

        private void currGame_ScoreUpdated(object sender, ScoreUpdatedEventArgs e)
        {
            m_CurrGameForm.ScoreUpdated(e.PlayerIdx, e.NewScore);
        }

        private void currGame_TurnChanged(object sender, TurnChangedEventArgs e)
        {
            Color currPlayerColor = GetPlayerColorByIdx((sender as Game).TurnIdx);
            m_CurrGameForm.UpdateCurrPlayer(e.CurrPlayerName, currPlayerColor);
        }

        private void currGameForm_CardClicked(object sender, CardClickedEventArgs e)
        {
            // Sends the clicked card's indexes to be handled by logic
            m_CurrGame.UserChoice(new KeyValuePair<int, int>(e.Row, e.Col));
        }

        private void initGame(GameParams i_NewGameParams)
        {
            // Create logic game
            this.m_CurrGame = new Game(i_NewGameParams);
            registerCards();

            // Create game form
            this.m_CurrGameForm = new GameForm(new DisplayParams()
            {
                Rows = m_CurrGame.GameBoard.Rows,
                Cols = m_CurrGame.GameBoard.Cols,
                FirstPlayerName = m_CurrGame.PlayerOne.Name,
                FirstPlayerColor = GetPlayerColorByIdx(1),
                SecondPlayerName = m_CurrGame.PlayerTwo.Name,
                SecondPlayerColor = GetPlayerColorByIdx(2)
            });
        }

        private void registerCards(bool i_ToReg = true)
        {
            int currMaxRows = m_CurrGame.GameBoard.Rows;
            int currMaxCols = m_CurrGame.GameBoard.Cols;

            for (int i = 0; i < currMaxRows; i++)
            {
                for (int j = 0; j < currMaxCols; j++)
                {
                    if (i_ToReg) // Register cards
                    {
                        m_CurrGame.GameBoard.GetCardByIdx(i, j).CardFlipped += GameCard_Flipped;
                    }
                    else // Unregister
                    {
                        m_CurrGame.GameBoard.GetCardByIdx(i, j).CardFlipped -= GameCard_Flipped;
                    }
                }
            }
        }

        internal Color GetPlayerColorByIdx(int i_PlayerIdx)
        {
            return (i_PlayerIdx == 1) ? Color.PaleGreen : Color.MediumPurple;
        }

        public static string GetNextBoardSize(string i_BoardSize)
        {
            int rows = (int)(i_BoardSize[0] - '0');
            int cols = (int)(i_BoardSize[4] - '0');
            // Send current board size and receive the next
            KeyValuePair<int,int> newBoardVals = Game.GetNextBoardSize(new KeyValuePair<int, int>(rows, cols));

            return string.Format("{0} x {1}", newBoardVals.Key, newBoardVals.Value);
        }

        private string getRandomImg()
        {
            string randomImg;

            try
            {
                do // Request for image from pre-defined pictures engine
                {
                    WebRequest request = HttpWebRequest.Create(sr_UrlSource);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    // Grab redirected URI -> Random image
                    randomImg = (response.StatusCode == HttpStatusCode.OK) ? response.ResponseUri.ToString() : null; // Status code not OK -> failed to grab image
                    request.Abort();
                } while (randomImg != null && imgAlreadyAssigned(randomImg)); // Generate another if the image already exists (rare)   
            }
            catch (WebException) // Failed to get image from web
            {
                randomImg = null;
            }

            return randomImg;
        }
        
        private string getCardDisplay(char i_CardValue)
        {
            string cardDisplayStr = string.Format("text|{0}", i_CardValue.ToString());
            
            // Check if the display for the given value is already cached
            if (r_ValToImgUrl.TryGetValue(i_CardValue, out string o_DisplayUrl))
            {
                cardDisplayStr = o_DisplayUrl;
            }
            // Not cached -> Get new display for this value
            else
            {
                string imgUrl = getRandomImg();
                if (imgUrl != null)
                {
                    cardDisplayStr = string.Format("image|{0}", imgUrl);
                }
                
                r_ValToImgUrl.Add(i_CardValue, cardDisplayStr); // Cache the display for this value
            }

            return cardDisplayStr;
        }

        private bool imgAlreadyAssigned(string i_UrlToCheck)
        {
            bool alreadyAssigned = false;
            string formattedUrlToCheck = string.Format("image|{0}", i_UrlToCheck);

            // Search for the given URL in the cache
            foreach (KeyValuePair<char, string> pair in r_ValToImgUrl)
            {
                if (pair.Value == formattedUrlToCheck)
                {
                    alreadyAssigned = true;
                    break;
                }
            }

            return alreadyAssigned;
        }

        public void GameCard_Flipped(object sender, CardFlippedEventArgs e)
        {
            KeyValuePair<int, int> cardLoc = new KeyValuePair<int, int>(e.Row, e.Col);

            if (e.IsRevealed)
            {
                string cardValue = getCardDisplay(e.Value);
                Color currPlayerColor = GetPlayerColorByIdx(e.FlippedByIdx);
                m_CurrGameForm.RevealCard(cardLoc, cardValue, currPlayerColor);
            }
            // Hide value
            else
            {
                m_CurrGameForm.HideCard(cardLoc);
            }
        }
    }
}