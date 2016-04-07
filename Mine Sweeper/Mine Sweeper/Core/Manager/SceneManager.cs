using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine_Sweeper
{
    class SceneManager : Singleton<SceneManager>
    {

        public IScene currScene
        {
            get;
            private set;
        }

        public SceneManager()
        {
           
        }

        public void Init()
        {
            currScene.Init();
        }

        public void Update()
        {
            currScene.Update();
        }

        public void Render()
        {
            currScene.Render();
        }

        public void Release()
        {
            currScene.Release();
        }

        public void ChangeScene(IScene scene)
        {
            if (currScene != null)
                currScene.Release();
            currScene = scene;
            currScene.Init();

            GameManager.instance.DrawRequest();
        }
    }
}
