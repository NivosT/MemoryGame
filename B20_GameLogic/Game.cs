using System;
using System.Collections.Generic;
using System.Linq;
using B20_GameLogic.Enums;

namespace B20_GameLogic
{
    public class Game
    {
        // Board size values
        const int k_MaxRowCol = 6;
        const int k_MinRowCol = 4;

        private Board m_GameBoard;
        private readonly Player r_PlayerOne;
        private readonly Player r_PlayerTwo;
        private readonly Card[] r_CurrCardChoices;
        private int m_TurnIdx; // Current player
        private eGameState m_GameState;

        public Player PlayerOne => this.r_PlayerOne;
        public Player PlayerTwo => this.r_PlayerTwo;
        public Board GameBoard => this.m_GameBoard;
        public eGameState GameState => this.m_GameState;
        public int TurnIdx => this.m_TurnIdx;

        // Handlers and events
        public delegate void CardFlippedEventHandler(object sender, CardFlippedEventArgs e);
        public delegate void ScoreUpdatedEventHandler(object sender, ScoreUpdatedEventArgs e);
        public delegate void GameFinishedEventHandler(object sender, GameFinishedEventArgs e);
        public delegate void TurnChangedEventHandler(object sender, TurnChangedEventArgs e);
        public event GameFinishedEventHandler GameFinished;
        public event TurnChangedEventHandler TurnChanged;

        public Game(GameParams i_GameParams)
        {
            this.m_GameBoard = new Board(i_GameParams.BoardSize.Key, i_GameParams.BoardSize.Value);
            this.r_PlayerOne = new Player(i_GameParams.FirstPlayerName, 1);
            this.r_PlayerTwo = new Player(i_GameParams.SecondPlayerName, 2, i_GameParams.SecondIsBot);
            this.r_CurrCardChoices = new Card[2];
            this.m_TurnIdx = 1;
            this.m_GameState = eGameState.HumanFirstCard; // Game starts with first player -> always human
        }

        public void UserChoice(KeyValuePair<int, int> i_CurrTurn)
        {
            switch (this.m_GameState)
            {
                case eGameState.HumanFirstCard:
                    humanPlayerTurn(i_CurrTurn);
                    nextState();
                    break;
                case eGameState.HumanSecondCard:
                    humanPlayerTurn(i_CurrTurn);
                    winOrNot(r_CurrCardChoices); // Checks and performs relevant routine
                    break;
                case eGameState.BotPlays:
                case eGameState.Finished:
                    // Do nothing
                    break;
            }
        }

        private void humanPlayerTurn(KeyValuePair<int, int> i_CurrTurn)
        {
            Player currPlayer = (m_TurnIdx == 1) ? PlayerOne : PlayerTwo;
            int currTurnRow = i_CurrTurn.Key;
            int currTurnCol = i_CurrTurn.Value;

            // Flip chosen card
            m_GameBoard.Cards[currTurnRow, currTurnCol].Flip(m_TurnIdx);

            // Save chosen card
            if (this.m_GameState == eGameState.HumanFirstCard)
            {
                r_CurrCardChoices[0] = m_GameBoard.Cards[currTurnRow, currTurnCol];
            }
            else
            {
                r_CurrCardChoices[1] = m_GameBoard.Cards[currTurnRow, currTurnCol];
            }
        }

        private void loseRoutine()
        {
            m_GameBoard.FlipDown(r_CurrCardChoices, m_TurnIdx); // Flip back the cards when the round has lost
        }

        private void winRoutine()
        {
            Player currPlayer = (m_TurnIdx == 1) ? PlayerOne : PlayerTwo;

            currPlayer.Score++;
            currPlayer.OnScoreUpdate();
        }

        private bool winOrNot(Card[] i_CheckPair)
        {
            // Check if values of the given cards are equal
            System.Threading.Thread.Sleep(1000);
            bool wonRound = (i_CheckPair[0].Value == i_CheckPair[1].Value);
            if (wonRound)
            {
                winRoutine();
            }
            else
            {
                //System.Threading.Thread.Sleep(1000);
                loseRoutine();
            }

            nextState(wonRound); // Update game state and turn if needed

            return wonRound;
        }

