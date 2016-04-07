using System;
using System.Collections;

namespace Mine_Sweeper
{
    public enum CellState
    {
        Discovered,
        Undiscovered,
        MineMarked,
        QuestionMarked,
    }

    public class Cell
    {
        public char userCellCharacter
        {
            get
            {
                switch (state)
                {
                    case CellState.Discovered:
                        return GetDiscoveredCharacter();
                    case CellState.Undiscovered:
                        return '■';
                    case CellState.MineMarked:
                        return '★';
                    case CellState.QuestionMarked:
                        return '？';
                }
                throw new Exception("Exception in userCellCharacter!");
            }
        }

        public CellState state { get; set; }
        public bool mineHave { get; set; }
        public int nearMineCount { get; private set; }

        private char[] numberCharacters = new char[] { '①', '②', '③', '④', '⑤', '⑥', '⑦', '⑧'};

        public Cell()
        {
            state = CellState.Undiscovered;
        }

        public void SetNearMineCount(int num)
        {
            nearMineCount = num;
        }

        public void SetMark()
        {
            if (state == CellState.Discovered)
                return;

            if (state == CellState.Undiscovered)
                state = CellState.MineMarked;
            else if (state == CellState.MineMarked)
                state = CellState.QuestionMarked;
            else if (state == CellState.QuestionMarked)
                state = CellState.Undiscovered;
        }

        public void Dig()
        {
            state = CellState.Discovered;
        }

        char GetDiscoveredCharacter()
        {
            if (nearMineCount == 0)
                return '　';
            else
                return numberCharacters[nearMineCount - 1];
        }
    }
}
