using ExerciseTracker.Models;

namespace ExerciseTracker.Controllers;

public interface IRunController
{
    Task<Run> GetRunByIdAsync(int id);
    Task<Run> CreateRunAsync(Run run);
    Task<Run> UpdateRunAsync(Run run);
    Task<Run> DeleteRunAsync(Run run);
}
