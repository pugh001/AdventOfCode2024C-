using AOC2024.Utility;

namespace AOC2024;

public class Day4
{
  private static readonly int[][] Directions =
  [
    [0, 1], // Right
    [1, 0], // Down
    [0, -1], // Left
    [-1, 0], // Up
    [1, 1], // Down-right (Diagonal)
    [1, -1], // Down-left (Diagonal)
    [-1, 1], // Up-right (Diagonal)
    [-1, -1] // Up-left (Diagonal)
  ];

  private static readonly int[][] Directions2 =
  [
    [1, 1], // Down-right (Diagonal)
    [1, -1], // Down-left (Diagonal)
    [-1, 1], // Up-right (Diagonal)
    [-1, -1] // Up-left (Diagonal)
  ];

  private static int[,]? _gridA;
  public static (long, long) Process(string input)
  {
    long resultPart1 = 0;
    long resultPart2 = 0;
    // Load and parse input data
    string[] data = new SetupInputFile().OpenFile(input).ToArray();

    int rowSize = data.Length;
    int colSize = data[0].Length;
    char[,] grid = new char[rowSize, colSize];
    _gridA = new int[rowSize, colSize];

    for (int r = 0; r < rowSize; r++)
    {
      for (int c = 0; c < colSize; c++)
      {
        grid[r, c] = data[r][c];
        _gridA[r, c] = 0;
      }
    }

    resultPart1 = processGridPart1(grid, rowSize, colSize);
    processGridPart2(grid, rowSize, colSize);
    resultPart2 = CountAOccourance(rowSize, colSize);
    return (resultPart1, resultPart2);


  }
  private static long processGridPart1(char[,] grid, int rowSize, int colSize)
  {

    long counter = 0;

    for (int r = 0; r < rowSize; r++)
    {
      for (int c = 0; c < colSize; c++)
      {
        foreach (int[] direction in Directions)
        {
          if (FoundWord(grid, r, c, direction[0], direction[1], "XMAS"))
          {
            counter++;
          }
        }
      }
    }


    return counter;
  }
  private static void processGridPart2(char[,] grid, int rowSize, int colSize)
  {
    for (int r = 0; r < rowSize; r++)
    {
      for (int c = 0; c < colSize; c++)
      {
        foreach (int[] direction in Directions2)
        {
          FoundWord(grid, r, c, direction[0], direction[1], "MAS");

        }
      }
    }
  }
  private static long CountAOccourance(int rowSize, int colSize)
  {

    long aCounter = 0;
    for (int r = 0; r < rowSize; r++)
    {
      for (int c = 0; c < colSize; c++)
      {
        if (_gridA[r, c] == 2)
        {
          aCounter++;
        }
      }
    }

    return aCounter;
  }
  private static bool FoundWord(char[,] grid, int startRow, int startCol, int rowDir, int colDir, string word)
  {
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);
    int wordLength = word.Length;
    int arow = -1;
    int acol = -1;
    for (int i = 0; i < wordLength; i++)
    {
      int newRow = startRow + i * rowDir;
      int newCol = startCol + i * colDir;

      if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols || grid[newRow, newCol] != word[i])
      {
        return false;
      }

      if (word[i] != 'A')
        continue;

      acol = newCol;
      arow = newRow;


    }

    if (word == "MAS")
    {
      _gridA[arow, acol]++;
    }

    return true;
  }
}