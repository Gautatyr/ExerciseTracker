using ExerciseTracker.Services;
using ExerciseTracker.Models;

namespace ExerciseTracker.Controllers;

public class RunController 
{
    private readonly IRunService _runService;

    public RunController(IRunService runService)
    {
        _runService = runService;
    }

    public async Task<Run> GetRunById()
    {
        return await _runService.GetRunByIdAsync(1);
    }

    public async Task<Run> CreateRun()
    {
        var run = new Run
        {
            Start = DateTime.Now,
            End = DateTime.Now,
            Duration = TimeSpan.Zero,
            Comment = "Its working",
            Distance = "4,4km"
        };

        return await _runService.AddRunAsync(run);
    }
}
