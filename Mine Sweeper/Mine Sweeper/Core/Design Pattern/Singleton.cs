using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine_Sweeper
{
    abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;
        public static T instance
        {
            get
            {

                if (_instance == null)
                    _instance = new T();
                return _instance;

            }
        }
    }
}
