using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;

public class MovieSearchViewModel
{
    //Contains the list of release dates
    [DataType(DataType.Date)]
    public SelectList? ReleaseDates { get; set; }

    //Contains the list of movies
    public List<Movie>? Movies { get; set; }

    //Contains the selected search string for movie name/overview
    public string? SearchString { get; set; }

    //Contains a list of popularity thresholds
    public SelectList? Popularities { get; set; }

    //Contains the list of genres
    public SelectList? Genres { get; set; }
    
    //Contains the selected genre
    public string? MovieGenre { get; set; }
    //Contains the selected release date

    [DataType(DataType.Date)]
    public DateTime? MovieReleaseDate { get; set; }
    
    //Contains the selected rating
    public SelectList? Ratings { get; set; }

    public SelectList? Prices { get; set; }

    public string? MovieRating { get; set; }
    public decimal? MoviePrice { get; set; }

    


}