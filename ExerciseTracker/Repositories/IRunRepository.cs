using ExerciseTracker.Models;

namespace ExerciseTracker.Repositories;

public interface IRunRepository : IRepository<Run>
{
    Task<Run> GetRunByIdAsync(int id);

    Task<List<Run>> GetAllRunsAsync();
}
