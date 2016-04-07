using System;
using System.Collections.Generic;
using Mine_Sweeper.Core.Utility;

namespace Mine_Sweeper
{
    class Map
    {
        public static readonly int EasyModeColumn = 9;
        public static readonly int EasyModeRow = 9;
        public static readonly int EasyModeMineCount = 9;

        public static readonly int NormalModeColumn = 16;
        public static readonly int NormalModeRow = 16;
        public static readonly int NormalModeMineCount = 40;

        public static readonly int HardModeColumn = 30;
        public static readonly int HardModeRow = 16;
        public static readonly int HardModeMineCount = 99;

        public int ColumnSize
        { get; private set; }
        public int RowSize
        { get; private set; }
        public int MineCount
        { get; private set; }

        private InGameBase ingameScene;
        private Cell[,] mapData;
        private List<Cell> currCleanCellList = new List<Cell>();
        private Difficulty difficulty;


        private bool mineCreated = false;

        public Map(Difficulty difficulty)
        {
            ingameScene = SceneManager.instance.currScene as InGameBase;

            this.difficulty = difficulty;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    ColumnSize = EasyModeColumn;
                    RowSize = EasyModeRow;
                    MineCount = EasyModeMineCount;
                    break;
                case Difficulty.Normal:
                    ColumnSize = NormalModeColumn;
                    RowSize = NormalModeRow;
                    MineCount = NormalModeMineCount;
                    break;
                case Difficulty.Hard:
                    ColumnSize = HardModeColumn;
                    RowSize = HardModeRow;
                    MineCount = HardModeMineCount;
                    break;
            }

            mapData = new Cell[ColumnSize, RowSize];
            for (int i = 0; i < mapData.Length; i++)
            {
                mapData[i % ColumnSize, i / ColumnSize] = new Cell();
            }
        }

        public void Render()
        {
            for (int row = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColumnSize; col++)
                {
                    ConsoleUtil.Write(mapData[col, row].userCellCharacter, new Point(col * 2, row));
                }
            }
        }

        public void SetMark(Point pos)
        {
            mapData[pos.x, pos.y].SetMark();
        }

        public void Dig(Point pos)
        {
            if (!mineCreated)
                CreateMine(pos);

            Cell targetCell = mapData[pos.x, pos.y];
            Cell[] nearCells = GetNearCells(targetCell, pos);

            int nearMineMarkedCount = Array.FindAll(nearCells, (cell) =>
            {
                return cell.state == CellState.MineMarked;
            }).Length;

            if (nearMineMarkedCount == targetCell.nearMineCount)
            {
                for (int i = 0; i < nearCells.Length; i++)
                {
                    if (nearCells[i].mineHave)
                    {
                        if (nearCells[i].state != CellState.MineMarked)
                        {
                            ingameScene.GameOver();
                            return;
                        }
                    }
                    else
                    {
                        nearCells[i].Dig();
                        if (nearCells[i].nearMineCount == 0)
                        {

                            DigCleanCell(nearCells[i], GetCellPos(nearCells[i]));
                        }
                    }
                }
            }
            else
            {
                if (targetCell.mineHave)
                    ingameScene.GameOver();
                else
                    targetCell.Dig();
            }
            if (IsGameClear())
                ingameScene.GameClear();
            GameManager.instance.DrawRequest();
        }

        void CreateMine(Point digPos)
        {
            mineCreated = true;

            int currMineCount = 0;
            Random r = new Random();

            while (currMineCount < MineCount)
            {
                Point newPos = new Point(r.Next(0, ColumnSize), r.Next(0, RowSize));

                if (newPos.x > digPos.x - 2 && newPos.x < digPos.x + 2 &&
                                      newPos.y > digPos.y - 2 && newPos.y < digPos.y + 2)
                    continue;

                if (mapData[newPos.x, newPos.y].mineHave)
                    continue;

                mapData[newPos.x, newPos.y].mineHave = true;
                currMineCount++;

            }

            SetNearMineCount();
        }

        void SetNearMineCount()
        {
            Cell[] nearCells = null;

            for (int row = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColumnSize; col++)
                {
                    int nearMineCount = 0;
                    nearCells = GetNearCells(mapData[col, row], new Point(col, row));
                    nearMineCount = Array.FindAll(nearCells, (cell) =>
                    {
                        return cell.mineHave;
                    }).Length;

                    mapData[col, row].SetNearMineCount(nearMineCount);
                }
            }
        }

        void DigCleanCell(Cell targetCell, Point pos)
        {
            currCleanCellList.Add(targetCell);

            List<Cell> nearCellList = new List<Cell>(GetNearCells(targetCell, pos));
            List<Cell> newNearCellList = new List<Cell>(nearCellList);

            for (int i = 0; i < nearCellList.Count; i++)
            {
                Cell currCell = nearCellList[i];
                if (currCleanCellList.Contains(currCell))
                    newNearCellList.Remove(currCell);
            }

            nearCellList = newNearCellList;

            for (int i = 0; i < nearCellList.Count; i++)
            {
                nearCellList[i].Dig();
                if (nearCellList[i].nearMineCount == 0)
                    DigCleanCell(nearCellList[i], GetCellPos(nearCellList[i]));
            }
        }

        Cell[] GetNearCells(Cell targetCell, Point targetPos)
        {
            List<Cell> nearCellList = new List<Cell>();

            for (int row = targetPos.y - 1; row < targetPos.y + 2; row++)
            {
                for (int col = targetPos.x - 1; col < targetPos.x + 2; col++)
                {
                    try // For index out of range exception
                    {
                        nearCellList.Add(mapData[col, row]);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return nearCellList.ToArray();
        }

        Point GetCellPos(Cell targetCell)
        {
            for (int row = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColumnSize; col++)
                {
                    if (mapData[col, row] == targetCell)
                        return new Point(col, row);
                }
            }
            throw new ArgumentException("There is no cell in this map!");
        }

        bool IsGameClear()
        {
            for (int row = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColumnSize; col++)
                {
                    if (mapData[col, row].mineHave && mapData[col, row].state != CellState.MineMarked)
                        return false;
                }
            }
            return true;
        }
    }
}