namespace B20_GameLogic
{
    public class TurnChangedEventArgs
    {
        private string m_CurrPlayerName;

        public string CurrPlayerName => this.m_CurrPlayerName;

        public TurnChangedEventArgs(string i_CurrPlayerName)
        {
            this.m_CurrPlayerName = i_CurrPlayerName;
        }
    }
}