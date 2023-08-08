using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        // using id as the parameter matches the optional placeholder for default routes
        // GET: Movies
        public async Task<IActionResult> Index(string? searchString, string? movieGenre, DateTime movieReleaseDate, decimal? moviePrice, string? movieRating)
        {

            
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            
            //LINQ query to get list of ratings
            IQueryable<string?> ratingQuery = from m in _context.Movie
                                              orderby m.Rating
                                              select m.Rating;

            //LINQ query to get list of prices
            IQueryable<decimal?> priceQuery = from m in _context.Movie
                                             orderby m.Price
                                             select m.Price;

            //LINQ query to get list of releaseDates
            IQueryable<DateTime?> dateQuery = from m in _context.Movie
                                             orderby m.ReleaseDate
                                             select m.ReleaseDate;
            
            //LINQ to get list of genres.
            IQueryable<string?> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;
            
            //LINQ query to get list of movies.
            var movies = from m in _context.Movie
                         select m;

            //Query the user input agains the list of all titles
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }
            //Query user input against the list of all genres
            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            //Query user selection against the list of all dates
            if (movieReleaseDate > DateTime.MinValue)
            {
                movies = movies.Where(x => x.ReleaseDate.Equals(movieReleaseDate));
            }
            //Query user selection against the list of all prices
            if (moviePrice > 0)
            {
                movies = movies.Where(x => x.Price.Equals(moviePrice));
            }
            //Query user selection against the list of all ratings
            if (!string.IsNullOrEmpty(movieRating))
            {
                movies = movies.Where(x => x.Rating == movieRating);
            }
            //intialize and set the view model.
            var movieGenreVM = new MovieSearchViewModel
            {
                Ratings = new SelectList(await ratingQuery.Distinct().ToListAsync()),
                Prices = new SelectList(await priceQuery.Distinct().ToListAsync()),
                ReleaseDates = new SelectList(await dateQuery.Distinct().ToListAsync()),
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        //Added Rating to the property [Bind]ing list 
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        //Added Rating to the property [Bind]ing list(adding new field to an already set DB schema)
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
