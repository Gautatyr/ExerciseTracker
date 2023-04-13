using ExerciseTracker.Services;
using ExerciseTracker.Models;

namespace ExerciseTracker.Controllers;

public class RunController : Controller
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

    public async Task<Run> CreateRun(Run run)
    {
        return await _runService.AddRunAsync(run);
    }

    public async Task<List<Run>> GetAllRunsAsync()
    {
        return await _runService.GetAllRunsAsync();
    }
}
