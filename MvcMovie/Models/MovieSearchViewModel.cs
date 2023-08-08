using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;

public class MovieSearchViewModel
{
    //Contains the list of movies
    public List<Movie>? Movies { get; set; }
    //Contains the list of genres
    public SelectList? Genres { get; set; }
    //Contains the list of release dates
    [DataType(DataType.Date)]
    public SelectList? ReleaseDates { get; set; }
    //Contains the list of prices
    public SelectList? Prices { get; set; }
    //Contains the list of ratings
    public SelectList? Ratings { get; set; }
    //Contains the selected search string
    public string? SearchString { get; set; }
    //Contains the selected genre
    public string? MovieGenre { get; set; }
    //Contains the selected release date
    
    public DateTime? MovieReleaseDate { get; set; }
    //Contains the selected price
    public decimal? MoviePrice { get; set; }
    //Contains the selected rating
    public string? MovieRating { get; set; }
    


}