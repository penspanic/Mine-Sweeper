using System;

namespace Mine_Sweeper
{
    class InputManager : Singleton<InputManager>
    {
        private bool[] prevKeyState = new bool[256];
        private bool[] currKeyState = new bool[256];

        private bool altDown = false;
        private bool ctrlDown = false;
        private bool shiftDown = false;

        public InputManager()
        {

        }

        public void Update()
        {
            prevKeyState = (bool[])currKeyState.Clone();
            for (int i = 0; i < 256; i++)
                currKeyState[i] = false;

            altDown = false;
            ctrlDown = false;
            shiftDown = false;

            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                altDown = keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt);
                ctrlDown = keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control);
                shiftDown = keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift);

                currKeyState[(int)keyInfo.Key] = true;
            }
        }

        public bool KeyDown(ConsoleKey key)
        {
            return currKeyState[(int)key];
        }

        public bool KeyDown(ConsoleModifiers modifiers)
        {
            switch(modifiers)
            {
                case ConsoleModifiers.Alt:
                    return altDown;
                case ConsoleModifiers.Control:
                    return ctrlDown;
                case ConsoleModifiers.Shift:
                    return shiftDown;
            }
            throw new ArgumentException("No " + modifiers.ToString() + " modifier defined!");
        }
    }
}
