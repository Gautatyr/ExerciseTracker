using ExerciseTracker;
using ExerciseTracker.Controllers;
using ExerciseTracker.Models;
using ExerciseTracker.Repositories;
using ExerciseTracker.Services;

ExerciseTrackerContext context = new ExerciseTrackerContext();
RunRepository runRepository = new RunRepository(context);
RunService runService = new RunService(runRepository);
RunController runController = new(runService);



UserInput userInput = new(runController);

userInput.MainMenu();