//Create a math game using the four operations
//It must show a menu asking the user to choose an operation
//The results from division should result in INTERGERS ONLY, and dividends should go from 0 to 100
//Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.
//An random operation is show to the user and he has to type an answer
//Record previous games in a list and have an option to visualize the history of previous games on the menu

string menuSelection;
string readAnswer;
List<string> answerList = new List<string>();
int x;
int y;
int result;
string question;

Random random = new Random();

do
{
    Console.WriteLine("""
    Math Game
    Type to choose an operation:
    1 - Sum
    2 - Subtraction
    3 - Multiplication
    4 - Division
    5 - Visualise your previous games
    exit to end
    """);

    menuSelection = Console.ReadLine();

    switch (menuSelection)
    {
        case "1":
        PlayGame("+", Sum);
        break;
        case "2":
        PlayGame("-", Subtraction);
        break;
        case "3":
        PlayGame("*", Multiplication);
        break;
        case "4":
        PlayGame("/", Division);
        break;
        case "5":
        VisualizePreviousGames();
        break;
    }
            
} while (menuSelection.ToLower() != "exit");

int Sum(int x, int y)
{   
    return x + y;
}
int Subtraction(int x, int y)
{   
    return x - y;
}
int Multiplication(int x, int y)
{   
    return x * y;
}

int Division(int x, int y)
{   
    return x / y;
}

void VisualizePreviousGames()
{
    Console.WriteLine();
    foreach (string item in answerList)
    {
        Console.WriteLine($"{item} ");
    }
    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}

//The Func<int, int, int> delegate type indicates a method that takes two int parameters and returns an int.
//The first 'int' is the return type, the second and third 'int are the first and second parameter

void PlayGame(string operatorName, Func<int, int, int> operationFunction)
{
    do
    {   
        Console.WriteLine($"Type 'back' to go back to the menu");

        x = random.Next(0, 101);

        if (operationFunction == Division)
        {
            do
            {
                y = random.Next(1, 101);
            } while (x % y != 0);
        } else
        {
            y = random.Next(0, 101);
        }

        result = operationFunction(x, y);
        question = $"{x} {operatorName} {y} = ?";
        Console.WriteLine(question);
        readAnswer = Console.ReadLine();
;
        if (readAnswer.ToLower() != "back")
        {
            answerList.Add(HistoryOfGames(question, int.Parse(readAnswer), result));
        }

 
    } while (readAnswer.ToLower() != "back");
}

int RandomNumber()
{
    Random random = new Random();
    return random.Next(0, 101);
}

string HistoryOfGames(string question, int answer, int result)
{   
    string historyOfGames = question;
    if (answer == result)
    {
        historyOfGames += $"\n{result} is the answer. You got it right!";
    } else
    {
        historyOfGames += $"\n{result} is the correct answer. You wrote {answer}.";
    }
    return historyOfGames;
}
