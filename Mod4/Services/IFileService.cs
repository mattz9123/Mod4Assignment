using System.Collections.Generic;

namespace Mod4.Services;

/// <summary>
///     This service interface only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public interface IFileService
{
    void Read(string file, List<int> movieIds, List<string> titles, List<string> genres);
    void Write(List<string> titleList, List<int> movieIdsList, List<string> genresList, string file);
}
