using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Window
{
    internal class Game
    {
        string m_FirstPlayerName;
        string m_SecondPlayerName;
        bool m_SecondIsBot;
        eBoardSize m_BoardSize;

        internal Game(GameParams i_GameParams)
        {
            this.m_FirstPlayerName = i_GameParams.FirstPlayerName;
            this.m_SecondPlayerName = i_GameParams.SecondPlayerName;
            this.m_SecondIsBot = i_GameParams.SecondIsBot;
            this.m_BoardSize = i_GameParams.BoardSize;

        }

    }
}
