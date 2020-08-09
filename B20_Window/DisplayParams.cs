using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Window
{
    public struct DisplayParams
    {
        string m_FirstPlayerName;
        Color m_FirstPlayerColor;
        string m_SecondPlayerName;
        Color m_SecondPlayerColor;
        int m_Rows;
        int m_Cols;

        public string FirstPlayerName { get => m_FirstPlayerName; set => m_FirstPlayerName = value; }
        public Color FirstPlayerColor { get => m_FirstPlayerColor; set => m_FirstPlayerColor = value; }
        public string SecondPlayerName { get => m_SecondPlayerName; set => m_SecondPlayerName = value; }
        public Color SecondPlayerColor { get => m_SecondPlayerColor; set => m_SecondPlayerColor = value; }
        public int Rows { get => m_Rows; set => m_Rows = value; }
        public int Cols { get => m_Cols; set => m_Cols = value; }
    }
}
