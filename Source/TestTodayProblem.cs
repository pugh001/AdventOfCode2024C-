using AOC2024.Utility;
using Xunit;
using Xunit.Abstractions;

namespace AOC2024;

public class TestProblems
{
  private readonly ITestOutputHelper _testOutputHelper;

  public TestProblems(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  public static IEnumerable<object[]> GetTestCases()
  {
    var fileHandler = new SetupInputFile();
    string path = fileHandler.GetSolutionDirectory();
    string testDataFile = $"{path}/Source/Answers/TestData.txt";

    var lines = File.ReadAllLines(testDataFile).Skip(1) // Skip header row
      .Where(line => !string.IsNullOrWhiteSpace(line));

    foreach (string line in lines)
    {
      string[] parts = line.Split(',');
      int day = int.Parse(parts[0]);
      long answer1 = long.Parse(parts[1]);
      long answer2 = long.Parse(parts[2]);

      yield return new object[] { day, answer1, answer2 };
    }
  }

  [Theory]
  [MemberData(nameof(GetTestCases))]
  public void TestPart1(int day, long expectedAnswer1, long _)
  {
    string filePart1 = TestFiles.GetInputData(day, "Part1Example.txt");

    object? dayInstance = Activator.CreateInstance(Type.GetType($"AOC2024.Day{day}") ?? throw new InvalidOperationException());
    var methodInfo = dayInstance.GetType().GetMethod("Process");
    if (methodInfo == null)
    {
      throw new InvalidOperationException("Process method not found.");
    }

    (long result1, _) = ((long, long))methodInfo.Invoke(dayInstance, new object[] { filePart1 });
    _testOutputHelper.WriteLine($"Day {day} Part 1: {result1}");

    // Assert results
    Assert.Equal(expectedAnswer1, result1);
  }

  [Theory]
  [MemberData(nameof(GetTestCases))]
  public void TestPart2(int day, long _, long expectedAnswer2)
  {
    string filePart2 = TestFiles.GetInputData(day, "Part2Example.txt");

    object? dayInstance = Activator.CreateInstance(Type.GetType($"AOC2024.Day{day}") ?? throw new InvalidOperationException());
    var methodInfo = dayInstance.GetType().GetMethod("Process");
    if (methodInfo == null)
    {
      throw new InvalidOperationException("Process method not found.");
    }

    (_, long result2) = ((long, long))methodInfo.Invoke(dayInstance, new object[] { filePart2 });
    _testOutputHelper.WriteLine($"Day {day} Part 2: {result2}");

    // Assert results
    Assert.Equal(expectedAnswer2, result2);
  }
}