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
    public class BreakController : Controller
    {
        private readonly ApplicationDBContext _context;

        public BreakController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult UpdateRunnerBreaks(int id)
        {
            ViewBag.id = id;
            IEnumerable<Runner> data = _context.runners.ToList();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddBreaks(int id)
        {
           
            ViewBag.id = id;

            string stime1 = Request.Form["breaktime1"];
            string stime2 = Request.Form["breaktime2"];

            if (id == 1)
            {
                await _context.runners.ForEachAsync(x => x.Break1_time1 = DateTime.Parse(stime1));
                _context.SaveChanges();
                await _context.runners.ForEachAsync(x => x.Break1_time2 = DateTime.Parse(stime2));
                _context.SaveChanges();
            }
            else
            {
                await _context.runners.ForEachAsync(x => x.Break2_time1 = DateTime.Parse(stime1));
                _context.SaveChanges();
                await _context.runners.ForEachAsync(x => x.Break2_time2 = DateTime.Parse(stime2));
                _context.SaveChanges();
            }

            IEnumerable<Runner> data = _context.runners.ToList();

            return RedirectToAction("AddBreaks", new { id = id });

        }











        // GET: Break
        public async Task<IActionResult> Index()
        {
            return View(await _context.runners.ToListAsync());
        }

        // GET: Break/Details/5
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

        // GET: Break/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Break/Create
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

        // GET: Break/Edit/5
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

        // POST: Break/Edit/5
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

        // GET: Break/Delete/5
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

        // POST: Break/Delete/5
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
