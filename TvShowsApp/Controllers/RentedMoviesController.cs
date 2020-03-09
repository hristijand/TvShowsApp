using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TvShowsApp.Models;

namespace TvShowsApp.Controllers
{
    [Authorize]
    public class RentedMoviesController : Controller
    {
        private readonly TvShowsContext _context;

        public RentedMoviesController(TvShowsContext context)
        {
            _context = context;
        }

        // GET: RentedMovies
        public async Task<IActionResult> Index()
        {
            
            //ViewBag.TvShowID = id;
           var tvShowsContext = _context.RentedMovies.Include(r => r.TvShow);
           return View(await tvShowsContext.ToListAsync());
        }

        // GET: RentedMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedMovies = await _context.RentedMovies
                .FirstOrDefaultAsync(m => m.RentedMovieId == id);
            if (rentedMovies == null)
            {
                return NotFound();
            }

            return View(rentedMovies);
        }

        // GET: RentedMovies/Create
        public IActionResult Create(int id)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name");
            ViewData["TvShowID"] = new SelectList(_context.TvShow.Where(x => !_context.RentedMovies.Select(y => y.TvID).Contains(x.TvShowsId)), "ID", "Title");
            ViewBag.TvShowID = id;
            return View();

        }

        // POST: RentedMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentedMovieId,UsersID,TvShowID,RentDate,ReturnDate")] RentedMovies rentedMovies)
        {
            if (ModelState.IsValid)
            {

                _context.Add(rentedMovies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            ViewData["UsersID"] = new SelectList(_context.Users, "UsersID", "Name", rentedMovies.UsersID);
            ViewData["TvShowID"] = new SelectList(_context.TvShow, "TvShowID", "ImageUrl", rentedMovies.TvID);
            return View(rentedMovies);
            
        }

        // GET: RentedMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedMovies = await _context.RentedMovies.FindAsync(id);
            if (rentedMovies == null)
            {
                return NotFound();
            }
            return View(rentedMovies);
        }

        // POST: RentedMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentedMovieId,UsersID,TvShowID,RentDate,ReturnDate")] RentedMovies rentedMovies)
        {
            if (id != rentedMovies.RentedMovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(rentedMovies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentedMoviesExists(rentedMovies.RentedMovieId))
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
            return View(rentedMovies);
        }

        // GET: RentedMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentedMovies = await _context.RentedMovies
                .FirstOrDefaultAsync(m => m.RentedMovieId == id);
            if (rentedMovies == null)
            {
                return NotFound();
            }

            return View(rentedMovies);
        }

        // POST: RentedMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentedMovies = await _context.RentedMovies.FindAsync(id);
            _context.RentedMovies.Remove(rentedMovies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentedMoviesExists(int id)
        {
            return _context.RentedMovies.Any(e => e.RentedMovieId == id);
        }
        public void /*Task<IActionResult>*/ Rent(int TvShowId, int UserId)
        {
            RentedMovies rentedMovies = new RentedMovies();
            rentedMovies.TvID = TvShowId;
            rentedMovies.UsersID = UserId;
            rentedMovies.RentDate = DateTime.Now;
            _context.Add(rentedMovies);
            /*await _context.SaveChangesAsync();*/


            TvShow TvShow = _context.TvShow.FirstOrDefault(x => x.TvShowsId == TvShowId);
            TvShow.Available = true;
            _context.Update(TvShow);

            _context.SaveChanges();
            /*await _context.SaveChangesAsync();*/
            /*return RedirectToAction("Index", "TvShows");*/
            }
        [HttpPost]
        /*public async Task<IActionResult> Return(int TvShowId, int UserId)*/
        public void /*Task<IActionResult>*/ Return(int TvShowId, int UserId)
        {
            RentedMovies rentedMovies = _context.RentedMovies.FirstOrDefault(x => x.ReturnDate == null && x.TvID == TvShowId);
            rentedMovies.ReturnDate = DateTime.Now;
            _context.Update(rentedMovies);
            /*await _context.SaveChangesAsync();*/

            TvShow TvShow = _context.TvShow.FirstOrDefault(x => x.TvShowsId == TvShowId);
            TvShow.Available = false;
            _context.Update(TvShow);

            _context.SaveChanges();

            /*await _context.SaveChangesAsync();
            return RedirectToAction("Index", "TvShows");*/
        }

    }
}
