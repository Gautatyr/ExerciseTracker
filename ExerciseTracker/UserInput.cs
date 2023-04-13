using ConsoleTableExt;
using ExerciseTracker.Controllers;
using ExerciseTracker.Models;

namespace ExerciseTracker;

public class UserInput
{
    RunController controller;
    string error = string.Empty;

    public UserInput(RunController runController) 
    {
        controller = runController;
    }

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine($"\nExercise Tracker\n");

        ConsoleTableBuilder
            .From(controller.GetAllRunsAsync().Result)
            .ExportAndWriteLine();

        Console.WriteLine($"\n1 - Add Run\n");
        Console.WriteLine($"\n2 - Update Run\n");
        Console.WriteLine($"\n3 - Delete Run\n");
        Console.WriteLine($"\n0 - Close Application\n");

        if (error != string.Empty) Console.Write(error);

        var input = Console.ReadLine(); 

        switch(input )
        {
            case "1":
                // add run
                break;
            case "2":
                // update run
                break;
            case "3":
                // delete run
                break;
            case "0":
                Environment.Exit(0);
                break;
            default:
                error = "\nWrong Input: Type a number between 0 to 3";
                break;
        }
    }

    private async Task AddRun()
    {
        Console.Clear();

        Run newRun = new
        {
            start = GetDateTime(),
            end = GetDateTime(),
            duration = end - start,
            distance = $"{GetDouble()}km",
            comment = Console.ReadLine()
        };

        await controller.CreateRun(newRun);
    }
}
