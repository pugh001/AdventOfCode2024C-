using System.Collections.Concurrent;
using AOC2024.Utility;

namespace AOC2024;

public class Day6
{
    public static (long, long) Process(string input)
    {
        // Load and parse input data
        string[] data = new SetupInputFile().OpenFile(input).ToArray();
        return (ProcessPart1(data), ProcessPart2(data));
    }

    private static long ProcessPart1(string[] data)
    {
        (char[,] grid, int guardRow, int guardCol) = InitializeGrid(data);
        (int, int)[] deltas = { (-1, 0), (0, 1), (1, 0), (0, -1) };
        int dirIndex = 0;

        // Simulate guard movement
        while (true)
        {
            int newRow = guardRow + deltas[dirIndex].Item1;
            int newCol = guardCol + deltas[dirIndex].Item2;

            if (IsOutOfBounds(newRow, newCol, grid) || grid[newRow, newCol] == '#')
            {
                // Turn right if blocked
                dirIndex = (dirIndex + 1) % 4;
            }
            else
            {
                grid[guardRow, guardCol] = 'X'; // Mark visited
                guardRow = newRow;
                guardCol = newCol;
            }

            if (IsOutOfBounds(newRow, newCol, grid))
                break;
        }
        return grid.Cast<char>().LongCount(cell => cell == 'X');
    }

    private static long ProcessPart2(string[] data)
    {
        long obstructionCount = 0;
        (char[,] grid, int guardRow, int guardCol) = InitializeGrid(data);

        // Precompute reachable cells
        bool[,] reachable = ComputeReachable(grid, guardRow, guardCol);

        // Parallelized simulation for obstruction
        Parallel.For(0, grid.GetLength(0), i =>
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (!reachable[i, j] || grid[i, j] != '.')
                    continue;

                grid[i, j] = '#'; // Temporarily place obstruction
                if (SimulateGuardMovement(guardRow, guardCol, grid))
                    Interlocked.Increment(ref obstructionCount);
                grid[i, j] = '.'; // Revert obstruction
            }
        });

        return obstructionCount;
    }

    private static (char[,], int, int) InitializeGrid(string[] data)
    {
        int rows = data.Length;
        int cols = data[0].Length;
        char[,] grid = new char[rows, cols];
        int guardRow = -1, guardCol = -1;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = data[i][j];
                if (grid[i, j] != '^')
                    continue;

                guardRow = i;
                guardCol = j;
                grid[i, j] = '.'; // Remove guard marker
            }
        }

        return (grid, guardRow, guardCol);
    }

    private static bool[,] ComputeReachable(char[,] grid, int guardRow, int guardCol)
    {
        bool[,] reachable = new bool[grid.GetLength(0), grid.GetLength(1)];
        Queue<(int, int)> queue = new();
        queue.Enqueue((guardRow, guardCol));
        (int, int)[] deltas = { (-1, 0), (0, 1), (1, 0), (0, -1) };

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();
            if (reachable[row, col])
                continue;

            reachable[row, col] = true;

            foreach (var (dr, dc) in deltas)
            {
                int newRow = row + dr, newCol = col + dc;
                if (!IsOutOfBounds(newRow, newCol, grid) && grid[newRow, newCol] == '.' && !reachable[newRow, newCol])
                    queue.Enqueue((newRow, newCol));
            }
        }

        return reachable;
    }

    private static bool SimulateGuardMovement(int guardRow, int guardCol, char[,] grid)
    {
        (int, int)[] deltas = { (-1, 0), (0, 1), (1, 0), (0, -1) };
        int currentRow = guardRow;
        int currentCol = guardCol;
        int dirIndex = 0;

        HashSet<int> visited = new();
        while (true)
        {
            int newRow = currentRow + deltas[dirIndex].Item1;
            int newCol = currentCol + deltas[dirIndex].Item2;

            if (IsOutOfBounds(newRow, newCol, grid))
                return false;

            if (grid[newRow, newCol] == '#')
            {
                // Turn right if blocked
                dirIndex = (dirIndex + 1) % 4;
            }
            else
            {
                // Move forward
                currentRow = newRow;
                currentCol = newCol;
            }

            // Check for loop using a compact state representation
            int state = (currentRow << 16) | (currentCol << 8) | dirIndex;
            if (!visited.Add(state))
                return true; // Loop detected
        }
    }

    private static bool IsOutOfBounds(int row, int col, char[,] grid)
        => row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1);
}
