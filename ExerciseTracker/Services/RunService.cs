﻿using ExerciseTracker.Models;
using ExerciseTracker.Repositories;

namespace ExerciseTracker.Services;

public class RunService : IRunService;
{
    private readonly IRunRepository _runRepository;

    public RunService(IRunRepository runRepository)
    {
        _runRepository = runRepository;
    }

    public async Task<List<Run>> GetAllRunsAsync()
    {
        return await _runRepository.GetAllRunsAsync();
    }

    public async Task<Run> GetRunByIdAsync(int id)
    {
        return await _runRepository.GetRunByIdAsync(id);
    }

    public async Task<Run> AddCustomerAsync(Run newRun)
    {
        return await _runRepository.AddAsync(newRun);
    }
}