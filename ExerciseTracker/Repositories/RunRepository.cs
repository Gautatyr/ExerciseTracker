using Microsoft.EntityFrameworkCore;
using ExerciseTracker.Models;

namespace ExerciseTracker.Repositories;

public class RunRepository : Repository<Run>, IRunRepository
{
    public RunRepository(ExerciseTrackerContext exerciseTrackerContext) : base(exerciseTrackerContext)
    {
    }

    public Task<Run> GetRunByIdAsync(int id)
    {
        return GetAll().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Run>> GetAllRunsAsync()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<Run> UpdateRunAsync(Run newRun, int id)
    {
        Run run = await GetRunByIdAsync(id);

        run.Start = newRun.Start;
        run.End = newRun.End;
        run.Comment = newRun.Comment;
        run.Distance = newRun.Distance;
        run.SetDuration();

        return run;
    }

    public async Task<Run> DeleteRunAsync(int id)
    {
        Run run = await GetRunByIdAsync(id);
        ExerciseTrackerContext.Remove(run);
        await ExerciseTrackerContext.SaveChangesAsync();
        return run;
    }
}
