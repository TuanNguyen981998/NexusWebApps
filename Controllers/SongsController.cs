using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckListApp.Data;
using CheckListApp.Models;
using Microsoft.AspNetCore.Authorization;
using CheckListApp.Areas.Identity.Data;
using CheckListApp.ViewModels;

namespace CheckListApp.Controllers
{
    [Authorize(Roles = RolesAndOperationsConstants.AdministratorsRole)]
    public class SongsController : Controller
    {
        private readonly CheckListAppContext _context;

        public SongsController(CheckListAppContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(SongsManagementViewModel songsManagementViewModel)
        {

            List<Song> songsCollection = await _context.Songs.ToListAsync();
            FilterHelper(ref songsCollection, songsManagementViewModel.InputFilterName);
            SortHelper(ref songsCollection, songsManagementViewModel.SelectedSortOption);
            songsManagementViewModel.SongsCollection = songsCollection;
            return View(songsManagementViewModel);
        }

        private void SortHelper(ref List<Song> songsCollection, string selectedSortOption)
        {
            if (!String.IsNullOrEmpty(selectedSortOption)
               && selectedSortOption != "-1")
            {
                switch (selectedSortOption)
                {
                    case "0":
                        songsCollection = songsCollection.OrderBy(song => song.Title).ToList();
                        break;
                    case "1":
                        songsCollection = songsCollection.OrderByDescending(song => song.Title).ToList();
                        break;
                    case "2":
                        songsCollection = songsCollection.OrderBy(song => song.AddedDate).ToList();
                        break;
                    case "3":
                        songsCollection = songsCollection.OrderByDescending(song => song.AddedDate).ToList();
                        break;
                    default:
                        songsCollection = songsCollection.OrderBy(song => song.Title).ToList();
                        break;
                }


            }
        }

        private void FilterHelper(ref List<Song> songsCollection, string inputFilterName)
        {
            if (!String.IsNullOrEmpty(inputFilterName))
            {
                songsCollection = songsCollection.Where(song => song.Title.Contains(inputFilterName)).ToList();
            }
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Artist,Duration,ReleaseDate,AddedDate")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Artist,Duration,ReleaseDate,AddedDate")] Song song)
        {
            if (id != song.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.ID))
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
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.ID == id);
        }
    }
}
