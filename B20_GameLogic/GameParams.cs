using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_GameLogic
{
    public struct GameParams
    {
        string m_FirstPlayerName;
        string m_SecondPlayerName;
        bool m_SecondIsBot;
        KeyValuePair<int,int> m_BoardSize;

        public string FirstPlayerName { get => m_FirstPlayerName; set => m_FirstPlayerName = value; }
        public string SecondPlayerName { get => m_SecondPlayerName; set => m_SecondPlayerName = value; }
        public bool SecondIsBot { get => m_SecondIsBot; set => m_SecondIsBot = value; }
        public KeyValuePair<int,int> BoardSize { get => m_BoardSize; set => m_BoardSize = value; }
    }
}
