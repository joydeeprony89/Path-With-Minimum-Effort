using System;
using System.Collections.Generic;

namespace Path_With_Minimum_Effort
{

  // https://leetcode.com/problems/path-with-minimum-effort/discuss/1000195/Thought-Process-from-Naive-BFS-to-Dijkstra
  class Program
  {
    static void Main(string[] args)
    {
      Solution sol = new Solution();
      var heights = new int[3][] { new int[] { 1, 2, 2 }, new int[] { 3, 8, 2 }, new int[] { 5, 3, 5 } };
      Console.WriteLine(sol.MinimumEffortPath(heights));
    }
  }

  public class Solution
  {
    class Pair
    {
      public int i;
      public int j;
      public Pair(int i, int j)
      {
        this.i = i;
        this.j = j;
      }
    }

    public int MinimumEffortPath(int[][] heights)
    {
      int m = heights.Length;
      int n = heights[0].Length;
      int[,] cost = new int[m, n];
      for (int i = 0; i < m; i++)
      {
        for (int j = 0; j < n; j++)
        {
          cost[i,j] = -1;
        }
      }

      List<int[]> directions = new List<int[]>() { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
      Queue<Pair> queue = new Queue<Pair>();
      queue.Enqueue(new Pair(0, 0));
      while (queue.Count > 0)
      {
        var current = queue.Dequeue();
        var currentRow = current.i;
        var currentColumn = current.j;
        var currentCost = cost[currentRow, currentColumn];

        foreach(var dir in directions)
        {
          var newRow = currentRow + dir[0];
          var newColumn = currentColumn + dir[1];
          if(NewPositionIsValid(newRow, newColumn, heights))
          {
            var neighbourhoodCost = cost[newRow, newColumn];
            int newCost = Math.Max(currentCost, Math.Abs(heights[currentRow][currentColumn] - heights[newRow][newColumn]));

            // if current cost is less than previous, visit it
            if (neighbourhoodCost == -1 || newCost < neighbourhoodCost)
            {
              cost[newRow,newColumn] = newCost;
              queue.Enqueue(new Pair(newRow, newColumn));
            }
          }
        }
      }

      int finalEffort = cost[m - 1, n - 1];
      return finalEffort == -1 ? 0 : finalEffort;
    }

    private bool NewPositionIsValid(int newRow, int newColumn, int[][] heights)
    {
      if (newRow < 0 || newRow >= heights.Length || newColumn < 0 || newColumn >= heights[0].Length) return false;
      return true;
    }
  }
}
