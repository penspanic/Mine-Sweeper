using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine_Sweeper
{
    interface IScene
    {
        void Init();
        void Update();
        void Render();
        void Release();
    }
}
