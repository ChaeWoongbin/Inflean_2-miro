﻿using System;
using System.Collections.Generic;
using System.Text;

namespace inflearn_2
{
    class position
    {
        public position(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }
    class Player
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        Random rand = new Random();

        Board _board;


        enum Dir
        {
            Up,
            Left,
            Down,
            Right
        }

        int _dir = (int)Dir.Up;

        List<position> _points = new List<position>();


        public void Initialize(int posX, int posY, Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;


            //RightHands(board); // 우수법
            BFS();

        }

        void BFS()
        {
            int[] deltaY = new int[] { -1, 0, 1, 0 };
            int[] deltaX = new int[] { 0, -1, 0, 1 };

            bool[,] found = new bool[_board._size, _board._size];
            position[,] parent = new position[_board._size, _board._size];

            Queue<position> q = new Queue<position>();

            q.Enqueue(new position(PosY, PosX));
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new position(PosY, PosX);

            while (q.Count > 0)
            {
                position pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X; 

                for(int i=0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];

                    if(nextX < 0 || nextX >= _board._size || nextY < 0 || nextY >= _board._size)
                    {
                        continue;
                    }

                    if(_board.Tile[nextY, nextX] == Board.TileType.Wall)
                    {
                        continue;
                    }
                    if(found[nextY, nextX])
                    {
                        continue;
                    }

                    q.Enqueue(new position(nextY,nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new position(nowY, nowX);
                }
            }

            int y = _board.DestY;
            int x = _board.DestX;

            while (parent[y,x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new position(y,x));
                position pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            _points.Add(new position(y, x));
            _points.Reverse();
        }

        void RightHands(Board board)
        {
            // 현재 보는 방향기준, 좌표변화
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] RightY = new int[] { 0, -1, 0, 1 };
            int[] RightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new position(PosY, PosX));

            // 길 찾기 + 업데이트
            while (PosY != board.DestY || PosX != board.DestX) // 목적지 도착까지
            {
                // 1.현재 방향에서 오른쪽 으로 갈 수 있는지
                if (board.Tile[PosY + RightY[_dir], PosX + RightX[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽 90도 회전
                    _dir = (_dir - 1 + 4) % 4;
                    // 1보 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new position(PosY, PosX));

                }
                // 2. 현재 바라보는 방향에서 전진 할 수 있는지
                else if (board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    // 1보전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new position(PosY, PosX));
                }
                else
                {
                    // 왼쪽 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                }

            }
        }


        const int MOVE_TICK = 30;
        int _sumTick = 0;
        int lastIndex = 0;

        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                if (lastIndex >= _points.Count)
                {
                    _board.complete = true;
                    return;
                }
                _sumTick = 0;

                PosY = _points[lastIndex].Y;
                PosX = _points[lastIndex].X;
                lastIndex++;
                /*
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
                }*/
            }
        }
    }

}
