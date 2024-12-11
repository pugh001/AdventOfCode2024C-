using AOC2024.Utility;

namespace AOC2024;

public class Day1
{
  public static (long, long) Process(string input)
  {
    var data = new SetupInputFile().OpenFile(input);
    long resultPart1 = 0, resultPart2 = 0;

    var list1 = new List<long>();
    var list2 = new List<long>();

    foreach (string line in data)
    {
      string[] x = line.Split(new[] { "   " }, StringSplitOptions.None);
      list1.Add(Convert.ToInt64(x[0]));
      list2.Add(Convert.ToInt64(x[1]));
    }

    list1.Sort();
    list2.Sort();
    for (int i = 0; i < list1.Count; i++)
    {
      int leftOccur = list2.Count(x => x.Equals(list1[i]));
      resultPart1 += Math.Abs(list1[i] - list2[i]);
      resultPart2 += list1[i] * leftOccur;
    }

    return (resultPart1, resultPart2);
  }
}