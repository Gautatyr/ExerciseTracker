using ConsoleTableExt;
using ExerciseTracker.Controllers;
using ExerciseTracker.Models;
using static ExerciseTracker.DataValidation;

namespace ExerciseTracker;

public class UserInput
{
    RunController controller;
    string error = string.Empty;

    public UserInput(RunController runController) 
    {
        controller = runController;
    }

    public async Task MainMenuAsync()
    {
        var input = string.Empty;

        do
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

            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await AddRun();
                    break;
                case "2":
                    await UpdateRun();
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
        } while (input != "0");
    }

    private async Task AddRun()
    {
        await controller.CreateRun(SetupRun());
    }

    private async Task UpdateRun()
    {
        Console.Clear();

        ConsoleTableBuilder
                .From(controller.GetAllRunsAsync().Result)
                .ExportAndWriteLine();

        Console.WriteLine("Type the id of the run you wish to Update");

        var id = GetIntInput();

        while (controller.GetRunById(id).Result == null)
        {
            Console.WriteLine("\nError: Invalid input, try again\n");
            id = GetIntInput();
        }

        Run newRun = SetupRun();
        newRun.Id = id;

        await controller.UpdateRunAsync(newRun);
    }

    private Run SetupRun()
    {
        Console.Clear();

        Console.WriteLine("\nPlease input the run's starting date and time (dd/mm/yy HH:mm)\n");
        var start = GetDateTimeInput();
        Console.WriteLine("\nPlease input the run's ending date and time (dd/mm/yy HH:mm)\n");
        var end = GetDateTimeInput();
        Console.WriteLine("\nPlease input the run's distance in km\n");
        var distance = GetNumberInput();
        Console.WriteLine("\nType in any comment you have on the run, or leave empty\n");
        var comment = Console.ReadLine();

        Run newRun = new()
        {
            Start = start,
            End = end,
            Distance = $"{distance}km",
            Comment = comment
        };

        newRun.SetDuration();

        return newRun;
    }
}
