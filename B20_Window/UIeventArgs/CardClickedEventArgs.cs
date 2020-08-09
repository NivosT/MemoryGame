using System;

namespace B20_Window
{
    public class CardClickedEventArgs : EventArgs
    {
        private readonly int r_Row;
        private readonly int r_Col;

        public int Row => this.r_Row;
        public int Col => this.r_Col;

        public CardClickedEventArgs(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }
    }
}
