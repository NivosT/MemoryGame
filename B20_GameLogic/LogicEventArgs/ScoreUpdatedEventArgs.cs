using System;

namespace B20_GameLogic
{
    public class ScoreUpdatedEventArgs : EventArgs
    {
        private int m_PlayerIdx;
        private int m_NewScore;

        public int PlayerIdx => this.m_PlayerIdx;
        public int NewScore => this.m_NewScore;

        public ScoreUpdatedEventArgs(int i_PlayerIdx, int i_NewScore)
        {
            this.m_PlayerIdx = i_PlayerIdx;
            this.m_NewScore = i_NewScore;
        }
    }
}