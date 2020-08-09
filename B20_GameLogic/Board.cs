using System;
using System.Collections.Generic;
using System.Linq;


namespace B20_GameLogic
{
    public class Board
    {
        private readonly int r_Rows;
        private readonly int r_Columns;
        private Card[,] m_Cards;
        
        public int Rows => this.r_Rows;
        public int Cols => this.r_Columns;
        public int NumOfPairs => ((this.r_Columns * this.r_Rows) / 2);
        internal Card[,] Cards => this.m_Cards;
        
        public Board(int i_Rows, int i_Cols)
        {
            this.r_Rows = i_Rows;
            this.r_Columns = i_Cols;
            this.m_Cards = new Card[i_Rows, i_Cols];
            setUpBoard(); // Setup this board with random values
        }

        private void setUpBoard()
        {
            int idx = 0;
            char[] pairArr = randomArrOfPairs();

            for (int j = 0; j < this.r_Rows; j++)
            {
                for (int k = 0; k < this.r_Columns; k++)
                {
                    Card tempc = new Card(pairArr[idx], j, k); // Set new card with board location
                    this.m_Cards[j, k] = tempc;
                    idx++;
                }
            }
        }

        public Card GetCardByIdx(int i_RowIdx, int i_ColIdx)
        {
            return Cards[i_RowIdx, i_ColIdx];
        }

        private char[] randomArrOfPairs()
        {
            List<int> pairsList = new List<int>();
            for (int i = 0; i < NumOfPairs; i++)
            {
                pairsList.Add(i);
                pairsList.Add(i);
            }
            // Randomize list
            var rnd = new Random();
            var result = pairsList.OrderBy(item => rnd.Next());

            // Set as int array
            char[] pairArr = new char[NumOfPairs * 2];
            int index = 0;

            foreach (var item in result)
            {
                pairArr[index] = (char)('A' + item);
                index++;
            }

            return pairArr;
        }

        internal void FlipDown(Card[] i_CardsToFlip, int i_FlippedByIdx)
        {
            foreach (Card c in i_CardsToFlip)
            {
                this.m_Cards[c.RowIdx, c.ColIdx].Flip(i_FlippedByIdx);
            }
        }

        internal List<Card> GetRevealedCards(bool i_DesiredStatus)
        {
            List<Card> cardList = new List<Card>();
            // Revealed/non-revealed list of cards according to desiredStatus
            foreach (Card c in this.Cards)
            {
                if (i_DesiredStatus)
                {
                    if (c.IsRevealed)
                    {
                        cardList.Add(c);
                    }
                }
                else
                {
                    if (!c.IsRevealed)
                    {
                        cardList.Add(c);
                    }
                }
            }

            return cardList;
        }

        public bool IsDone()
        {
            bool isDone = true;
            // All cards are revealed == board is done
            foreach (Card c in this.m_Cards)
            {
                if (!c.IsRevealed)
                {
                    isDone = !isDone;
                    break;
                }
            }

            return isDone;
        }
    }
}

