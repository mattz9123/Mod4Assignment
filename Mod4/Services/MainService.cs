using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ApplicationTemplate.Services;

namespace Mod4.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>
public class MainService : IMainService
{
    private readonly IFileService _fileService;
    public MainService(IFileService fileService)
    {
        _fileService = fileService;
    }


    public void Invoke()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection
            .AddLogging(x => x.AddConsole())
            .AddTransient<IFileService, FileService>()
            .BuildServiceProvider();
        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();


        //logger.Log(LogLevel.Information, "Here is an informational message");
        string file = "movies.csv";
        string secondFile = null;
        bool continueProgram = true;

        while (continueProgram)
        {
            // read file
            if (File.Exists(file))
            {
                // Ask user if they would like to list or add movies
                Console.WriteLine("1. List movies");
                Console.WriteLine("2. Add movie");
                Console.WriteLine("3. List 10 movies");
                Console.WriteLine("4. List shows");
                Console.WriteLine("5. List videos");
                Console.WriteLine("6. Exit");
                string choice = Console.ReadLine();

                
                
                var fileService = serviceProvider.GetService<IFileService>();
                

                // add movie IDs to a list
                List<int> movieIds = new List<int>();

                // add movie titles to a list
                List<string> titles = new List<string>();

                // add movie genres to a list
                List<string> genres = new List<string>();

                Media media = null;

                try
                {
                    fileService.Read(file, movieIds, titles, genres);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
                if (choice == "1")
                {
                    // Run this function to display the movies, function found at bottom of this page
                    DisplayMovies(titles, movieIds, genres);
                }
                else if (choice == "2")
                {
                    try
                    {
                        // Run this function to write to the movies.csv file
                        fileService.Write(titles, movieIds, genres, file);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                    }
                }
                else if (choice == "3")
                {
                    // Run this to display top 10 movies file, while creating new movie object
                    secondFile = "top10movies.csv";
                    media = new Movie();
                    media.Display(secondFile);

                }
                else if (choice == "4")
                {
                    // Run this to display shows file
                    secondFile = "shows.csv";
                    media = new Show();
                    media.Display(secondFile);
                }
                else if (choice == "5")
                {
                    // Run this to display video file
                    secondFile = "videos.csv";
                    media = new Video();
                    media.Display(secondFile);
                }
                else if (choice == "6")
                {
                    // Ends the loop
                    continueProgram = false;
                }
                
            }
        }
    }

    public void DisplayMovies(List<string> titleList, List<int> movieIdsList, List<string> genresList)
    {
        // Display movies based on input from user
        Console.WriteLine($"Current number of movies: {movieIdsList.Count}");
        Console.WriteLine("Number of movies to list: ");
        int numOfMovies = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < numOfMovies; i++)
        {
            Console.WriteLine($"Movie ID: {movieIdsList[i]}, Movie Title: {titleList[i]}, Movie Genre: {genresList[i]}");
        }
    }

}
