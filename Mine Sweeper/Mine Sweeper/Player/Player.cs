using System;
using Mine_Sweeper.Core.Utility;

namespace Mine_Sweeper
{
    class Player
    {
        public Point selectedPos
        {
            get;
            private set;
        }

        InGameBase ingameScene;

        public Player()
        {
            ingameScene = SceneManager.instance.currScene as InGameBase;
        }


        public void Update()
        {
            KeyProcess();
        }

        public void Render()
        {
            ShowCursor();
        }

        void KeyProcess()
        {
            CursorMove();
            Select();
        }

        void CursorMove()
        {
            Point movePos = new Point(
                (InputManager.instance.KeyDown(ConsoleKey.LeftArrow) ? -1 : 0) +
                (InputManager.instance.KeyDown(ConsoleKey.RightArrow) ? 1 : 0),
                (InputManager.instance.KeyDown(ConsoleKey.UpArrow) ? -1 : 0) +
                (InputManager.instance.KeyDown(ConsoleKey.DownArrow) ? 1 : 0));

            Point newPos = selectedPos + movePos;

            if (newPos == selectedPos)
                return;

            if (newPos.x < 0 || newPos.x > ingameScene.currMap.ColumnSize - 1 ||
                newPos.y < 0 || newPos.y > ingameScene.currMap.RowSize - 1)
                return;

            selectedPos = newPos;
            GameManager.instance.DrawRequest();
        }

        void Select()
        {
            if (InputManager.instance.KeyDown(ConsoleModifiers.Shift))
            {
                if(InputManager.instance.KeyDown(ConsoleKey.Spacebar))
                {
                    // Mine mark or Question mark
                    ingameScene.currMap.SetMark(selectedPos);
                }
            }
            else
            {
                if(InputManager.instance.KeyDown(ConsoleKey.Spacebar))
                {
                    // Dig
                    ingameScene.currMap.Dig(selectedPos);
                }
            }
        }

        void ShowCursor()
        {
            ConsoleUtil.Write('▣', new Point(selectedPos.x * 2, selectedPos.y));
        }
    }
}