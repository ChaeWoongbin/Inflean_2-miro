using System;
using System.Collections.Generic;
using System.Text;

namespace inflearn_2
{
   
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; }
        public int _size { get; private set; }

        public int DestX { get; private set; }
        public int DestY { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }

        public void Initialize(int size, Player player)
        {
            _player = player;
            if (size % 2 == 0) return; // 미로는 짝수

            Tile = new TileType[size, size];
            _size = size;

            DestX = _size - 2;
            DestY = _size - 2;

            //Mazes for Programmers

            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        void GenerateBySideWinder()
        {
            // 벽 생성
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    //if(x==0 || x == _size -1 || y ==0 || y == _size -1)
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        Tile[y, x] = TileType.Empty;
                    }
                }
            }

            // 길뚫기
            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                int count = 1;
                for (int x = 0; x < _size; x++)
                {
                    //if(x==0 || x == _size -1 || y ==0 || y == _size -1)
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        continue;
                    }

                    if (y == _size - 2 && x == _size - 2)
                    {
                        continue;
                    }

                    // 외곽벽 
                    if (y == _size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    // 외곽벽 
                    if (x == _size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 랜덤
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }

                }
            }

        }

        void GenerateByBinaryTree()
        {
            // 벽 생성
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    //if(x==0 || x == _size -1 || y ==0 || y == _size -1)
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        Tile[y, x] = TileType.Empty;
                    }
                }
            }

            // binary tree 알고리즘
            // 길뚫기
            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    //if(x==0 || x == _size -1 || y ==0 || y == _size -1)
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        continue;
                    }


                    if (y == _size - 2 && x == _size - 2)
                    {
                        continue;
                    }

                    // 외곽벽 
                    if (y == _size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    // 외곽벽 
                    if (x == _size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 랜덤
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty: return ConsoleColor.Green;
                case TileType.Wall: return ConsoleColor.Red;
                default: return ConsoleColor.Green;
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for(int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x == _player.PosX && y == _player.PosY)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(CIRCLE);
                        continue;
                    }else if (x == DestX && y == DestY)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(CIRCLE);
                        continue;
                    }

                    Console.ForegroundColor = GetTileColor(Tile[y,x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }
    }
}
        
