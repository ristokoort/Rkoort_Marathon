using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rkoort_Marathon.Models;

namespace Rkoort_Marathon.Controllers
{
    public class FinalizeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public FinalizeController(ApplicationDBContext context)
        {
            _context = context;
        }


        public IActionResult FinalizeRunners()
        {
            var data = _context.runners.Where(x => x.EndTime == null && x.Break1 != null && x.Break2 != null).ToList();

            return View(data);
        }

        public IActionResult UpdateFinalize(int id)
        {
            string ftime = Request.Form["endtime"];
            var data = _context.runners.Where(x => x.id == id).FirstOrDefault();
            if (ftime != null)
            {
                data.EndTime = DateTime.Parse(ftime);
                _context.Update(data);
                _context.SaveChanges();

            }
            return RedirectToAction("FinalizeRunners");
        }













        // GET: Finalize
        public async Task<IActionResult> Index()
        {
            return View(await _context.runners.ToListAsync());
        }

        // GET: Finalize/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runners = await _context.runners
                .FirstOrDefaultAsync(m => m.id == id);
            if (runners == null)
            {
                return NotFound();
            }

            return View(runners);
        }

        // GET: Finalize/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Finalize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Breaks,StartTime,EndTime")] Runner runners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(runners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(runners);
        }

        // GET: Finalize/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runners = await _context.runners.FindAsync(id);
            if (runners == null)
            {
                return NotFound();
            }
            return View(runners);
        }

        // POST: Finalize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Breaks,StartTime,EndTime")] Runner runners)
        {
            if (id != runners.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(runners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RunnersExists(runners.id))
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
            return View(runners);
        }

        // GET: Finalize/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runners = await _context.runners
                .FirstOrDefaultAsync(m => m.id == id);
            if (runners == null)
            {
                return NotFound();
            }

            return View(runners);
        }

        // POST: Finalize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var runners = await _context.runners.FindAsync(id);
            _context.runners.Remove(runners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RunnersExists(int id)
        {
            return _context.runners.Any(e => e.id == id);
        }
    }
}
