
//Challenges to do:
//Add a function that let's the user pick the number of questions. (In my code the user can already answer how many questions he wants)
//Create a 'Random Game' option where the players will be presented with questions from random operations.
//Try to implement levels of difficulty. (Maybe)
using System.Diagnostics;

string menuSelection;
string readAnswer;
int userAnswer;
List<string> answerList = new List<string>();
int x;
int y;
int result;
int scoreRight = 0;
int totalScore = 0;
string question;

Stopwatch stopwatch = Stopwatch.StartNew();

Random random = new Random();

do
{
    Console.WriteLine("""
    Math Game
    Choose from the options below:
    1 - Sum
    2 - Subtraction
    3 - Multiplication
    4 - Division
    5 - Random Operations
    6 - Visualise your previous games and final score
    exit to end the game
    """);

    menuSelection = Console.ReadLine();

    if (menuSelection == "exit")
    {
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine($"You took {elapsed.Hours} hours, {elapsed.Minutes} minutes, and {elapsed.Seconds} seconds to finish the game.");
    }

    switch (menuSelection)
    {
        case "1":
            PlayGame(Sum);
            break;
        case "2":
            PlayGame(Subtraction);
            break;
        case "3":
            PlayGame(Multiplication);
            break;
        case "4":
            PlayGame(Division);
            break;
        case "5":
            PlayGame(GetRandomOperation(), true);
            break;
        case "6":
            VisualizePreviousGames();
            Console.WriteLine($"\nYou final score is {scoreRight}/{totalScore}");
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            break;
    }

} while (menuSelection.ToLower() != "exit");

int Sum(int x, int y) => x + y;
int Subtraction(int x, int y) => x - y;
int Multiplication(int x, int y) => x * y;
int Division(int x, int y) => x / y;

Func<int, int, int> GetRandomOperation()
{
    int option = random.Next(1, 5);

    Func<int, int, int> operation = option switch
    {
        1 => Sum,
        2 => Subtraction,
        3 => Multiplication,
        4 => Division,
        _ => throw new InvalidOperationException("Invalid option")
    };

    return operation;
}

string GetOperatorName(Func<int, int, int> operationFunction)
{
    if (operationFunction == Sum)
    {
        return "+";
    }
    else if (operationFunction == Subtraction)
    {
        return "-";
    }
    else if (operationFunction == Multiplication)
    {
        return "*";
    }
    else if (operationFunction == Division)
    {
        return "/";
    }
    else
    {
        throw new InvalidOperationException("Invalid option");
    }
}

void VisualizePreviousGames()
{
    Console.WriteLine();
    foreach (string item in answerList)
    {
        Console.WriteLine($"{item} ");
    }
}

//The Func<int, int, int> delegate type indicates a method that takes two int parameters and returns an int.
//The first 'int' is the return type, the second and third int are the first and second parameter

void PlayGame(Func<int, int, int> operationFunction, bool randomGames = false)
{
    do
    {
        Console.WriteLine("Type 'b' to go back to the menu");

        if (randomGames)
            operationFunction = GetRandomOperation();

        string operatorName = GetOperatorName(operationFunction);

        x = random.Next(0, 101);

        if (operationFunction == Division)
        {
            do
            {
                y = random.Next(1, 101);
            } while (x % y != 0);
        }
        else
        {
            y = random.Next(0, 101);
        }

        result = operationFunction(x, y);
        question = $"{x} {operatorName} {y} = ?";
        Console.WriteLine(question);
        readAnswer = Console.ReadLine();

        while (readAnswer.ToLower() != "b" && !int.TryParse(readAnswer, out userAnswer))
        {
            Console.WriteLine("Type a valid integer or 'b' to go back to the menu");
            readAnswer = Console.ReadLine();
        }

        if (readAnswer.ToLower() != "b")
        {
            answerList.Add(HistoryOfGames(question, int.Parse(readAnswer), result));
        }

    } while (readAnswer.ToLower() != "b");
}

string HistoryOfGames(string question, int answer, int result)
{
    string historyOfGames = question;
    if (answer == result)
    {
        historyOfGames += $"\n{result} is the answer. You got it right!";
        scoreRight++;
        totalScore++;
    }
    else
    {
        historyOfGames += $"\n{result} is the correct answer. You wrote {answer}.";
        totalScore++;
    }
    return historyOfGames;
}
