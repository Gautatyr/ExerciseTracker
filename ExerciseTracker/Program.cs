using ExerciseTracker.Controllers;
using ExerciseTracker.Models;
using ExerciseTracker.Repositories;
using ExerciseTracker.Services;

Console.WriteLine("Hello world");
ExerciseTrackerContext context = new ExerciseTrackerContext();
RunRepository runRepository = new RunRepository(context);
RunService runService = new RunService(runRepository);
RunController runController = new(runService);

runController.CreateRun();

Console.WriteLine("Run created");

Run run = runController.GetRunById().Result;

Console.WriteLine("Run get");

Console.WriteLine(run.Id + run.Comment);

Console.ReadLine();