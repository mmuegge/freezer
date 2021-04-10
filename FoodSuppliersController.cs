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
    public class FoodSuppliersController : Controller
    {
        private readonly DataContext _context;

        public FoodSuppliersController(DataContext context)
        {
            _context = context;
        }

        // GET: FoodSuppliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodSuppliers.ToListAsync());
        }

        // GET: FoodSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSupplier = await _context.FoodSuppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodSupplier == null)
            {
                return NotFound();
            }

            return View(foodSupplier);
        }

        // GET: FoodSuppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FoodSupplier foodSupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodSupplier);
        }

        // GET: FoodSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSupplier = await _context.FoodSuppliers.FindAsync(id);
            if (foodSupplier == null)
            {
                return NotFound();
            }
            return View(foodSupplier);
        }

        // POST: FoodSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FoodSupplier foodSupplier)
        {
            if (id != foodSupplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodSupplierExists(foodSupplier.Id))
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
            return View(foodSupplier);
        }

        // GET: FoodSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSupplier = await _context.FoodSuppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodSupplier == null)
            {
                return NotFound();
            }

            return View(foodSupplier);
        }

        // POST: FoodSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodSupplier = await _context.FoodSuppliers.FindAsync(id);
            _context.FoodSuppliers.Remove(foodSupplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodSupplierExists(int id)
        {
            return _context.FoodSuppliers.Any(e => e.Id == id);
        }
    }
}
