using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Freezer_MVC.Models;

namespace Freezer_MVC.Controllers
{
    public class FoodGroupsController : Controller
    {
        private readonly DataContext _context;

        public FoodGroupsController(DataContext context)
        {
            _context = context;
        }

        // GET: FoodGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodGroups.ToListAsync());
        }

        // GET: FoodGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodGroup = await _context.FoodGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodGroup == null)
            {
                return NotFound();
            }

            return View(foodGroup);
        }

        // GET: FoodGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FoodGroup foodGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodGroup);
        }

        // GET: FoodGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodGroup = await _context.FoodGroups.FindAsync(id);
            if (foodGroup == null)
            {
                return NotFound();
            }
            return View(foodGroup);
        }

        // POST: FoodGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FoodGroup foodGroup)
        {
            if (id != foodGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodGroupExists(foodGroup.Id))
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
            return View(foodGroup);
        }

        // GET: FoodGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodGroup = await _context.FoodGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodGroup == null)
            {
                return NotFound();
            }

            return View(foodGroup);
        }

        // POST: FoodGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodGroup = await _context.FoodGroups.FindAsync(id);
            _context.FoodGroups.Remove(foodGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodGroupExists(int id)
        {
            return _context.FoodGroups.Any(e => e.Id == id);
        }
    }
}
