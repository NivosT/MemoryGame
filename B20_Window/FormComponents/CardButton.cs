using B20_GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B20_Window
{
    public class CardButton : Button
    {
        private static int sr_CardButtonHeight = 100;
        private static int sr_CardButtonWidth = 100;
        
        private readonly int r_Row;
        private readonly int r_Col;

        public static int CardButtonHeight => sr_CardButtonHeight;
        public static int CardButtonWidth => sr_CardButtonWidth;
        public int Row => this.r_Row;
        public int Col => this.r_Col;

        public CardButton(int i_Row, int i_Col)
        {
            this.r_Row = i_Row;
            this.r_Col = i_Col;
            initButtonParams();
        }

        private void initButtonParams()
        {
            this.Text = "";
            this.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Size = new Size(sr_CardButtonWidth, sr_CardButtonHeight);
            this.Margin = new Padding(5, 5, 5, 5);
        }
    }
}
