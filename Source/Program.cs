using AOC2024.Utility;

namespace AOC2024;

static internal class Program
{
  public static void Main(string[] args)
  {
    for (int day = 1; day < 25; day++)
    {
      string inputFilePath = TestFiles.GetInputData(day, "puzzleInput.txt");
      object? dayInstance = Activator.CreateInstance(Type.GetType($"AOC2024.Day{day}") ?? throw new InvalidOperationException());
      Console.Write("Day " + day + ":");
      Console.WriteLine(dayInstance.GetType().GetMethod("Process").Invoke(dayInstance, new object[] { inputFilePath }));
    }
  }
}