using System;
using System.Collections.Generic;
using System.Text;

namespace inflearn_2
{
   
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] _tile;
        public int _size;

        public enum TileType
        {
            Empty,
            Wall,
        }

        public void Initialize(int size)
        {
            if (size % 2 == 0) return; // 미로는 짝수

            _tile = new TileType[size, size];
            _size = size;

            //Mazes for Programmers

            GenerateByBinaryTree();
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
                        _tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        _tile[y, x] = TileType.Empty;
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
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    // 외곽벽 
                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 랜덤
                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;
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

                    Console.ForegroundColor = GetTileColor(_tile[y,x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }
    }
}
        
