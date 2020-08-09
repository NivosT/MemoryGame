using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_GameLogic
{
    public class Card
    {
        private readonly char r_Value;
        private readonly int r_RowIdx;
        private readonly int r_ColIdx;
        private bool m_IsRevealed;

        internal char Value => this.r_Value;
        internal int RowIdx => this.r_RowIdx;
        internal int ColIdx => this.r_ColIdx;
        internal bool IsRevealed { get => this.m_IsRevealed; set => this.m_IsRevealed = value; }
        
        public event Game.CardFlippedEventHandler CardFlipped;

        public Card(char i_Val, int i_RowIdx, int i_ColIdx)
        {
            this.r_Value = i_Val;
            this.r_RowIdx = i_RowIdx;
            this.r_ColIdx = i_ColIdx;
            this.m_IsRevealed = false;
        }

        internal void Flip(int i_FlippedByIdx)
        {
            this.IsRevealed = !this.IsRevealed;
            OnFlip(i_FlippedByIdx);
        }

        public void OnFlip(int i_FlippedByIdx)
        {
            CardFlipped?.Invoke(this, new CardFlippedEventArgs(this, i_FlippedByIdx));
        }

        internal bool Equals(Card i_CardToCompare)
        {
            return (this.RowIdx == i_CardToCompare.RowIdx && this.ColIdx == i_CardToCompare.ColIdx);
        }
    }
}
