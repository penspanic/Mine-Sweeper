using System;
using System.Collections.Generic;

namespace Mine_Sweeper
{
    class InGameBase : IScene
    {
        public Map currMap
        { get; private set; }
        public Player currPlayer
        { get; private set; }

        public void Init()
        {
            currMap = new Map(Difficulty.Easy);
            currPlayer = new Player();
        }
  
        public void Update()
        {
            currPlayer.Update();
        }

        public void Render()
        {
            currMap.Render();
            currPlayer.Render();
        }

        public void Release()
        {

        }

        
        public void GameOver()
        {
            GameManager.instance.Quit();
        }

        public void GameClear()
        {
            SceneManager.instance.ChangeScene(new GameEnd());
        }
    }
}