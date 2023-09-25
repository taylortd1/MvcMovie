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
    [StringLength(100)]
    public string? Genre { get; set; }

    public string? Rating { get; set; }
    
    public decimal? Price { get; set; }
}
