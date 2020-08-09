using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace B20_GameLogic
{
    public class Player
    {
        private readonly string r_Name;
        private readonly bool r_IsBot;
        private readonly int r_PlayerIdx;
        private int m_Score;

        public string Name => this.r_Name;
        internal bool IsBot => this.r_IsBot;
        internal int Score { get => this.m_Score; set => this.m_Score = value; }

        public event Game.ScoreUpdatedEventHandler ScoreUpdated;

        public Player(String i_Name, int i_PlayerIdx, bool i_IsBot = false) // By default, bot is not defined
        {
            this.r_Name = i_Name;
            this.r_PlayerIdx = i_PlayerIdx;
            this.r_IsBot = i_IsBot;
            this.m_Score = 0;
        }

        public void OnScoreUpdate()
        {
            ScoreUpdated?.Invoke(this, new ScoreUpdatedEventArgs(r_PlayerIdx, Score));
        }
    }
}
