namespace B20_GameLogic
{
    public class GameFinishedEventArgs
    {
        private string m_WinnerName;

        public string WinnerName => this.m_WinnerName;

        public GameFinishedEventArgs(string i_WinnerName)
        {
            this.m_WinnerName = i_WinnerName;
        }
    }
}