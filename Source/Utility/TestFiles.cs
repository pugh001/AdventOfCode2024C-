namespace AOC2024.Utility;

public class TestFiles
{
  public static string GetInputData(int day, string myFile)
  {
    var fileHandler = new SetupInputFile();
    string path = fileHandler.GetSolutionDirectory();
    string fileOne = $"{path}/Source/Day{day}/{myFile}";
    return fileOne;
  }
}