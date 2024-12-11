using AOC2024.Utility;

namespace AOC2024;

public class Day11
{
  private static readonly Dictionary<(long, int), long> Blinks = new();

  public static (long, long) Process(string input)
  {
    var data = new SetupInputFile().OpenFile(input).First();
    var stones = data.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

    return (ResultPart1(stones, 25), ResultPart1(stones, 75));
  }

  private static long ResultPart1(List<long> stones, int blinks)
  {
    return stones.Sum(stone => ProcessAStone(stone, blinks));
  }

  private static long ProcessAStone(long stone, int count)
  {
    if (Blinks.TryGetValue((stone, count), out long cachedResult))
    {
      return cachedResult;
    }

    long result = count == 0 ? 1 : stone == 0 ? ProcessAStone(1, count - 1) :
      stone.ToString().Length % 2 == 0 ? ProcessSplitStone(stone, count) :
      ProcessAStone(stone * 2024, count - 1);

    Blinks[(stone, count)] = result;
    return result;
  }

  private static long ProcessSplitStone(long stone, int count)
  {
    string digits = stone.ToString();
    int mid = digits.Length / 2;
    long left = long.Parse(digits[..mid]);
    long right = long.Parse(digits[mid..]);
    return ProcessAStone(left, count - 1) + ProcessAStone(right, count - 1);
  }
}