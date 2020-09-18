using System;

namespace inflearn_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize(25);

            Console.CursorVisible = false;


            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;

            while (true)
            {
                Console.SetCursorPosition(0,0);
                board.Render();
            }
        }
    }
}
