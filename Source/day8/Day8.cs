using AOC2024.Utility;

namespace AOC2024;

public class Day8
{
  public static (long, long) Process(string input)
  {
    // Load and parse input data
    string[] data = new SetupInputFile().OpenFile(input).ToArray();
    return (ProcessPart1(data), ProcessPart2(data));
  }


  private static long ProcessPart1(string[] lines)
  {


    // Create and "fill" matrix
    char[,] map = new char[lines.Length, lines[0].Length];
    for (int i = 0; i < lines.Length; i++)
    {
      for (int j = 0; j < lines[i].Length; j++)
      {
        map[i, j] = lines[i][j];
      }
    }

    var antinodes = new HashSet<(int, int)>(); // Keep track of antinodes

    // Iterate over the map to find antinodes
    for (int i = 0; i < map.GetLength(0); i++)
    {
      for (int j = 0; j < map.GetLength(1); j++)
      {
        char c = map[i, j]; // Current character

        // Skip dots
        if (c == '.') continue;

        // Find all occurrences of the same character
        var occurrences = new List<(int, int)>(); // Keep track of specific occurrences
        for (int x = 0; x < map.GetLength(0); x++)
        {
          for (int y = 0; y < map.GetLength(1); y++)
          {
            if (map[x, y] == c) occurrences.Add((x, y)); // Add occurrence of specific character
          }
        }

        // Find antinodes by checking all pairs of occurrences
        foreach ((int x1, int y1) in occurrences)
        {
          foreach ((int x2, int y2) in occurrences)
          {
            if (x1 == x2 && y1 == y2) continue; // Skip same occurrence

            int dx = x2 - x1; // Distance between x coordinates
            int dy = y2 - y1; // Distance between y coordinates

            // Calculate the antinode position
            int antinodeX = x2 + dx; // Antinode x coordinate
            int antinodeY = y2 + dy; // Antinode y coordinate

            // Check if antinode is within bounds
            if (antinodeX >= 0 && antinodeX < map.GetLength(0) && antinodeY >= 0 && antinodeY < map.GetLength(1))
            {
              antinodes.Add((antinodeX, antinodeY));
            }

            // Calculate the other antinode position
            antinodeX = x1 - dx; // Antinode x coordinate
            antinodeY = y1 - dy; // Antinode y coordinate

            // Check if antinode is within bounds
            if (antinodeX >= 0 && antinodeX < map.GetLength(0) && antinodeY >= 0 && antinodeY < map.GetLength(1))
            {
              antinodes.Add((antinodeX, antinodeY));
            }
          }
        }
      }
    }

    return antinodes.Count;
  }

  private static long ProcessPart2(string[] lines)

  {
    // Create and "fill" matrix
    char[,] map = new char[lines.Length, lines[0].Length];
    for (int i = 0; i < lines.Length; i++)
    {
      for (int j = 0; j < lines[i].Length; j++)
      {
        map[i, j] = lines[i][j];
      }
    }

    var antinodes = new HashSet<(int, int)>(); // Keep track of antinodes

    // Iterate over the map to find antinodes
    for (int i = 0; i < map.GetLength(0); i++)
    {
      for (int j = 0; j < map.GetLength(1); j++)
      {
        char c = map[i, j];

        // Skip dots
        if (c == '.') continue;

        // Find all occurrences of the same character
        var occurrences = new List<(int, int)>(); // Keep track of specificoccurrences
        for (int x = 0; x < map.GetLength(0); x++)
        {
          for (int y = 0; y < map.GetLength(1); y++)
          {
            if (map[x, y] == c) occurrences.Add((x, y)); // Add occurrence of specific character
          }
        }

        // Check if this position is an antinode
        if (occurrences.Count > 1)
        {
          antinodes.Add((i, j));
        }

        // Find other antinodes in the same line
        foreach ((int x1, int y1) in occurrences)
        {
          foreach ((int x2, int y2) in occurrences)
          {
            if (x1 == x2 && y1 == y2) continue; // Skip same occurrence

            int dx = x2 - x1; // Distance between x coordinates
            int dy = y2 - y1; // Distance between y coordinates

            // Calculate the antinode position
            for (int k = -map.GetLength(0); k <= map.GetLength(0); k++) // Iterate over all possible antinode positions
            {
              int antinodeX = x1 + k * dx; // Antinode x coordinate
              int antinodeY = y1 + k * dy; // Antinode y coordinate

              // Check if antinode is within bounds
              if (antinodeX >= 0 && antinodeX < map.GetLength(0) && antinodeY >= 0 && antinodeY < map.GetLength(1))
              {
                antinodes.Add((antinodeX, antinodeY));
              }
            }
          }
        }
      }
    }

    return antinodes.Count;
  }
}