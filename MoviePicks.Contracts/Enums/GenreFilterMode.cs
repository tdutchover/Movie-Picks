namespace MR.Models.Enums;

/// <summary>
/// Determines how genres are matched in an EF Core query filter.
/// </summary>
public enum GenreFilterMode
{
    // Movies will be allowed (not filtered out) if they match any of the genres.
    MatchAny,

    // Movies will allowed (not filtered out) only if they match all of the genres.
    MatchAll
}
