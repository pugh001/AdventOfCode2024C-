using AOC2024.Utility;
using Xunit;
using Xunit.Abstractions;

namespace AOC2024;

public class TestDay5(ITestOutputHelper testOutputHelper)
{
  private const int Day = 5;


  public void Day5Part1()
  {

    string filePart1 = TestFiles.GetInputData(Day, "Part1Example.txt");
    (long result1, _) = Day5.Process(filePart1);
    // Assert results
    Assert.Equal(143, result1);
  }


  public void Day5Part2()
  {

    string filePart1 = TestFiles.GetInputData(Day, "Part2Example.txt");
    (_, long result2) = Day5.Process(filePart1);
    // Assert results
    Assert.Equal(123, result2);
  }
}