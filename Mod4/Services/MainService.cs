using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            .BuildServiceProvider();
        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

        string file = "C:/Users/mattz/source/repos/AssignmentModule4/Mod4/ml-latest-small/movies.csv";
        bool continueProgram = true;

        while (continueProgram)
        {
            // read file
            if (File.Exists(file))
            {
                // Ask user if they would like to list or add movies
                Console.WriteLine("1. List movies");
                Console.WriteLine("2. Add movie");
                Console.WriteLine("3. Exit");
                string choice = Console.ReadLine();

                // add movie IDs to a list
                List<int> movieIds = new List<int>();

                // add movie titles to a list
                List<string> titles = new List<string>();

                // add movie genres to a list
                List<string> genres = new List<string>();

                try
                {
                    StreamReader sr = new StreamReader(file);
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        int movieIndex = line.IndexOf('"');
                        if (movieIndex == -1)
                        {
                            string[] movieInfo = line.Split(',');

                            movieIds.Add(int.Parse(movieInfo[0]));

                            titles.Add(movieInfo[1]);

                            genres.Add(movieInfo[2].Replace("|", ", "));
                        }
                        else
                        {
                            movieIds.Add(int.Parse(line.Substring(0, movieIndex - 1)));
                            line = line.Substring(movieIndex + 1);
                            movieIndex = line.IndexOf('"');
                            titles.Add(line.Substring(0, movieIndex));
                            line = line.Substring(movieIndex + 2);
                            genres.Add(line.Replace("|", ", "));
                        }
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }


                if (choice == "1")
                {
                    Console.WriteLine($"Current number of movies: {movieIds.Count}");
                    Console.WriteLine("Number of movies to list: ");
                    int numOfMovies = Convert.ToInt32(Console.ReadLine());

                    for (int i = 0; i < numOfMovies; i++)
                    {
                        Console.WriteLine($"Movie ID: {movieIds[i]}, Movie Title: {titles[i]}, Movie Genre: {genres[i]}");
                    }
                }
                else if (choice == "2")
                {

                    Console.WriteLine("Title: ");
                    string addTitle = Console.ReadLine();
                    int newMovieID = movieIds[movieIds.Count - 1] + 1;
                    try
                    {
                        if (titles.Contains(addTitle))
                        {
                            Console.WriteLine("Movie already exists");
                        }
                        else
                        {
                            titles.Add(addTitle);
                            movieIds.Add(newMovieID);

                            Console.WriteLine("Genre (if entering multiple, separate with space): ");
                            string addGenre = Console.ReadLine();
                            string stringGenre = addGenre.Replace(" ", "|");

                            genres.Add(addGenre);

                            StreamWriter sw = new StreamWriter(file, true);

                            sw.WriteLine($"{newMovieID},{addTitle},{stringGenre}");
                            sw.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                    }

                }
                else if (choice == "3")
                {
                    continueProgram = false;
                }


            }
        }
    }
}
