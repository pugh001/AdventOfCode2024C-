using AOC2024.Utility;

namespace AOC2024;

public class Day7
{
  public static (long, long) Process(string input)
  {
    // Load and parse input data
    string[] data = new SetupInputFile().OpenFile(input).ToArray();
    return (ProcessPart1(data), ProcessPart2(data));
  }

  private static long ProcessPart1(string[] data)
  {
    long totalCalibrationResult = 0;
    foreach (string dataItem in data)
    {
      string[] allValues = dataItem.Split(':');
      long answer = long.Parse(allValues[0]);
      long[] sumValues = Array.ConvertAll(allValues[1].Trim().Split(' '), long.Parse);

      if (CanEvaluateToTarget(sumValues, answer, 1))
      {
        totalCalibrationResult += answer;
      }
    }

    return totalCalibrationResult;


  }

  private static long ProcessPart2(string[] data)
  {
    long totalCalibrationResult = 0;
    foreach (string dataItem in data)
    {
      string[] allValues = dataItem.Split(':');
      long answer = long.Parse(allValues[0]);
      long[] sumValues = Array.ConvertAll(allValues[1].Trim().Split(' '), long.Parse);

      if (CanEvaluateToTarget(sumValues, answer, 2))
      {
        totalCalibrationResult += answer;
      }
    }

    return totalCalibrationResult;
  }


  private static bool CanEvaluateToTarget(long[] numbers, long targetValue, int partOn)
  {
    // Generate all combinations of operators
    int numOperators = numbers.Length - 1;
    List<string> operatorCombinations = partOn == 1 ?
      GenerateOperatorCombinationsPart1(numOperators) :
      GenerateOperatorCombinationsPart2(numOperators);
    foreach (string operators in operatorCombinations)
    {
      if (EvaluateExpression(numbers, operators) == targetValue)
      {
        return true;
      }
    }

    return false;
  }

  private static List<string> GenerateOperatorCombinationsPart1(int numOperators)
  {
    var combinations = new List<string>();
    int totalCombinations = (int)Math.Pow(2, numOperators);

    for (int i = 0; i < totalCombinations; i++)
    {
      char[] ops = new char[numOperators];
      for (int j = 0; j < numOperators; j++)
      {
        ops[j] = (i & 1 << j) == 0 ?
          '+' :
          '*';
      }

      combinations.Add(new string(ops));
    }

    return combinations;
  }
  private static List<string> GenerateOperatorCombinationsPart2(int numOperators)
  {
    var combinations = new List<string>();
    int totalCombinations = (int)Math.Pow(3, numOperators);

    for (int i = 0; i < totalCombinations; i++)
    {
      char[] ops = new char[numOperators];
      int temp = i;
      for (int j = 0; j < numOperators; j++)
      {
        int op = temp % 3;
        ops[j] = op switch
        {
          0 => '+',
          1 => '*',
          2 => '|', // Represent concatenation as '|'
          _ => throw new InvalidOperationException("Invalid operator.")
        };
        temp /= 3;
      }

      combinations.Add(new string(ops));
    }

    return combinations;
  }
  private static long EvaluateExpression(long[] numbers, string operators)
  {
    long result = numbers[0];

    for (int i = 0; i < operators.Length; i++)
    {
      switch (operators[i])
      {
        case '+':
          result += numbers[i + 1];
          break;
        case '*':
          result *= numbers[i + 1];
          break;
        case '|': // Concatenation operator
          result = Concatenate(result.ToString(), numbers[i + 1].ToString());
          break;
      }
    }

    return result;
  }

  private static long Concatenate(string left, string right)
  {
    string concatenated = left + right;
    return long.Parse(concatenated);
  }
}