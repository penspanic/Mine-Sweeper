using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine_Sweeper
{
    class GameManager : Singleton<GameManager>
    {

        public bool isRun
        {
            get;
            private set;
        }

        private bool isDraw;


        public GameManager()
        {
            isRun = true;
            isDraw = true;
        }

        public void Run()
        {
            Init();
            while(isRun)
            {
                Update();
                if (isDraw)
                    Render();
                System.Threading.Thread.Sleep(10);
            }
            Release();
        }

        public void Init()
        {
            Console.SetWindowSize(80, 40);
            Console.SetBufferSize(80, 40);
           
            SceneManager.instance.ChangeScene(new InGameBase());
            SceneManager.instance.Init();

            Console.CursorVisible = false;
        }

        public void Update()
        {
            InputManager.instance.Update();
            SceneManager.instance.Update();
        }

        public void Render()
        {
            isDraw = false;
            Console.Clear();
            SceneManager.instance.Render();
        }

        public void Release()
        {
            SceneManager.instance.Release();
        }

        public void DrawRequest()
        {
            isDraw = true;
        }

        public void Quit()
        {
            isRun = false;
        }
    }
}