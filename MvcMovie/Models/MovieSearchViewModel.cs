using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;

public class MovieSearchViewModel
{
    public List<Movie>? Movies { get; set; }
    public SelectList? Genres { get; set; }
    public string? MovieGenre { get; set; }
    public string? SearchString { get; set; }
    public SelectList? ReleaseDates { get; set; }
    public DateTime? MovieReleaseDate { get; set; }
    public decimal? MoviePrice { get; set; }
    public SelectList? Prices { get; set; }


}