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
    public class FoodsController : Controller
    {
        private readonly DataContext _context;
        private bool order = true;      // true: aufsteigend sortieren

        public FoodsController(DataContext context)
        {
            _context = context;

        }

        // GET: Foods
        public async Task<IActionResult> Index(string foodsearch, string sorting)
        {
            ViewData["GetFooddetails"] = foodsearch;
            ViewData["SortingName"] = string.IsNullOrEmpty(sorting) ? "Name" : "";
            
            var dataContext = _context.Foods.Include(f => f.FoodGroup).Include(f => f.FoodSupplier);
           
            var foodquery =     from x in dataContext
                                orderby x.Name ascending
                                select x;
           
            if (String.IsNullOrEmpty(foodsearch) && String.IsNullOrEmpty(sorting))
            {
                return View(await foodquery.ToListAsync());
            }
            if (!String.IsNullOrEmpty(foodsearch))
            {
                //var foodquery = from x in dataContext select x;
                foodquery = foodquery.Where(x => x.Name.Contains(foodsearch) || x.FoodGroup.Name.Contains(foodsearch)).OrderBy(x => x.Name);
                //return View(await foodquery.AsNoTracking().ToListAsync());
            }

            //var foodquery = from x in dataContext select x;
            if (!String.IsNullOrEmpty(sorting))
            {
                switch (sorting)
                {
                    case "Name":
                        foodquery = foodquery.OrderByDescending(x => x.Name);
                        break;
                    default:
                        foodquery = foodquery.OrderByDescending(x => x.Name);
                        break;
                }


                //foodquery = foodquery.Where(x => x.Name.Contains(foodsearch) || x.FoodGroup.Name.Contains(foodsearch));
                //return View(await foodquery.AsNoTracking().ToListAsync());
            }
            return View(await foodquery.AsNoTracking().ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodGroup)
                .Include(f => f.FoodSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["FoodGroupId"] = new SelectList(_context.FoodGroups, "Id", "Name");
            ViewData["FoodSupplierId"] = new SelectList(_context.FoodSuppliers, "Id", "Name");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,DateIn,BestBeforeDate,FoodSupplierId,FoodGroupId")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodGroupId"] = new SelectList(_context.FoodGroups, "Id", "Name", food.FoodGroupId);
            ViewData["FoodSupplierId"] = new SelectList(_context.FoodSuppliers, "Id", "Name", food.FoodSupplierId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["FoodGroupId"] = new SelectList(_context.FoodGroups, "Id", "Name", food.FoodGroupId);
            ViewData["FoodSupplierId"] = new SelectList(_context.FoodSuppliers, "Id", "Name", food.FoodSupplierId);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,DateIn,BestBeforeDate,FoodSupplierId,FoodGroupId")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
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
            ViewData["FoodGroupId"] = new SelectList(_context.FoodGroups, "Id", "Name", food.FoodGroupId);
            ViewData["FoodSupplierId"] = new SelectList(_context.FoodSuppliers, "Id", "Name", food.FoodSupplierId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodGroup)
                .Include(f => f.FoodSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
