using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Mod4.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileService : IFileService
{
    private readonly ILogger<IFileService> _logger;

    public FileService(ILogger<IFileService> logger)
    {
        _logger = logger;
    }
    public void Read(string file, List<int> movieIds, List<string> titles, List<string> genres) // Reads the file
    {
        StreamReader sr = new StreamReader(file);
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            int movieIndex = line.IndexOf('"');
            if (movieIndex == -1)
            { // Add to the lists in the correct format
                string[] movieInfo = line.Split(',');

                movieIds.Add(int.Parse(movieInfo[0]));

                titles.Add(movieInfo[1]);

                genres.Add(movieInfo[2].Replace("|", ", "));
            }
            else
            { // Add to the lists in the correct format
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

    public void Write(List<string> titleList, List<int> movieIdsList, List<string> genresList, string file)
    {
        {
            // Ask user for title of movie
            Console.WriteLine("Title: ");
            string addTitle = Console.ReadLine();
            // Create new ID for movie
            int newMovieID = movieIdsList[movieIdsList.Count - 1] + 1;
            if (titleList.Contains(addTitle))
            {
                Console.WriteLine("Movie already exists");
            }
            else
            {

                Console.WriteLine("Genre (if entering multiple, separate with space): ");
                string addGenre = Console.ReadLine();

                StreamWriter sw = new StreamWriter(file, true);
                // Add the user's entered title to the movie list
                titleList.Add(addTitle);
                // Add the new ID to the ID list
                movieIdsList.Add(newMovieID);
                // Add the genre(s) to the list
                genresList.Add(addGenre);
                // Format the genre(s)
                string stringGenre = addGenre.Replace(" ", "|");
                // Write the title, ID, and genre(s) to the list
                sw.WriteLine($"{newMovieID},{addTitle},{stringGenre}");
                sw.Close();
            }
        }
    }
}
