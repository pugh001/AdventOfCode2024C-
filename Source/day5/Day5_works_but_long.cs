using System.Text.RegularExpressions;
using AOC2024.Utility;

namespace AOC2024;

public class Day5_works_but_long
{
  private static readonly Dictionary<int, List<int>> _pageOrder = new();

  public static (long, long) Process(string input)
  {
    long resultPart1 = 0;
    long resultPart2 = 0;
    // Load and parse input data
    string[] data = new SetupInputFile().OpenFile(input).ToArray();
    int position = 0;
    foreach (string line in data)
    {
      position++;
      if (line == "") break;

      if (Regex.IsMatch(line, @"^\d{1,2}|\d{1,2}$"))
      {
        loadPageOrder(line);
      }
    }

    for (int i = position; i < data.Length; i++)
    {
      string line = data[i];
      //split as page list into array.
      int[] pages = line.Split(',').Select(int.Parse).ToArray();
      if (pageInOrder(pages))
      {
        resultPart1 += pages[pages.Length / 2];
      }
      else
      {
        resultPart2 += pageInOrderPart2(pages);

      }
    }


    return (resultPart1, resultPart2);


  }
  private static int pageInOrderPart2(int[] pages)
  {
    var skipped = new List<int>();
    var skippedPostion = new List<int>();
    for (int i = 0; i < pages.Length; i++)
    {
      int key = pages[i];
      if (!_pageOrder.ContainsKey(key))
      {
        skipped.Add(key);
        skippedPostion.Add(i);
        continue; //return false;
      }

      for (int j = i + 1; j < pages.Length; j++)
      {
        int checkValue = pages[j];
        if (!_pageOrder[key].Contains(checkValue))
        {
          (pages[i], pages[j]) = (pages[j], pages[i]);
          return pageInOrderPart2(pages);
        }
      }

      for (int j = 0; j < skipped.Count; j++)
      {
        int checkValue = skipped[j];
        if (_pageOrder[key].Contains(checkValue))
        {
          (pages[skippedPostion[j]], pages[j]) = (pages[j], pages[skippedPostion[j]]);
          return pageInOrderPart2(pages);
        }
      }
    }

    return pages[pages.Length / 2];

  }
  private static bool pageInOrder(int[] pages)
  {
    var skipped = new List<int>();
    for (int i = 0; i < pages.Length; i++)
    {
      int key = pages[i];
      if (!_pageOrder.ContainsKey(key))
      {
        skipped.Add(key);
        continue; //return false;
      }

      for (int j = i + 1; j < pages.Length; j++)
      {
        int checkValue = pages[j];
        if (!_pageOrder[key].Contains(checkValue))
        {
          return false;
        }
      }

      if (skipped.Any(checkValue => _pageOrder[key].Contains(checkValue)))
      {
        return false;
      }
    }

    return true;

  }

  private static void loadPageOrder(string line)
  {

    //spilt as key / value
    int[] values = line.Split('|').Select(int.Parse).ToArray();
    if (!_pageOrder.ContainsKey(values[0]))
    {
      _pageOrder[values[0]] = new List<int>();
    }

    _pageOrder[values[0]].Add(values[1]);
  }
}