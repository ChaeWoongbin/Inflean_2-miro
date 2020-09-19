using System;
using System.Collections.Generic;
using System.Text;

namespace inflearn_2
{
    class Player
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        Random rand = new Random();

        Board _board;
        public void Initialize(int posX, int posY, int destX, int destY, Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;

        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                int randValue = rand.Next(0, 5);

                switch (randValue)
                {
                    case 0:// 상
                        if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                        {
                            PosY = PosY - 1;
                        }
                        break;
                    case 1: // 하
                        if (PosY + 1 < _board._size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                        {
                            PosY = PosY + 1;
                        }
                        break;
                    case 2: // 좌
                        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                        {
                            PosX = PosX - 1;
                        }
                        break;
                    case 3: // 우
                        if (PosY + 1 < _board._size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                        {
                            PosX = PosX + 1;
                        }
                        break;
                }
            }
        }
    }

}
