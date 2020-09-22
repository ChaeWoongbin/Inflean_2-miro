using System;
using System.Collections.Generic;

namespace Execsize
{
    class Program
    {
        class Graph
        {
            // 배열 형식 그래프
            int[,] array_graph = new int[6, 6]
            {
               { 0,1,0,1,0,0 },
               { 1,0,1,1,0,0 },
               { 0,1,0,0,0,0 },
               { 1,1,0,0,1,0 },
               { 0,0,0,1,0,1 },
               { 0,0,0,0,1,0 }
            }; // 양방향 ( 대각선 대칭 )

            // List 형식 그래프
            List<int>[] List_grqaph = new List<int>[]
            {
                new List<int>() { 1, 3 },       //0
                new List<int>() { 0, 2, 3 },     //1
                new List<int>() { 1 },          //2
                new List<int>() { 0, 1,4  },    //3
                new List<int>() { 3, 5 },       //4
                new List<int>() { 4 },          //5
            };
            #region DFS
            //bool[] visited = new bool[6]; // 방문 확인
            public void DFS(int now, bool[] visited)
            {
                // now방문
                // now와 연결된 정점확인 후 , 미방문 노드 방문
                Console.WriteLine(now);
                visited[now] = true;

                //array
                for(int next = 0; next < array_graph.GetLength(0); next++)
                {
                    if(array_graph[now,next] == 0) // 연결확인
                    {
                        continue;
                    }
                    if(visited[next] == true) // 방문확인
                    {
                        continue;
                    }

                    DFS(next,visited);
                }
            }
           
            public void DFS2(int now, bool[] visited)
            {
                // now방문
                // now와 연결된 정점확인 후 , 미방문 노드 방문
                Console.WriteLine(now);
                visited[now] = true;

                //List
                foreach (int next in List_grqaph[now])
                {
                    if (visited[next] == true) // 방문확인 ( List는 연결 노드만 있음 )
                    {
                        continue;
                    }

                    DFS2(next, visited);
                }
            }

            public void SearchAll()
            {
                // 전체 순회가 되었는지 확인 ( 각 그래프가 2개 이상일때 )
                bool[] visited = new bool[6];
                for(int now = 0;  now<6; now++)
                {
                    if(visited[now] == false)
                    {
                        DFS(now, visited);
                    }
                }
            }
            #endregion // 깊이 우선 탐색, dfs, dfs2, searchal,l

            public void BFS(int start) // queue
            {
                bool[] found = new bool[6];
                int[] parent = new int[6];
                int[] distance = new int[6];

                Queue<int> q = new Queue<int>();
                q.Enqueue(start);
                found[start] = true;
                parent[start] = start;
                distance[start] = 0;

                while (q.Count > 0)
                {
                    int now = q.Dequeue();
                    Console.WriteLine(now);

                    for(int next = 0; next < array_graph.GetLength(0); next++)
                    {
                        if(array_graph[now,next] == 0)
                        {
                            continue;
                        }
                        if (found[next])
                        {
                            continue;
                        }
                        q.Enqueue(next);
                        found[next] = true;
                        parent[next] = now;
                        distance[next] = distance[now] + 1;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
           // DFS ( 깊이 우선 Depth First Search ) 
           // 끝 노드 선확인

            Graph graph = new Graph();
            //graph.DFS2(3);
            graph.BFS(0);

            // BFS ( 너비 우선 Depth First Search )
            //
        }
    }
}
