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
    public class AddStartController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AddStartController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult AddStartTimes()
        {
            IEnumerable<Runners> data = _context.runners.ToList();

            return View(data);
        }


        public IActionResult LoadRunners()
        {

            _context.Database.ExecuteSqlRaw("delete from runners");
            _context.Database.ExecuteSqlRaw("insert into runners(firstname,lastname,breaks) select firstname,lastname,0 from runnersMaster");

            return RedirectToAction("AddStartTimes");
        }

        [HttpPost]
        public async Task<IActionResult> AddStartTime()
        {
            //_db.Database.ExecuteSqlRaw("deleet from runners");
            //_db.Database.ExecuteSqlRaw("insert into runners(firstname,lastname) select firstname,lastname from runnersMaster");

            string stime = Request.Form["starttime"];


            await _context.runners.ForEachAsync(x => x.StartTime = DateTime.Parse(stime));
            _context.SaveChanges();

            return RedirectToAction("AddStartTimes");
        }


        [HttpGet]


        public IActionResult DeleteRunner(int id)
        {

            var data = _context.runners.Where(x => x.id == id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("AddStartTimes");
        }












        // GET: AddStart
        public async Task<IActionResult> Index()
        {
            return View(await _context.runners.ToListAsync());
        }

        // GET: AddStart/Details/5
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

        // GET: AddStart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddStart/Create
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

        // GET: AddStart/Edit/5
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

        // POST: AddStart/Edit/5
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

        // GET: AddStart/Delete/5
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

        // POST: AddStart/Delete/5
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
