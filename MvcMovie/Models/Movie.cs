using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movie
{
    public int Id { get; set; }
    
    //Validation for Title
    [StringLength(60, MinimumLength =3)]
    [Required]
    public string? Title { get; set; }
    
    //Changes the view display of ReleaseDate
    [Display(Name ="Release Date")]
    [DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }

    //Validation for genre
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string? Genre { get; set; }
    
    //Validation for price
    [Range(1,100)]
    [DataType(DataType.Currency)]
    public decimal? Price { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required]
    //Adding a new field after the DB schema is already set
    public string? Rating { get; set; }
}
