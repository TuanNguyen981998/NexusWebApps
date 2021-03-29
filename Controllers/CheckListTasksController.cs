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
using CheckListApp.ViewModels;
using static CheckListApp.Models.CheckListTask;
using Microsoft.AspNetCore.Identity;
using CheckListApp.Areas.Identity.Data;

namespace CheckListApp.Controllers
{
    public class CheckListTasksController : Controller
    {
        private readonly CheckListAppContext _context;
        private readonly UserManager<CheckListAppUser> _userManager;
        

        public CheckListTasksController(CheckListAppContext context, UserManager<CheckListAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CheckListTasks
        [Authorize]
        public async Task<IActionResult> Index(CheckListTasksViewModel checkListTasksViewModel)
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            List<CheckListTask> userCheckList = await _context.CheckListTasks.Where(t => t.UserID == userID).ToListAsync();
            FilterHelper(ref userCheckList, checkListTasksViewModel.SelectedPriorityFilter);
            SortHelper(ref userCheckList, checkListTasksViewModel.SelectedSortOption);
            checkListTasksViewModel.CheckListTasks = userCheckList;

            return View(checkListTasksViewModel);
        }

        // GET: CheckListTasks/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListTask = await _context.CheckListTasks
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (checkListTask == null)
            {
                return NotFound();
            }

            return View(checkListTask);
        }

        // GET: CheckListTasks/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CheckListTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,TaskDescription,TaskPriority,DateDue,DateModified,TaskStatus,UserID")] CheckListTask checkListTask)
        {
            if (ModelState.IsValid)
            {
                checkListTask.DateModified = DateTime.Now;
                checkListTask.UserID = _userManager.GetUserId(User);
                _context.Add(checkListTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", checkListTask.UserID);
            return View(checkListTask);
        }

        // GET: CheckListTasks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListTask = await _context.CheckListTasks.FindAsync(id);

            if (checkListTask == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", checkListTask.UserID);
            return View(checkListTask);
        }

        // POST: CheckListTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TaskDescription,TaskPriority,DateDue,DateModified,TaskStatus,UserID")] CheckListTask checkListTask)
        {
            if (id != checkListTask.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    checkListTask.DateModified = DateTime.Now;
                    _context.Update(checkListTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckListTaskExists(checkListTask.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", checkListTask.UserID);
            return View(checkListTask);
        }

        // GET: CheckListTasks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkListTask = await _context.CheckListTasks
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (checkListTask == null)
            {
                return NotFound();
            }

            return View(checkListTask);
        }

        // POST: CheckListTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkListTask = await _context.CheckListTasks.FindAsync(id);
            _context.CheckListTasks.Remove(checkListTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckListTaskExists(int id)
        {
            return _context.CheckListTasks.Any(e => e.ID == id);
        }

        private void FilterHelper(ref List<CheckListTask> checkList, string filterOption)
        {
            if (!String.IsNullOrEmpty(filterOption)
                && filterOption != "All")
            {
                TASKPRIORITY priorityFilter;
                switch (filterOption)
                {
                    case "0":
                        priorityFilter = TASKPRIORITY.LOW;
                        break;
                    case "1":
                        priorityFilter = TASKPRIORITY.MEDIUM;
                        break;
                    case "2":
                        priorityFilter = TASKPRIORITY.HIGH;
                        break;
                    case "3":
                        priorityFilter = TASKPRIORITY.CRITICAL;
                        break;
                    default:
                        priorityFilter = TASKPRIORITY.LOW;
                        break;
                }
                checkList = checkList.Where(task => task.TaskPriority == priorityFilter).ToList();
            }
        }
        private void SortHelper(ref List<CheckListTask> checkList, string sortOption)
        {
            if (!String.IsNullOrEmpty(sortOption)
                && sortOption != "-1")
            {
                switch (sortOption)
                {
                    case "0":
                        checkList = checkList.OrderBy(task => task.TaskDescription).ToList();
                        break;
                    case "1":
                        checkList = checkList.OrderByDescending(task => task.TaskDescription).ToList();
                        break;
                    case "2":
                        checkList = checkList.OrderBy(task => task.DateDue).ToList();
                        break;
                    case "3":
                        checkList = checkList.OrderByDescending(task => task.DateDue).ToList();
                        break;
                    case "4":
                        checkList = checkList.OrderBy(task => task.TaskPriority).ToList();
                        break;
                    default:
                        checkList = checkList.OrderByDescending(task => task.TaskPriority).ToList();
                        break;
                }

            }
        }

        public async Task<IActionResult> UpdateTimer(int id)
        {
            CheckListTask checkListTask = await _context.CheckListTasks.FindAsync(id);
            return PartialView("TimerPartial", checkListTask);
        }
    }
}
