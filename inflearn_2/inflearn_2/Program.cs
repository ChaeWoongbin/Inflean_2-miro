using System;

namespace inflearn_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Board board = new Board();
            board.Initialize(25,player);
            player.Initialize(1,1,board);

            Console.CursorVisible = false;


            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;

            board.complete = false;

            while (true)
            {
                #region 프레임관리
                int currentTick = System.Environment.TickCount;
                if(currentTick - lastTick < WAIT_TICK)
                {
                    continue;
                }
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion
                /*
                if (board.complete)
                {
                    player = new Player();
                    board = new Board();
                    board.Initialize(25, player);
                    player.Initialize(1, 1, board);

                    board.complete = false;
                }
                */
                player.Update(deltaTick);

                Console.SetCursorPosition(0,0);
                board.Render();
            }
        }

    }
}
