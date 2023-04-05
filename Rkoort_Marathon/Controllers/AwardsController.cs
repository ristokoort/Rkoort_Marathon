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
    public class AwardsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AwardsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AwardTheRunners()
        {
            List<Award> awds = new List<Award>();

            var data = _context.runners.Where(x => x.EndTime != null).ToList();

            for (int x = 0; x < data.Count; x++)
            {
                DateTime break1start = (DateTime)data[x].Break1;




                DateTime break2start = (DateTime)data[x].Break2;





                DateTime endtime = (DateTime)data[x].EndTime;


                DateTime FinalTime = endtime;

                TimeSpan tm = ((TimeSpan)(FinalTime - data[x].StartTime));



                Award awd = new Award
                {
                    FirstName = data[x].FirstName,
                    LastName = data[x].LastName,
                    StartTime = data[x].StartTime,
                    EndTime = data[x].EndTime,
                    time = tm.TotalSeconds,
                    Break1 = data[x].Break1,
                    Break2 = data[x].Break2

                };

                awds.Add(awd);
            }

            var passData = awds.OrderBy(x => x.time).ToList();

            return View(passData);

        }









        // GET: Awards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Award.ToListAsync());
        }

        // GET: Awards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var award = await _context.Award
                .FirstOrDefaultAsync(m => m.id == id);
            if (award == null)
            {
                return NotFound();
            }

            return View(award);
        }

        // GET: Awards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Awards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,StartTime,EndTime,time")] Award award)
        {
            if (ModelState.IsValid)
            {
                _context.Add(award);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(award);
        }

        // GET: Awards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var award = await _context.Award.FindAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        // POST: Awards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,StartTime,EndTime,time")] Award award)
        {
            if (id != award.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(award);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwardExists(award.id))
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
            return View(award);
        }

        // GET: Awards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var award = await _context.Award
                .FirstOrDefaultAsync(m => m.id == id);
            if (award == null)
            {
                return NotFound();
            }

            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var award = await _context.Award.FindAsync(id);
            _context.Award.Remove(award);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwardExists(int id)
        {
            return _context.Award.Any(e => e.id == id);
        }
    }
}
