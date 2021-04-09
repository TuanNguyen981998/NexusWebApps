using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckListApp.Data;
using CheckListApp.Models;
using Microsoft.AspNetCore.Identity;
using CheckListApp.Areas.Identity.Data;

namespace CheckListApp.Controllers
{
    public class UsersSongHomeController : Controller
    {
        private readonly CheckListAppContext _context;
        private readonly UserManager<CheckListAppUser> _userManager;

        public UsersSongHomeController(CheckListAppContext context, UserManager<CheckListAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UsersSongHome
        public async Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var userSongs = await _context.UsersSongs.Where(u => u.UserID == userID).ToListAsync();
            var topUserPlayedSongs = userSongs.OrderBy(s => s.HitCount).Take(6).Select(s=>s.Song).ToList();
            
            return View(topUserPlayedSongs);
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.ID == id);
        }
    }
}
