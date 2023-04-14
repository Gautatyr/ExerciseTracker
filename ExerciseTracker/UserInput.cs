using ConsoleTableExt;
using ExerciseTracker.Controllers;
using ExerciseTracker.Models;
using static ExerciseTracker.DataValidation;

namespace ExerciseTracker;

public class UserInput
{
    private readonly RunController controller;
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

            DisplayRunTable(controller.GetAllRunsAsync().Result);

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
                    Console.WriteLine("\nType the id of the run you want to Delete\n");
                    var id = GetRunIdInput(controller);
                    await controller.DeleteRunAsync(controller.GetRunById(id).Result);
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

        DisplayRunTable(controller.GetAllRunsAsync().Result);

        Console.WriteLine("Type the id of the run you wish to Update");

        var id = GetRunIdInput(controller);

        var run = controller.GetRunById(id).Result;

        Run newRun = SetupRun();

        run.Start = newRun.Start;
        run.End = newRun.End;
        run.Comment = newRun.Comment;
        run.Distance = newRun.Distance;
        run.SetDuration();

        await controller.UpdateRunAsync(run);
    }

    private static Run SetupRun()
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

    private static void DisplayRunTable(List<Run> list)
    {
        ConsoleTableBuilder
                .From(list)
                .WithTitle("EXERCISE TRACKER", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                .WithColumn("ID", "Starting Time", "Ending Time", "Run Duration", "Distance", "Comment")
                .WithCharMapDefinition(new Dictionary<CharMapPositions, char> {
                    {CharMapPositions.BottomLeft, '=' },
                    {CharMapPositions.BottomCenter, '=' },
                    {CharMapPositions.BottomRight, '=' },
                    {CharMapPositions.BorderTop, '=' },
                    {CharMapPositions.BorderBottom, '=' },
                    {CharMapPositions.BorderLeft, '|' },
                    {CharMapPositions.BorderRight, '|' },
                    {CharMapPositions.DividerY, '|' },
                })
                .WithHeaderCharMapDefinition(new Dictionary<HeaderCharMapPositions, char> {
                    {HeaderCharMapPositions.TopLeft, '=' },
                    {HeaderCharMapPositions.TopCenter, '=' },
                    {HeaderCharMapPositions.TopRight, '=' },
                    {HeaderCharMapPositions.BottomLeft, '|' },
                    {HeaderCharMapPositions.BottomCenter, '-' },
                    {HeaderCharMapPositions.BottomRight, '|' },
                    {HeaderCharMapPositions.Divider, '|' },
                    {HeaderCharMapPositions.BorderTop, '=' },
                    {HeaderCharMapPositions.BorderBottom, '-' },
                    {HeaderCharMapPositions.BorderLeft, '|' },
                    {HeaderCharMapPositions.BorderRight, '|' },
                })
                .ExportAndWriteLine();
    }
}
