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
        long visitedPositions = 0;
        var (grid, guardRow, guardCol) = InitializeGrid(data);

        string directions = "^>v<";
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

        // Count visited positions
        foreach (var cell in grid)
            if (cell == 'X')
                visitedPositions++;

        return visitedPositions;
    }

    private static long ProcessPart2(string[] data)
    {
        long obstructionCount = 0;
        var (grid, guardRow, guardCol) = InitializeGrid(data);

        string directions = "^>v<";
        (int, int)[] deltas = { (-1, 0), (0, 1), (1, 0), (0, -1) };

        // Find possible obstruction locations
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] != '.' || (i == guardRow && j == guardCol))
                    continue;

                if (SimulateGuardMovement(i, j, grid, guardRow, guardCol, deltas))
                    obstructionCount++;
            }
        }

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
                if (grid[i, j] == '^')
                {
                    guardRow = i;
                    guardCol = j;
                    grid[i, j] = '.'; // Remove guard marker
                }
            }
        }

        return (grid, guardRow, guardCol);
    }

    private static bool SimulateGuardMovement(
        int obstructionRow,
        int obstructionCol,
        char[,] grid,
        int guardRow,
        int guardCol,
        (int, int)[] deltas)
    {
        char[,] simulationGrid = (char[,])grid.Clone();
        simulationGrid[obstructionRow, obstructionCol] = '#';

        int currentRow = guardRow;
        int currentCol = guardCol;
        int dirIndex = 0;
        HashSet<(int, int, int)> visited = new();

        while (true)
        {
            int newRow = currentRow + deltas[dirIndex].Item1;
            int newCol = currentCol + deltas[dirIndex].Item2;

            if (IsOutOfBounds(newRow, newCol, grid))
                return false;

            if (simulationGrid[newRow, newCol] == '#')
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

            // Check for loop
            if (!visited.Add((currentRow, currentCol, dirIndex)))
                return true; // Loop detected
        }
    }

    private static bool IsOutOfBounds(int row, int col, char[,] grid)
        => row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1);
}
