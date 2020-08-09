using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace B20_GameLogic
{
    public class CardFlippedEventArgs : EventArgs
    {
        private readonly char r_Value;
        private readonly int r_FlippedByIdx;
        private readonly int r_Row;
        private readonly int r_Col;
        private readonly bool r_IsRevealed;

        public char Value => this.r_Value;
        public int FlippedByIdx => this.r_FlippedByIdx;
        public int Row => this.r_Row;
        public int Col => this.r_Col;
        public bool IsRevealed => this.r_IsRevealed;

        public CardFlippedEventArgs(Card i_CardForArgs, int i_FlippedByIdx)
        {
            r_Value = i_CardForArgs.Value;
            r_FlippedByIdx = i_FlippedByIdx;
            r_Row = i_CardForArgs.RowIdx;
            r_Col = i_CardForArgs.ColIdx;
            r_IsRevealed = i_CardForArgs.IsRevealed;
        }
    }
}