        private void nextState(bool i_WonRound = false)
        {
            if (m_GameBoard.IsDone()) // Game Over
            {
                this.m_GameState = eGameState.Finished;
                OnFinished();
            }
            else
            {
                switch (m_GameState) // Move to next state according to current state & round result
                {
                    case eGameState.BotPlays:
                        if (i_WonRound)
                        {
                            BotPlayerTurn(); // Another bot turn
                        }
                        else
                        {
                            nextTurn();
                            this.m_GameState = eGameState.HumanFirstCard; // After bot, always comes a human player
                        }

                        break;
                    case eGameState.HumanFirstCard:
                        this.m_GameState = eGameState.HumanSecondCard; // Move to second card choice
                        break;
                    case eGameState.HumanSecondCard:
                        if (i_WonRound)
                        {
                            this.m_GameState = eGameState.HumanFirstCard; // Extra round for this human player
                        }
                        else
                        {
                            bool nextIsBot = nextTurn();
                            if (nextIsBot)
                            {
                                this.m_GameState = eGameState.BotPlays; // Init bot turn
                                BotPlayerTurn();
                            }
                            else
                            {
                                this.m_GameState = eGameState.HumanFirstCard; // Next is human
                            }
                        }

                        break;
                }
            }
        }

        private void OnFinished()
        {
            Player winner = getWinner();
            string winnerName = (winner != null) ? winner.Name : null;
            GameFinished?.Invoke(this, new GameFinishedEventArgs(winnerName));
        }

        private void OnTurnChanged()
        {
            TurnChanged?.Invoke(this, new TurnChangedEventArgs(GetPlayerNameByIdx(m_TurnIdx)));
        }

        private bool BotPlayerTurn()
        {
            int choiceNum = 0;
            System.Threading.Thread.Sleep(1000);

            // Run 2 card selections
            while (choiceNum < 2)
            {
                // Select a bot choice according to configuration
                r_CurrCardChoices[choiceNum] = naiveBotChoice();
                parseChoice(r_CurrCardChoices[choiceNum], out int o_RowInput, out int o_ColInput);
                m_GameBoard.Cards[o_RowInput, o_ColInput].Flip(m_TurnIdx); // reveal card in game board

                System.Threading.Thread.Sleep(1000);
                choiceNum++;
            }

            return winOrNot(r_CurrCardChoices); // Return if this player won this round
        }

        private void parseChoice(Card i_ValidChoice, out int o_RowInput, out int o_ColInput)
        {
            // Get indeces from given card
            o_RowInput = i_ValidChoice.RowIdx;
            o_ColInput = i_ValidChoice.ColIdx;
        }

        private Card naiveBotChoice()
        {
            List<Card> nonRevealedList = m_GameBoard.GetRevealedCards(false); // List of non-revealed cards in the board
            // Randomize order of cards left
            var rnd = new Random();
            var rndOrder = nonRevealedList.OrderBy(item => rnd.Next());

            return rndOrder.ElementAt(0);
        }

        private bool nextTurn()
        {
            bool nextIsBot = false;
            this.m_TurnIdx = (m_TurnIdx == 1) ? 2 : 1; // Update the turn index to other player
            OnTurnChanged();
            if (m_TurnIdx == 2)
            {
                nextIsBot = (PlayerTwo.IsBot);
            }

            return nextIsBot;
        }

        private Player getWinner()
        {
            Player winner;
            if (PlayerOne.Score == PlayerTwo.Score)
            {
                winner = null;
            }
            else
            {
                winner = (PlayerOne.Score > PlayerTwo.Score) ? PlayerOne : PlayerTwo;
            }

            return winner;
        }

        public string GetPlayerNameByIdx(int i_PlayerIdx)
        {
            return (i_PlayerIdx == 1) ? PlayerOne.Name : PlayerTwo.Name;
        }

        public static KeyValuePair<int, int> GetNextBoardSize(KeyValuePair<int, int> i_CurrBoardSize)
        {
            int rows = i_CurrBoardSize.Key;
            int cols = i_CurrBoardSize.Value;

            do
            {
                if (cols != k_MaxRowCol)
                {
                    cols++;
                }
                else
                {
                    cols = k_MinRowCol;
                    rows = (rows != k_MaxRowCol) ? ++rows : k_MinRowCol;
                }
            } while ((cols * rows) % 2 != 0); // Accept only even board size

            return new KeyValuePair<int, int>(rows, cols);
        }
    }
}
