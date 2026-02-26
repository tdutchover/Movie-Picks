namespace MR.Models;

using MR.Models.Enums;
using System.Collections.Generic;

public abstract class MovieFilterCriteriaBase
{
    public int? Rating { get; set; }

    public List<string> Genres { get; set; } = new List<string>();
    
    public GenreFilterMode GenreFilterMode { get; set; }
}
