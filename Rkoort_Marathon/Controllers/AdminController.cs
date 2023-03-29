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
    public class AdminController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult ConfigureAdmin()
        {


            var data = _context.runners.OrderBy(x => x.Breaks).ToList();

            return View(data);

        }

        [HttpPost]
        public IActionResult Update(int id)
        {

            string fname = Request.Form["fname"];
            string lname = Request.Form["lname"];
            string stime = Request.Form["stime"];
            string ltime = Request.Form["ltime"];
            string breaks = Request.Form["breaks"];

            Runners rns = new Runners
            {
                id = id,
                FirstName = fname,
                LastName = lname,
                StartTime = DateTime.Parse(stime),
                EndTime = DateTime.Parse(ltime),
                Breaks = int.Parse(breaks)
            };

            _context.Update(rns);
            _context.SaveChanges();

            return RedirectToAction("ConfigureAdmin");

        }













        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.runners.ToListAsync());
        }

        // GET: Admin/Details/5
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

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Breaks,StartTime,EndTime")] Runners runners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(runners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(runners);
        }

        // GET: Admin/Edit/5
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

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Breaks,StartTime,EndTime")] Runners runners)
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

        // GET: Admin/Delete/5
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

        // POST: Admin/Delete/5
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
